# 特性和自定义特性

---

## 1> 特性

- 特性是一种允许我们向程序的程序集添加元数据的语言结构，它是用于保存程序结构信息的某种特殊类型的类。

- 特性提供的功能强大的方法以将声明信息于C#代码（类型，方法，属性）等相关特性的目的告诉编译器把程序结构中的某组元数据嵌入到程序集中，它可以防止在几乎所有的声明中。

- 即特性本质是一个类，可以利用特性类为元数据添加额外信息，比如一个类，成员变量，成员方法等名为他们添加更多的额外信息，之后可以通过反射来获取这些额外信息。

---

## 2> 自定义特性

---

### 2.1 继承特性基类 Attribute

```csharp
 class MyCustomAttribute : Attribute
    {
        public string Info;
        public MyCustomAttribute(string info)
        {
            this.Info = info;
        }
        public void Func()
        {
            Console.WriteLine("这是一个特性类");
        }
    }
```

---

### 2.2 自定义特性类的使用

- 基本语法：[特型名（参数列表）]  //特性名会自动隐藏Attribute后缀

- 本质上调用特性类的构造函数，特性可以写在类，函数，变量的上一行，表示该类型具有我特性类提供的特性信息。

```csharp
[MyCustom("类特性")]
    class Myclass
    {
        [MyCustom("方法特性")]
        public void Func([MyCustom("参数特性")] int a)
        {
        }
        [MyCustom("变量特性")]
        public int value;
    }
```

- 特性的使用：

```csharp
  public class test
    {
        static void Main(string[] args)
        {
            Myclass mc = new Myclass();
            Type t = mc.GetType();//获取程序集类型成员
            //判断是否使用了特性MyCustomAttribute
            if (t.IsDefined(typeof(MyCustomAttribute),false))
            {   //判断方法  |是否特性类中有关类型声明的信息|    是否查询其父类
                //查询事件和属性时会忽略第二个参数
                Console.WriteLine("使用了有关特性");
                //只判断t类型是否使用了特性类特性,t内部成员是否使用并不知.
            }
 //获取Type中元数据的所有特性,一个类型可以使用多个特性,用object[]去接受所有的特性
            object[] arr = t.GetCustomAttributes(true);
         //是否获得继承链上的特性
         //arr中的对象是myCustomAttribute类型或其他特性类.
         //通过遍历可以查看各个MyCustomAttribute中类对象的成员info或方法func
         //仅指从t获得的特性组,即修饰MyClass的特性,不是其成员的特性.
        }
    }
```

---

### 2.3 限制自定义特性的使用范围

- 在MyCustomAtribute特性类的声明前加一个系统特性，限制该自定义特性类的使用范围。

```csharp
[AttributeUsage(AttributeTargets.枚举成员，
                                  AllowMultiple，Inherited)]
```

- AttributeTargets  标注特性能使用在那些地方，可以用 | 并列多个限制。
- AllowMultiple  是否允许多个特性实例用在同一个目标上。

- Inherited  特性是否能被派生类和重写成员继承

---

### 2.4 系统自带特性—过时特性（Obsolete）

- 用于提示用户，使用的方法等成员已经过时，建议使用新方法。

- 一般在函数前修饰函数

  [Obsolete("方法已过时"，false)]

  - false表示使用该方法会形成警告
  - true表示使用该方法会形成错误

---

### 2.5 系统自带特性—调用者信息特性

- using System.Runtime.CompilerServices；

- CallerFilePath  哪个文件调用

- CallerLineNumber  哪一行调用

- CallerMemberName  哪个方法调用

> 一般作为函数参数的特性，作为可选参数传入

```csharp
void Func（int a，[CallerFilePath] string filename = '_'）
{ 
  cw(filename);
}
```

---

### 2.6 系统自带特性—条件编译特性

- Conditional   与预处理指令#define配合使用，主要可以用在一些调试代码上，有时想执行有时不想执行的代码：

- [Conditional("自定义符号")]修饰方法，当该符号没有被#define定义，则该方法是无法被编译的，当#define定义之后，方法可以正常编译。

---

### 2.7 系统自带特性—外部dll包函数特性

- DllImport   用来标记非.Net（C#）的函数，表明该函数在一个外部的DLL当中定义，一般用来调用C或者C++的DLL包函数特性。

- 需要引用命名空间using System.Runtime.InteropServices;

- 通过DLLImport（"方法路径或完全限定名称"）可以调用外包C或C++DLL文件中的方法。

- 只能把参数传出，再返回，没有实际看到的代码。

```csharp
[DLLImport("方法限定名称")]//包括路径
public static extern int Add（int a，int b);
```

---
