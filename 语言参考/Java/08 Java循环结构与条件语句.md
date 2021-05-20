# Java 循环结构


---


## 1> while

```java
while( 布尔表达式 ) {
  //循环内容
}
```
>只要布尔表达式为 true，循环就会一直执行下去。
```java
public class Test {
   public static void main(String args[]) {
      int x = 10;
      while( x < 20 ) {
         System.out.print("value of x : " + x );
         x++;
         System.out.print("\n");
      }
   }
}
```

---


## 2> do...while

```java
do {
       //代码语句
}while(布尔表达式);
```
>对于 while 语句而言，如果不满足条件，则不能进入循环。但有时候我们需要即使不满足条件，也至少执行一次。
```java
public class Test {
   public static void main(String args[]){
      int x = 10;
 
      do{
         System.out.print("value of x : " + x );
         x++;
         System.out.print("\n");
      }while( x < 20 );
   }
}
```

---


## 3> for

* for 循环执行的次数是在执行前就确定的。
```java
for(初始化; 布尔表达式; 更新) {
    //代码语句
}
```
>关于 for 循环有以下几点说明：
* 最先执行初始化步骤。可以声明一种类型，但可初始化一个或多个循环控制变量，也可以是空语句。
* 然后，检测布尔表达式的值。如果为 true，循环体被执行。如果为 false，循环终止，开始执行循环体后面的语句。
* 执行一次循环后，更新循环控制变量。
* 再次检测布尔表达式。循环执行上面的过程。
```java
public class Test {
   public static void main(String args[]) {
 
      for(int x = 10; x < 20; x = x+1) {
         System.out.print("value of x : " + x );
         System.out.print("\n");
      }
   }
}
```

---


## 4> 增强 For 循环

* java5 以上
```java
for(声明语句 : 表达式)
{
   //代码句子
}
```
* **声明语句**：声明新的局部变量，该变量的类型必须和数组元素的类型匹配。其作用域限定在循环语句块，其值与此时数组元素的值相等。
* **表达式**：表达式是要访问的数组名，或者是返回值为数组的方法。
```java
public class Test {
   public static void main(String args[]){
      int [] numbers = {10, 20, 30, 40, 50};
 
      for(int x : numbers ){
         System.out.print( x );
         System.out.print(",");
      }
      System.out.print("\n");
      String [] names ={"James", "Larry", "Tom", "Lacy"};
      for( String name : names ) {
         System.out.print( name );
         System.out.print(",");
      }
   }
}
```

---


## 5> break 关键字

* break 主要用在循环语句或者 switch 语句中，用来跳出整个语句块。
* break 跳出最里层的循环，并且继续执行该循环下面的语句
```java
public class Test {
public static void main(String args[]) {
int [] numbers = {10, 20, 30, 40, 50};

for(int x : numbers ) {
// x 等于 30 时跳出循环
if( x == 30 ) {
break;
}
System.out.print( x );
System.out.print("\n");
}
}
}
```

---


## 6> continue 关键字

* continue 适用于任何循环控制结构中。作用是让程序立刻跳转到下一次循环的迭代。
* 在 for 循环中，continue 语句使程序立即跳转到更新语句。
* 在 while 或者 do…while 循环中，程序立即跳转到布尔表达式的判断语句。

---


# Java 条件语句


---


## 7> if...else if...else

```java
  public class Test {
    public static void main(String args[]) {
      int x = 30;

      if (x == 10) {
        System.out.print("Value of X is 10");
      } else if (x == 20) {
        System.out.print("Value of X is 20");
      } else if (x == 30) {
        System.out.print("Value of X is 30");
      } else {
        System.out.print("这是 else 语句");
      }
    }
  }
```

---


## 8> switch  case

* switch case 语句判断一个变量与一系列值中某个值是否相等，每个值称为一个分支。
```java
switch(expression){
    case value :
       //语句
       break; //可选
    case value :
       //语句
       break; //可选
    //你可以有任意数量的 case 语句
    default : //可选
       //语句
}
```
>switch case 语句有如下规则：
* switch 语句中的变量类型可以是：byte、short、int 或者 char。从 Java SE 7 开始，switch 支持字符串 String 类型了，同时 case 标签必须为字符串常量或字面量。
* switch 语句可以拥有多个 case 语句。每个 case 后面跟一个要比较的值和冒号。
* case 语句中的值的数据类型必须与变量的数据类型相同，而且只能是常量或者字面常量。
* 当变量的值与 case 语句的值相等时，那么 case 语句之后的语句开始执行，直到 break 语句出现才会跳出 switch 语句。
* 当遇到 break 语句时，switch 语句终止。程序跳转到 switch 语句后面的语句执行。case 语句不必须要包含 break 语句。如果没有 break 语句出现，程序会继续执行下一条 case 语句，直到出现 break 语句。
* switch 语句可以包含一个 default 分支，该分支一般是 switch 语句的最后一个分支（可以在任何位置，但建议在最后一个）。default 在没有 case 语句的值和变量值相等的时候执行。default 分支不需要 break 语句。
* 不同于 C#，java 允许贯穿
>**switch case 执行时，一定会先进行匹配，匹配成功返回当前 case 的值，再根据是否有 break，判断是否继续输出，或是跳出判断**
```java
public class Test {
   public static void main(String args[]){
      //char grade = args[0].charAt(0);
      char grade = 'C';
 
      switch(grade)
      {
         case 'A' :
            System.out.println("优秀"); 
            break;
         case 'B' :
         case 'C' :
            System.out.println("良好");
            break;
         case 'D' :
            System.out.println("及格");
            break;
         case 'F' :
            System.out.println("你需要再努力努力");
            break;
         default :
            System.out.println("未知等级");
      }
      System.out.println("你的等级是 " + grade);
   }
}
```

---




