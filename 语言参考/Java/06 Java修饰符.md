# Java 修饰符


---


## 1> 访问修饰符

* default：同一个包内可见，使用对象为类，接口，变量，方法。
* private：在同一个类内可见，修饰变量，方法，不能修饰类(外部类)
* public：对所有类可见，修饰类，接口，变量，方法
* protected：对同一个包内的类和所有子类可见，修饰变量和方法，不能修饰类(外部类)。

---


### 1.1 默认访问修饰

* 接口变量隐式为public static final
* 接口的方法默认为public

---


### 1.2 私有访问修饰

* 类和接口不能声明为private

---


### 1.3 公有访问修饰

* public修饰的对象能够被任何其它类访问到
* Java程序的Main方法需要设置为public，这样Java解释器才可以运行该类。

---


### 1.4 受保护的访问修饰

* protected 需要从以下两个点来分析说明：
    * 子类与基类在同一包中：被声明为 protected 的变量、方法和构造器能被同一个包中的任何其他类访问；
    * 子类与基类不在同一包中：那么在子类中，子类实例可以访问其从基类继承而来的 protected 方法，而不能访问基类实例的protected方法。

---


### 1.5 访问控制和继承

* 请注意以下方法继承的规则：
    * 父类中声明为 public 的方法在子类中也必须为 public。
    * 父类中声明为 protected 的方法在子类中要么声明为 protected，要么声明为 public，不能声明为 private。
    * 父类中声明为 private 的方法，不能够被继承。

---


## 2> 非访问修饰符


---
### 

### 2.1 static

* 静态变量：
    * static 关键字用来声明独立于对象的静态变量，无论一个类实例化多少对象，它的静态变量只有一份拷贝。 静态变量也被称为类变量。局部变量不能被声明为 static 变量。
* 静态方法：
    * static 关键字用来声明独立于对象的静态方法。静态方法不能使用类的非静态变量。静态方法从参数列表得到数据，然后计算这些数据。

---


### 2.2 final

>final变量
* final 表示"最后的、最终的"含义，变量一旦赋值后，不能被重新赋值。被 final 修饰的实例变量必须显式指定初始值。
* final 修饰符通常和 static 修饰符一起使用来创建类常量。
```java
public class Test{
  final int value = 10;
  // 下面是声明常量的实例
  public static final int BOXWIDTH = 6;
  static final String TITLE = "Manager";
 
  public void changeValue(){
     value = 12; //将输出一个错误
  }
}
```

---


>final方法
* 父类的final方法可以被继承但是不能被重写。主要目的防止内容被修改
```java
public class Test{
    public final void changeName(){
       // 方法体
    }
}
```

---


>final类
* final类不能被继承，没有类能够继承final类的任何特性

---


### 2.3 abstract

>抽象类
* 抽象类不能用来实例化对象，声明抽象类的唯一目的是为了将来对该类进行扩充。
* 一个类不能同时被final与abstract修饰。
* 抽象方法只能出现在抽象类中。
* 抽象类可以包括抽象方法和非抽象方法。

---


>抽象方法
* 抽象方法是一种没有任何实现的方法，实现方法由子类实现。
* 不能被声明为final和static
* 子类必须为实现父类的所有抽象方法。

---


### 2.4 synchronized

* synchronized关键字声明的方法同一时间只能被一个线程访问。synchronized可以应用于四个访问修饰符。
```java
public synchronized void showDetails(){
//.......
}
```

---


### 2.5 transient

* 序列化的对象包含被transient修饰的实例对象时，java虚拟机会跳过该特定的变量。
* 该修饰符包含在定义变量的语句中，用来预处理类和变量的数据类型。
```java
public transient int limit = 55;   // 不会持久化
public int b; // 持久化
```

---


### 2.6 volatile

* volatile 修饰的成员变量在每次被线程访问时，都强制从共享内存中重新读取该成员变量的值。
* 当成员变量发生变化时，会强制线程将变化值回写到共享内存。这样在任何时刻，两个不同的线程总是看到某个成员变量的同一个值。
```java
public class MyRunnable implements Runnable
{
    private volatile boolean active;
    public void run()
    {
        active = true;
        while (active) // 第一行
        {
            // 代码
        }
    }
    public void stop()
    {
        active = false; // 第二行
    }
}
```
* 通常情况下，在一个线程调用 run() 方法（在 Runnable 开启的线程），在另一个线程调用 stop() 方法。 如果 第一行 中缓冲区的 active 值被使用，那么在 第二行 的 active 值为 false 时循环不会停止。
* 但是以上代码中我们使用了 volatile 修饰 active，所以该循环会停止。

---


