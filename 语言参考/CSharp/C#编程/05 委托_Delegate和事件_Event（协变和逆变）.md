# 委托与事件

---

## 1> 委托

- 委托可以被认为是函数的容器，理解为表示函数方法的变量类型。用来存储，传递方法，本质上是一个类，可用来定义函数的类型，不同的方法或函数必须对应和各自格式（方法签名）一致的委托。

---

### 1.1 基本语法（delegate）

<访问修饰符>\<delegate><返回值><委托名><参数列表>

- 一般声明在namespace，可以在方法中。默认为public

- 命名不能重名，不存在重载的概念。

- 可用于作类成员，方法参数。

```csharp
public delegate void D_Method();
    D_Method delemethod = new D_method(方法名)；
```

---

### 1.2 调用委托

- Delemothod.Invoke() 或 Delemothod();

---

### 1.3 多播委托

- （+, +=，- , -=）容器可以由多个方法，按一定的顺序（添加），也可以删除指定的方法。除匿名方法。当委托为空时发布调用是error的做法。

---

### 1.4 内置委托

```csharp
Action<in params object[] T>(T1,T2...)
//最多16个参数,可逆变泛型无返回委托
Func<in params object[] T,out TResult>(T1,T2...)
//可协变泛型返回值委托
```

---

## 2> 事件

- 基于委托的存在，事件是委托的安全包裹，让委托的使用更加具有安全性，是一种特殊的变量类型。

---

### 2.1 基本语法

<访问修饰符>\<event><委托类型><事件名>；

---

### 2.2 基本特征

- 事件是作为成员变量存在于类中的，用法与委托多播类似，但是事件相对于委托的区别，不能在类外部赋值，不能在类外部调用，它只能作为成员存在与类和结构体以及接口中。

```csharp
public Action myfun;
public event myfun Fun_Event;
//（多播）
myfun = 方法名
```

- 事件的订阅和取消

```csharp
Fun_Event = 方法名（或者 += 方法）
```

- 可以在类的内部像委托一样调用事件
- 事件在外部调用时，只能在声明时初始化，只能使用+=，-=进行订阅和取消。不能类的外部赋值=。只能在类成员内部去封装调用。

- 事件只能作为成员声明，不可再其他成员内部声明（除类和结构体）

---

### 2.3 在方法体中调用

```csharp
class A{
/  public event Action myevent;//在声明事件的类中使用事件
  void Func(){
    myevent = 方法；
    myevent += 方法；
    myevent.Invoke();
  }
}
class B{
  svm{
    A a = new A();
    a.myevent += 方法名；（订阅和取消）
  }
}
```

>注意>>事件在外部时只能出现在+=，-= 的左侧，只能通过订阅取消，不能像委托那样灵活多播。并且事件不能在类外部去调用，只能通过内部封装方法在类外部调用，在类的外部增加方法和或删除事件的订阅和取消。

---

## 3> 匿名方法

- 没有方法名称的函数，其使用主要配合委托和事件进行使用，脱离委托和事件，是不会使用匿名方法的。

---

### 3.1 基本语法

```csharp
var func = delegate(参数列表){   方法体   }；
-----------------------------------------------
Action a = delegate(){cw("66666")}//第一种声明方式
Action a = ()=>{cw("66666")}//第二种声明方式
var a = (int a，int b)=>{return a+b}//第三种声明方式
```

---

### 3.2 特征

- 当委托类型作为返回类型时，方法组和匿名方法都可以作为return返回。

- 缺点：当匿名方法存入委托中是无法通过（- ,-=）的方法移除，只有Action => null时，才会清除。

---

## 4> Lambda表达式

- 可以理解为匿名方法的简写，使用上与匿名方法相同，配合委托与事件使用。

---

### 4.1 基本语法

```csharp
var a = （参数列表） => {   函数逻辑   }
```

- 当参数类型与委托参数列表一致时，可以将类型名称省略。

---

### 4.2 闭包原则

- 在一个方法中使用委托或事件的变量添加匿名方法时，匿名方法使用了外层函数的变量，即使外层函数已经停止，在委托中已添加的匿名方法块中已存储了该外层变量的副本。

- 但该变量若在外层结束时发生变化，即该变量提供的值并非变量创建时的值，而是父函数范围内的最终值。

---

## 5> 用委托方法实现自定义类型的排序

---

### 5.1.1 List的sort自带排序

```csharp
List<T> list = new List<T>();
list.Sort();//正序，list.Reserse();倒序
list.Sort(index,count);
```

---

### 5.1.2 Sort排序的本质

- 本质是将对象的元素转化成IComparable\<T>接口的类型，在调用接口的CompareTo(T t)

方法，在List\<T>类中，继承接口IComparable\<T>并实现其方法，指定规则，当使用sort（）方法时，按照接口方法定义的规则排序。

---

### 5.1.3 自定义类型排序

```csharp
class Test:IComparable<Test>{
    public int Money;
    public Test(int money){
      this.Money = money;
    }
    public int CompareTo(Test other){
    //实现接口方法时，返回1和-1表示排在对象右侧还是左侧
      if(Money>other.Money){
        return 1;
      }else{
        return -1;
      }
    }
}
```

---

### 5.1.4 通过委托类型排序（系统自带）

- Comparison\<in T>(T x,T y) 返回一个int值，

- list.Sort(Comparison\<in T>  comparison)

1. 创建一个方法：

```csharp
public int ListCompare(Test a,Test b){
//此方法用于模拟实现CompareTo()方法
  return a.Money>b.Money?1:-1;
}                             
//调用委托排序
Comparison<Test> Comp = ListCompare;
list.Sort(ListCompare)
```

---

### 5.1.5 Sort排序总结

- 系统内置委托类型可以直接Sort()排序，自定义类型排序可以继承接口Comparison\<T>去实现CompareTo方法，或者使用lambda匿名委托方法或委托方法（自定义排序规则）

```csharp
list.Sort((a,b)=>a.Money>b.Money?1;-1);
```

---

## 6> 协变和逆变

- 协变表示父类容器装子类对象（里氏替换原则），即派生程度大的用派生程度小的去表示，string=- object

- 逆变表示：由于里氏替换，原则上子类不能用父类实例化，即object对象不能直接表示其子类变量，需要强转，逆变表示可以让object逆向转变成string而不用强转。

- 协变out和逆变in用来修饰泛型的，用于在泛型中修饰泛型字母，且只能在泛型接口和委托中使用，in只能用于传入参数，out只能用于返回参数修饰。

---
