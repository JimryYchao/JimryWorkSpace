# 反射

---

## 1> 程序集

- 程序集是由编译器编译得到的，供进一步编译执行的那个中间产物，在windows系统中，它一般表现为后缀为dll库文件或者是.exe(可执行文件)的格式。

---

## 2> 元数据

- 用来描述数据的数据，这个数据不仅仅用于程序上，在别的领域也有元数据（程序中的类，类中的函数，变量等信息就是程序的元数据，且他们保存在程序集中）

---

## 3> 反射

- 程序正在运行时，可以查看其他程序集的元数据或自身元数据，一个程序查看本身或者其他程序的元数据的行为就叫反射。

- 在程序运行时，通过反射可以得到其他程序或者自己程序集代码的各种信息，包括类，变量，函数等，实例化获取的元数据进行执行或者操作。

---

## 4> 反射的作用

- 因为反射可以在程序编译后获得信息，所以它提高了程序的拓展性和灵活性。
  1. 程序运行时得到所有元数据，包括元数据的特性
  2. 程序运行时，实例化对象，操作对象。
  3. 程序运行时创建新对象，用这些对象执行任务。

---

## 5> 语法相关（Type）

---

### 5.1 Type

- Type类是类的信息类，是反射功能的基础，是访问元数据的主要方式，使用Type的成员获取有关类型声明的信息（包括构造函数，方法，字段，属性和类的事件）

- 获取Type，通过对象最根本的object中的GetType()可以获取对象的type

>- Type type = obj.GetType();通过Type获取对象的基本信息
>- typeof方法 Type type = typeof(类型名称)  传入类名
>- 通过类的完全限定名称，也可以获得类型的信息。
>
>>- Type type = Type.GetType("System.Int32");//完全限定名称
>
>- 三种方式获得某个类的int a = 10；或Int32的类型指向的内存地址是同一个

---

### 5.2 得到类的程序集信息

> 可以通过声明的type得到类型所在程序集信息。

- type.Assembly  //属性，指向该类型程序集信息。

---

### 5.3 获取类中的所有公共成员

```csharp
using System.Reflection;
Type t = typeof(classA);//得到classA的所有公共成员。
//使用MemberInfo[] 数组存储classA的公共成员。
MemberInfo[] infos = t.GetMembers();
//可以得到object类中的四个public方法和classA中的公共成员，classA中的构造函数，void ctor()表示无参构造                                                void ctor(T t)表示带参构造
```

---

### 5.4 获取所有构造函数

```csharp
Type t = typeof(classA);//得到classA的所有公共成员。
ConstructorInfo[] ctors = t.GetConstructors();
```

> 获取一个构造并执行

- 得到公祖奥函数传入Type数组，数组中内容按顺序是参数类型。
- 执行构造函数传入object数组，表示按顺序传入的参数。
- 得到无参构造，无参构造没有参数，object[]中传入null

```csharp
ConstructorInfo ctor  = t.GetConstructor(new Type[0]);//无参
classA ca = ctor.Invoke(null<或者 new object[0]>) as classA;//反射得到构造并执行
```

- 得到有参构造，object[]中按顺序传入参数

```csharp
ConstructorInfo ctor2 = t.GetConstructor(
                  new Type[typeof(int),typeof(string)]);
classA ca2 = ctor2.Invoke(new object[]{15,"hello"}) as classA;
//调用构造并且传入参数
```

---

### 5.5 获取类的公共成员变量

```csharp
FieldInfo[] fieldInfos = t.GetFields();//获取所有公共成员变量
FieldInfo[] fieldInfo = t.GetField("变量名称");//没有时返回null
//常规类调用变量
classA ca = new classA();
ca.inta = 666;
//通过反射获取变量
FieldInfo[] fieldInfo = t.GetField("inta")
var a = （int）fieldInfo.Getvalue(ca);//进入ca对象中调用inta
//反射修改值
fieldInfo.SetValue（ca,15）;// 在ca中将inta改为15
```

---

### 5.6 获取类的公共成员方法

- 通过GetMothod（Type中的方法）得到类中的方法,MethodInfo是方法的反射信息

```csharp
//得到classA中的公共成员方法
MethodInfo[] methods = t.GetMethods();
MethodInfo method1 = t.GetMethod（"方法名"）；
//当方法重载时，调用时需要在参数列表中用Type[]表示参数类型
MethodInfo method2 = t.GetMethod（"方法名"，
              new type[]{typeof(参数类型)，typeof(参数类型)...}）；
//调用方法
method1.Invoke(object,object[] parameters);//第一个参数表示反射类的实例，之后的数组是参数列表，无参长度为0.
//静态方法，第一个参数用null代替new classA（）的实例。
```

---

### 5.7 其他成员（公共）

- 得到枚举：GetEnumName\<s>

- 得到接口：GetInterface\<s>

- 得到事件：GetEvent\<s>

    EventInfo ev = t.GetEvent("事件名")；

- 得属性：GetProperty\<s>--------Propertyinfo

---

## 6> 相关语法（Assembly和Activator）

---

### 6.1 Activator类

- 用于快速实例化对象的类，用于将Type对象快速实例化为对象，先得到一个Type，后快速实例化对象。

- 即通过Type对象用Activator类方法快速实例化，并返回一个object？类型。

```csharp
Type t = typeof（classB）；
//无参
classB cb = Activator.CreateInstance(t) as classB;
//有参
classB cb = Activator.CreateInstance(t, params object[]) as classB;
```

---

### 6.2 Assembly类

- 程序集类，主要用来加载其他程序集，加载后才能用Type来使用其他程序集的信息，如果想要使用不是自己程序集中的内容，需要先加载程序集如dll文件（库文件），即简单的把库文件看成一种代码仓库，它提供了给使用者一些直接拿来用的变量，函数或类。

---

#### 6.2.1 Assembly三种加载程序集的方法

- 一般在加载同一文件下的其他程序集：

    Assembly asin = Assembly.Load("程序集名称")；

- 一般用来加载不同文件下的其他程序集

    Assembly asone = Assembly.LoadFrom("包含程序集清单的文件的名称或者路径")；

- Assembly Astwo = Assembly.LoadFrom("要加载的文件的完全限定路径")；

---

#### 6.2.2 再加载程序集中的一个类对象，之后使用反射

> 加载程序集：

- Assembly asbly = Assembly.LoadFrom("完整dll文件路径/dll名称")；
  - Type[]  types = asbly.GetTypes();  表示将程序集中的各个类型封装到Type[]数组中，遍历查看它的包含内容，方便调用。
  - foreach  并输出成员cw（item）;//cw打印简写

- 在加载程序集中的某一个类对象，之后才能使用反射。
  - Type tp = asbly.GetType("完全限定名称")；
  - MemberInfo[]  members = tp.GetMembers();然后遍历成员得到成员的名称，方便成员的调用。
  - 通过反射，实例化一个类对象成员，可以使用Activator方法去调用。

---

#### 6.2.3 当类中存在枚举的对象时

- 首先得到枚举的Type,得到可以传入的参数，将asbly中的枚举类型封装到Type类中

    Type tenum = asbly.GetType("枚举的完全限定名称")；

- 枚举的成员都是成员变量，用FieldInfo[] values = tenum.GetFields();或者得到特定枚举成员，要指定名称。

- 实例化对象：

    object obj = Activator.GreateInstance(调用构造函数的类名称，params object[])

> 参数列表中枚举对象的枚举中的某一个成员值，需要将FieldInfo调用的枚举成员转换成值，即使用values.GetValue(null) 。

---

#### 6.2.4 类库工程创建

- 专门创建类库只执行代码逻辑的搭建，当在主Main类中通过引用类库的代码成员变量方法，属性，构造函数。

- 编写类库代码时，会自动打包成一个dll文件，形成一个封装的程序集，但不执行任何代码逻辑，只能被调用，在线程中实现。

- 反射对于Unity引起的基本工作机制来说，主要是在其基础上实现的。

---
