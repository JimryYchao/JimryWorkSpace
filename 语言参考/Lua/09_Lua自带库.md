# lua常用自带库（time，Math，package）

---

## 1> 字符串和表

- string 和 table

## 2> 时间

- 系统时间：os.time()，os.date("*t") 返回一个时间记录的表

os.date("*t").day-------系统的某日

- 自己传入参数，得到时间：os.time(year = 2014, month = 8, day = 14)

## 3> Math

```lua
math.abs(-999)-----绝对值
math.deg(math.pi)-----弧度转角度
math.cos(math.pi)-----三角函数传入弧度值
math.floor(1.9)------1//向下取整数
math.ceil(5.2)-------6//向上取整数
matn.max(154，5.2)-------------最大值
math.min(5.2，1)-------------最小值
math.modf(1.2)-------1   0.2 //小数分离
math.pow(2,5)--------幂运算，2^5
math.sqrt(4)---------2 //开方
---------随机数
math.randomseed(os.time())-------预先设置随机数种子
math.random(100)-----------这个会受到随机数中的影响
math.random(100)---------这个不受影响，随机值随机产生
```

## 4> package 路径

```lua
package.path-------返回lua脚本加载路径，string类型
```
