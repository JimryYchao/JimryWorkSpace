# Java数组

* Java 语言中提供的数组是用来存储固定大小的同类型元素。
* 
---


## 1> 声明数组

```java
dataType[] arrayRefVar;   // 首选的方法

或

dataType arrayRefVar[];  // 效果相同，但不是首选方法
double[] myList;         // 首选的方法
或
double myList[];         //  效果相同，但不是首选方法
```

---


## 2> 创建数组

* Java语言使用new操作符来创建数组
```java
dataType[] arrayRefVar = new dataType[arraySize];
dataType[] arrayRefVar = {value0, value1, ..., valuek};
```
* 数组的索引从0开始

---


## 3> 遍历数组

* For
```java
      double[] myList = {1.9, 2.9, 3.4, 3.5};
 
      // 打印所有数组元素
      for (int i = 0; i < myList.length; i++) {
         System.out.println(myList[i] + " ");
      }
```
* For-Each（For增强循环）
```java
for(type element: array)
{
    System.out.println(element);
}
public class TestArray {
   public static void main(String[] args) {
      double[] myList = {1.9, 2.9, 3.4, 3.5};
 
      // 打印所有数组元素
      for (double element: myList) {
         System.out.println(element);
      }
   }
}
```

---


## 4> 数组和函数

* 数组可以作为参数传递给方法。
* 同样方法可以返回数组类型。

---


## 5> 多维数组

```java
String str[][] = new String[3][4];
```
* 与C#的交错数组相似，但Java没有arr[ , ]的格式数组表示二维数组等。
```java
type[][] typeName = new type[typeLength1][typeLength2];
```
* Java多维数组的声明可以为每一维分配空间，即直接确定typeLength1和typeLength1的大小，表示几行几列。
* 也可以分步分配，分别为第0维度的各个数组元素各自声明下一维度的空间。
```java
String s[][] = new String[2][];
s[0] = new String[2];// 0--2
s[1] = new String[3];// 0--3
s[0][0] = new String("Good");
s[0][1] = new String("Luck");
s[1][0] = new String("to");
s[1][1] = new String("you");
s[1][2] = new String("!");
```
* 数组的读取通过索引引用arr[a][b],第a行第b列，均从0开始

---


## 6> Java 的Array类

>java.util.Arrays 类能方便地操作数组，它提供的所有方法都是静态的。

* 具有以下功能：
    * 给数组赋值：通过 fill 方法。
    * 对数组排序：通过 sort 方法,按升序。
    * 比较数组：通过 equals 方法比较数组中元素值是否相等。
    * 查找数组元素：通过 binarySearch 方法能对排序好的数组进行二分查找法操作。
    * 
|序|方法和说明 |
|:----|:----|
|1|**public static int binarySearch(Object[] a, Object key)**<br>用二分查找算法在给定数组中搜索给定值的对象(Byte,Int,double等)。数组在调用前必须排序好的。如果查找值包含在数组中，则返回搜索键的索引；否则返回 (-(*插入点*) - 1)。|
|2|**public static boolean equals(long[] a, long[] a2)**<br>如果两个指定的 long 型数组彼此*相等*，则返回 true。如果两个数组包含相同数量的元素，并且两个数组中的所有相应元素对都是相等的，则认为这两个数组是相等的。换句话说，如果两个数组以相同顺序包含相同的元素，则两个数组是相等的。同样的方法适用于所有的其他基本数据类型（Byte，short，Int等）。|
|3|**public static void fill(int[] a, int val)**<br>将指定的 int 值分配给指定 int 型数组指定范围中的每个元素。同样的方法适用于所有的其他基本数据类型（Byte，short，Int等）。|
|4|**public static void sort(Object[] a)**<br>对指定对象数组根据其元素的自然顺序进行升序排列。同样的方法适用于所有的其他基本数据类型（Byte，short，Int等）。|


---


