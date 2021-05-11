# Lua变量参考

---

## 前言

- Lua是一种轻量小巧的脚本语言，用标准C语言编写并以源代码形式开放， 其设计目的是为了嵌入应用程序中，从而为应用程序提供灵活的扩展和定制功能。

---

### 0.1 Lua特性

- 轻量级：它用标准C语言编写并以源代码形式开放，编译后仅仅一百余K，可以很方便的嵌入别的程序里。

- 可扩展：Lua提供了非常易于使用的扩展接口和机制：由宿主语言(通常是C或C++)提供这些功能，Lua可以使用它们，就像是本来就内置的功能一样。

- 其他特性：
  - 支持面向过程(procedure-oriented)编程和函数式编程(functional programming)；
  - 自动内存管理；只提供了一种通用类型的表（table），用它可以实现数组，哈希表，集合，对象；
  - 语言内置模式匹配；闭包(closure)；函数也可以看做一个值；提供多线程（协同进程，并非操作系统所支持的线程）支持；
  - 通过闭包和table可以很方便地支持面向对象编程所需要的一些关键机制，比如数据抽象，虚函数，继承和重载等。

---

### 0.2 Lua的应用场景

- 游戏开发
- 独立应用脚本
- Web 应用脚本
- 扩展和数据库插件如：MySQL Proxy 和 MySQL WorkBench
- 安全系统，如入侵检测系统

---

## 1> 基本语法

### 1.1 注释

- 单行注释：（  --  ）

- 多行注释：（--[[     ]]--），（ --[[    ]]  ）,（ --[[     --]] ）

- 推荐的多行注释：    --[=[   注释内容   ]=]--

---

### 1.2 标识符原则

- 与C#相同的标识符原理，但是有一些区别：

最好不要使用下划线加大写字母的标示符，因为Lua的保留字也是这样的。在Lua中，是区分大小写的。

---

### 1.3 关键字

|and|break|do|else|elseif|end|
|:----|:----|:----|:----|:----|:----|
|false|for|function|if|in|local|
|nil|not|or|repeat|return|then|
|true|until|while|goto|    |    |

- 一般约定，以下划线开头连接一串大写字母的名字（比如 _VERSION）被保留用于 Lua 内置全局变量。

---

### 1.4 全局变量

- 在默认情况下，变量总是认为是全局的。全局变量不需要声明，给一个变量赋值后即创建了这个全局变量，访问一个没有初始化的全局变量也不会出错，只不过得到的结果是：nil。

```lua
print(b)--nil
b=10
print(b)--0
```

- 如果你想删除一个全局变量，只需要将变量赋值为nil。

```lua
b = nil
print(b)      --nil
```

---

## 2> Lua数据类型

- Lua 是动态类型语言，变量不要类型定义，只需要为变量赋值。 值可以存储在变量中，作为参数传递或结果返回。

---

### 2.1 8种基本类型

- Lua 中有 8 个基本类型分别为：nil、boolean、number、string、userdata、function、thread 和 table。

|nil|只有值nil属于该类，表示一个无效值（在条件表达式中相当于false）。|
|:----|:----|
|boolean|包含两个值：false和true。|
|number|表示双精度类型的实浮点数（有效数字15到16位）|
|string|字符串由一对双引号或单引号来表示|
|function|由 C 或 Lua 编写的函数|
|userdata|表示任意存储在变量中的C数据结构|
|thread|表示执行的独立线路，用于执行协同程序|
|table|Lua 中的表（table）其实是一个"关联数组"（associative arrays），数组的索引可以是数字、字符串或表类型。在 Lua 里，table 的创建是通过"构造表达式"来完成，最简单构造表达式是{}，用来创建一个空表。|

---

### 2.2 获取变量的类型

- type(变量) 返回该变量的变量类型的字符串。

```lua
print(type("Hello world"))      --> string
print(type(10.4*3))             --> number
print(type(print))              --> function
print(type(type))               --> function
print(type(true))               --> boolean
print(type(nil))                --> nil
print(type(type(X)))            --> string
```

---

### 2.3 nil类型(空)

- nil 类型表示一种没有任何有效值，它只有一个值 -- nil，例如打印一个没有赋值的变量，便会输出一个 nil 值。

- 对于全局变量和 table，nil 还有一个"删除"作用，给全局变量或者 table 表里的变量赋一个 nil 值，等同于把它们删掉。

```lua
tab1 = { key1 = "val1", key2 = "val2", "val3" }
for k, v in pairs(tab1) do
    print(k .. " - " .. v)  --<1 - val3><key1 - val1><key2 - val2>
end
----------------------------------
tab1.key1 = nil
for k, v in pairs(tab1) do
    print(k .. " - " .. v)  --<1 - val3><key2 - val2>//key1对被删除
end
```

- 测试一个变量是否为空

```lua
type(X)--nil
type(X)==nil--false//类型不同返回false
type(X)=="nil"--true//字符串值相同返回true
--[=[ 由于type返回字符串类型，所以需要用"nil"用来测试而不是nil ]=]--
```

---

### 2.4 boolean(布尔)

- 在Lua中，**boolean**类型判定**nil**和**false**为**false**，而把**true**和**0**判定为**true**（其他数字也为true）

```lua
print(type(true))--boolean-
print(type(false))--boolean
print(type(nil))--nil
-----------------------------------------------
if false or nil then
    print("至少有一个是 true")
else
    print("false 和 nil 都为 false")false 和 nil 都为 false--
end
------------------------------------------------
if 0 then
    print("数字 0 是 true")--数字 0 是 true
else
    print("数字 0 为 false")
end
```

---

### 2.5 number(数字)

- Lua 默认只有一种 number 类型 -- double（双精度）类型（默认类型可以修改 luaconf.h 里的定义）

```lua
print(type(2))
print(type(2.2))
print(type(0.2))
print(type(2e+1))
print(type(0.2e-1))
print(type(7.8263692594256e-06))
```

---

### 2.6 string(字符串)

- 字符串由一对双引号或单引号来表示。

```lua
string1 = "this is string1"
string2 = 'this is string2'
```

- [[ ]] 来表示多行字符串

```lua
print("haha\nhaha")
print([[这是第一行
这是第二行
这是第三行]])   
```

- 对一个数字字符串上进行算数操作时，Lua 会尝试将这个数字字符串转成一个数字:

```lua
print("2" + 6)--8
print("2" + "6")--8
print("2 + 6")--2 + 6
print("-2e2" * "6")--   -1200
print("error" + 1)--//error：转换error为number失败
```

- （ .. ）用来拼接字符串或者变量成为新字符串，不支持boolean，nil类型与字符串拼接

```lua
print("haha".."huohuo")
n1 = 123
n2 = 321
print(n1..n2)
```

- （ % ）%+特殊字母表示拼接特定的类型，使用string.format方法
  - %d：与数字拼接

  - %a：与任何字符拼接

  - %s：与字符配对...等

```lua
print(string.format("大家好,我今年%d岁",25))--大家好,我今年25岁
```

- 使用（#）获取str的长度，其中汉字占3个长度，英文字符占一个长度

```lua
s = "abcd"
print(#s)--4
s = "字符串"
print(#s)--9
```

---

### 2.7 table(表)

- 在 Lua 里，table 的创建是通过"构造表达式"来完成，最简单构造表达式是{}，用来创建一个空表。也可以在表里添加一些数据，直接初始化表。

- Lua是从1开始排序的，因此，使用pairs遍历打印输出时，会先按照顺序输出表的值，然后再按照键值对的键的哈希值打印。

```lua
-- 创建一个空的 table
local tbl1 = {}
 
-- 直接初始表
local tbl2 = {"apple", "pear", "orange", "grape"}
```

- Lua 中的表（table）其实是一个"关联数组"（associative arrays），数组的索引可以是数字或者是字符串。

```lua
tabl = {a =1,b = 2,[1]=666}
```

>可以在表中像声明变量一样配置表的键值对，但是数字索引需要用中括号括起来。

```lua
a = {}
a["key"] = "value"
----------------此Key非彼Key，此处Key是一个number类型，上面“key”是字符串key
key = 10
a[key] = 22
a[key] = a[key] + 11
for k, v in pairs(a) do
    print(k .. " : " .. v)--<key : value> <10 : 33>
end
```

- 不同于其他语言的数组把 0 作为数组的初始索引，在 Lua 里表的默认初始索引一般以 1 开始。

```lua
local tbl = {"apple", "pear", "orange", "grape"}
for key, val in pairs(tbl) do
    print("Key", key)--1，2，3，4
end
```

- table 不会固定长度大小，有新数据添加时 table 长度会自动增长，没初始的 table 都是 nil。

```lua
a3 = {}
for i = 1, 10 do
    a3[i] = i
end
a3["key"] = "val"
print(a3["key"])--val
print(a3["none"])--nil
```

---

### 2.8 function(函数)

- 在 Lua 中，函数是被看作是"第一类值（First-Class Value）"，函数可以存在变量里。

```lua
function factorial1(n)
    if n == 0 then
        return 1
    else
        return n * factorial1(n - 1)--5*4*3*2*1//递归算法
    end
end
print(factorial1(5))--120
factorial2 = factorial1
print(factorial2(5))--120
```

- 当函数作为变量名传入函数时，作为实参使用匿名写法，作为匿名函数传入

```lua
function testFun(tab,fun)
        for k ,v in pairs(tab) do
                print(fun(k,v));
        end
end
--------------------------------
tab={key1="val1",key2="val2"};
testFun(tab,
        function(key,val)--匿名函数
              return key.."="..val;
        end);
```

---

### 2.9 thread(线程)

- 在 Lua 里，最主要的线程是协同程序（coroutine）。它跟线程（thread）差不多，拥有自己独立的栈、局部变量和指令指针，可以跟其他协同程序共享全局变量和其他大部分东西。

- 线程跟协程的区别：线程可以同时多个运行，而协程任意时刻只能运行一个，并且处于运行状态的协程只有被挂起（suspend）时才会暂停。

---

### 2.10 uesrdata(自定义类型)

- userdata 是一种用户自定义数据，用于表示一种由应用程序或 C/C++ 语言库所创建的类型，可以将任意 C/C++ 的任意数据类型的数据（通常是 struct 和 指针）存储到 Lua 变量中调用。

---

## 3> Lua 变量

- Lua 变量有三种类型：全局变量、局部变量、表中的域

- Lua 中的变量全是全局变量，那怕是语句块或是函数里，除非用 local 显式声明为局部变量。

- 局部变量的作用域为从声明位置开始到所在语句块结束。即遇到end时将在下面的语句中恢复默认值，end的作用为将当前语句块结束，并将语句块中的局部变量转换为默认值nil。

- 变量的默认值均为 nil。

```lua
a = 5               -- 全局变量
local b = 5         -- 局部变量
function joke()
    c = 5           -- 全局变量
    local d = 6     -- 局部变量
end
joke()
print(c,d)          --> 5 nil
do
    local a = 6     -- 局部变量
    b = 6           -- 对局部变量重新赋值
    print(a,b);     --> 6 6
end
print(a,b)      --> 5 6
```

---

### 3.1 赋值语句

- 赋值是改变一个变量的值和改变表域的最基本的方法。

- Lua 可以对多个变量同时赋值，变量列表和值列表的各个元素用逗号分开，赋值语句右边的值会依次赋给左边的变量。

```lua
a = "hello" .. "world"
t.n = t.n + 1
a, b = 10, 2*x       -->       a=10; b=2*x
```

- 交换数值：遇到赋值语句Lua会先计算右边所有的值然后再执行赋值操作，所以我们可以这样进行交换变量的值。

```lua
x, y = y, x                     -- swap 'x' for 'y'
a[i], a[j] = a[j], a[i]         -- swap 'a[i]' for 'a[j]'
```

- 当变量个数和值的个数不一致时，Lua会一直以变量个数为基础采取以下策略：
  - 当变量个数 > 值的个数，按变量个数补足nil。
  - 当变量个数 < 值的个数，多余的值会被忽略。

**注意：如果要对多个变量赋值必须依次对每个变量赋值。**

```lua
a, b, c = 0, 1
print(a,b,c)             --> 0   1   nil
------------------------------------------------------------
a, b = a+1, b+1, b+2     -- value of b+2 is ignored
print(a,b)               --> 1   2
------------------------------------------------------------
a, b, c = 0
print(a,b,c)             --> 0   nil   nil
```

- 多值赋值经常用来交换变量，或将函数调用返回给变量：

```lua
a, b = f()
```

- 应该尽可能的使用局部变量，有两个好处：
  - 避免命名冲突。
  - 访问局部变量的速度比全局变量更快。

---

### 3.2 索引

- [ ]  ：对于表和字符串可以使用[] 进行元素访问。

- table还可以点出元素。

```lua
t[i]
t.i                 -- 当索引为字符串类型时的一种简化写法
gettable_event(t,i) -- 采用索引访问本质上是一个类似这样的函数调用
```

---

## 4> 循环Loop

- 循环结构是在一定条件下反复执行某段程序的流程结构，被反复执行的程序被称为循环体。循环语句是由循环体及循环的终止条件两部分组成的。

---

### 4.1 While循环

- Lua 编程语言中 while 循环语句在判断条件为 true 时会重复执行循环体语句。

```lua
while <condition> do
    statements<循环体语句>
end
--[=[ statements(循环体语句) 可以是一条或多条语句，condition(条件) 
可以是任意表达式，在 condition(条件) 为 true 时执行循环体语句。    ]=]--
a=10
while( a < 20 )
do
   print("a 的值为:", a)
   a = a+1
end
```

---

### 4.2 For循环

- Lua 编程语言中 for 循环语句可以重复执行指定语句，重复次数可在 for 语句中控制。

- Lua 编程语言中 for 语句有两大类：**数值for循环**和**泛型for循环**

---

#### 4.21 数值for循环

```lua
for var=exp1,exp2,exp3 do  
    <执行体>  
end  
--var 从 exp1 变化到 exp2，每次变化以 exp3 为步长递增 var，
--并执行一次 "执行体"。exp3 是可选的，如果不指定，默认为1。
for i=10,1,-1 do   --递减1，循环范围10~1
    print(i)
end
--[[  for i=1,f(x) do

print(i)

end        ]]--
```

>for的三个表达式在循环开始前一次性求值，以后不再进行求值。比如上面的f(x)只会在循环开始前执行一次，其结果用在后面的循环中。

```lua
function f(x)  
    print("function")  
    return x*2  
end  
for i=1,f(5) do print(i)  --先计算f(5)=10,然后确定循环范围1~10
end
```

---

#### 4.22 泛型for循环

- 泛型 for 循环通过一个迭代器函数来遍历所有值，类似C#中的 foreach 语句。

- Lua 编程语言中泛型 for 循环语法格式：

```lua
a = {"one", "two", "three"}
for i, v in ipairs(a) do 
--i是数组索引值，v是对应索引的数组元素值。ipairs是Lua提供的一个迭代器函数，用来迭代数组。
    print(i, v)
end 
---------------------------------------------------
days = {"Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"}  
for i,v in ipairs(days) do  print(v) end  --按照数组的索引依次遍历，从1开始
```

---

### 4.3 Repeat...until循环

- Lua 编程语言中 repeat...until 循环语句不同于 for 和 while循环，for 和 while 循环的条件语句在当前循环执行开始时判断，而 repeat...until 循环的条件语句在当前循环结束后判断。

```lua
repeat
   statements
until( condition )
```

- 循环条件判断语句（condition）在循环体末尾部分，所以在条件进行判断前循环体都会执行一次。
- 如果条件判断语句（condition）为false，循环会重新开始执行，直到条件判断语句（condition）为 true 才会停止执行。

```lua
--[ 变量定义 --]
a = 10
--[ 执行循环 --]
repeat
   print("a的值为:", a)
   a = a + 1
until( a > 15 )
```

---

### 4.4 嵌套循环

- Lua 编程语言中允许循环中嵌入循环。

---

## 5> Lua IF条件控制

- Lua 编程语言流程控制语句通过程序设定一个或多个条件语句来设定。在条件为 true 时执行指定程序代码，在条件为 false 时执行其他指定代码。

- 控制结构的条件表达式结果可以是任何值，**Lua认为false和nil为假，true和非nil为真。**

- Lua的 If 条件语句需要 End 结尾，语法结构由if，then，else，elseif，end组成。

- Lua中没有内置的switch语句，需要自己实现。

```lua
--[[if语法:
  if 条件语句 then //if~then标准格式,必须要end结尾
  执行区域
  end
]]--
--单分支if
a = 9
if a > 5 then
  print("666")--666
end
--双分支if
if a < 5 then
  print("不满足")
else
  print("满足")--满足
--多分支
a = 5
if a < 5 then
  print("666")
elseif a > 5 then
  print("666666666")
else
  print("other")--other
end
```

- Lua if 语句允许嵌套, 这就意味着你可以在一个 if 或 else if 语句中插入其他的 if 或 else if 语句。

---

## 6> 函数  function

- 在Lua中，函数是对语句和表达式进行抽象的主要方法。既可以用来处理一些特殊的工作，也可以用来计算一些值。

- Lua 提供了许多的内置函数，你可以很方便的在程序中调用它们，如**print()**函数可以将传入的参数打印在控制台上。

- Lua 函数主要有两种用途：

1.完成指定的任务，这种情况下函数作为调用语句使用；

2.计算并返回值，这种情况下函数作为赋值语句的表达式使用。

```lua
<local> function function_name( argument1, argument2, argument3..., argumentn)--参数列表，可选全局或局部函数
    function_body--方法体
    return result_params--可以返回多个值，或无返回值
end
```

- 函数可以作为参数传入另一个函数中。

```lua
myprint = function(param)
print("这是打印函数 -   ##",param,"##")
end
function add(num1,num2,functionPrint)
result = num1 + num2
-- 调用传递的函数参数
functionPrint(result)
end
myprint(10)
-- myprint 函数作为参数传递
add(2,5,myprint)
```

---

### 6.1 可变参数（ . . . ）

- Lua 函数可以接受可变数目的参数，和 C 语言类似，在函数参数列表中使用三点 ... 表示函数有可变的参数。

```lua
function add(...)  
local s = 0  
  for i, v in ipairs{...} do   --> {...} 表示一个由所有变长参数构成的数组  
    s = s + v  
  end  
  return s  
end  
print(add(3,4,5,6,7))  --->25
```

---

### 6.11 通过{...}遍历可变参数

- 当可变参数符作为函数的参数时，内部需要声明一个table去接收。

```lua
function average(...)
   result = 0
   local arg={...}    --> arg 为一个表，局部变量
   for i,v in ipairs(arg) do
      result = result + v
   end
   print("总共传入 " .. #arg .. " 个数")
   return result/#arg
end
print("平均值为",average(10,5,3,4,5,6))--5.5
```

- 有时候我们可能需要几个固定参数加上可变参数，固定参数必须放在变长参数之前:

```lua
function fwrite(fmt, ...)  ---> 固定的参数fmt
    return io.write(string.format(fmt, ...))    
end
fwrite("runoob\n")--runoob--->fmt = "runoob", 没有变长参数。  
fwrite("%d%d\n", 1, 2)--12--->fmt = "%d%d", 变长参数为 1 和 2
```

---

### 6.2 通过select函数访问可变参数

- 通常在遍历变长参数的时候只需要使用 {…}，然而变长参数可能会包含一些 nil，那么就可以用 select 函数来访问变长参数了：select('#', …) 或者 select(n, …)

- select('#', …)   返回可变参数的长度

- select(n, …)     用于返回 n 到 select('#',…) 的参数，即n之后的参数

- 调用 select 时，必须传入一个固定实参 selector(选择开关) 和一系列变长参数。如果 selector 为数字 n，那么 select 返回 n 后所有的参数，否则只能为字符串 #，这样 select 返回变长参数的总数。

```lua
do  
    function foo(...)  
        for i = 1, select('#', ...) do  -->获取参数总数
            --select获取到传入的可变参数的数目
            local arg = select(i, ...); -->读取参数
            --选择出位于第i个位置之后的可变参数
            print("arg", arg);  
        end  
    end  
 
    foo(1, 2, 3, 4);  
end
```

### 6.3 函数嵌套与闭包

- 闭包原则：**闭包就是能够读取其他函数内部变量的函数**。例如在javascript中，只有函数内部的子函数才能读取**局部变量**，所以闭包可以理解成“**定义在一函数内部的函数**“。在本质上，**闭包是将函数内部和函数外部连接起来的桥梁**。

```lua
------------------------
function F8()
  return function()
  print(123)
  end
end
F = F8()
F()--123
------------------------
--Lua的闭包
function F9(x)
  --改变函数的生命周期
  return function(y)
  return x+y
  end
end
F10 = F9(15)
--[[
返回的函数类型会把F函数的临时变量存储起来，
从而改变了传入的15的生命周期。
]]
            
print(F10(5))-- 15+5 = 20
------------------------
```

---

## 7> 运算符

---

### 7.1 算术运算符

- 用于number或数字字符串：

```lua
a = 21
b = 10
c = a + b-------------------------加法
print("Line 1 - c 的值为 ", c )
c = a - b-------------------------减法
print("Line 2 - c 的值为 ", c )
c = a * b-------------------------乘法
print("Line 3 - c 的值为 ", c )
c = a / b-------------------------除法
print("Line 4 - c 的值为 ", c )
c = a % b-------------------------取余
print("Line 5 - c 的值为 ", c )
c = a^2 -------------------------幂运算
print("Line 6 - c 的值为 ", c )
c = -a   -------------------------符号运算
print("Line 7 - c 的值为 ", c )
```

---

### 7.2 条件(关系)运算符

- (<，<=，>，>=，==，\~=)：与C#不同的是**不等于的格式是\~=**

```lua
-- > < >= <= == ~=(不等于)
print(3>1)
print(3>=1)
print(3<1)
print(3<=1)
print(3==1)
print(3~=1)--不等于
```

---

### 7.3 逻辑运算符

- 逻辑与（and），逻辑或（or），逻辑非（not）。

- 在Lua中的逻辑运算符也支持短路原则，左操作数不满足,则右操作数不执行。

```lua
-- and(&&) or(||) not(!)
print(true and false)--false
print(false or true)--true
print(not true)--false
-- Lua的逻辑运算支持短路,左操作数不满足,则右操作数不执行
print(false and print("123"))--<false>//print("123被短路")，并不执行
print(true and print("123"))--false ;返回 123  nil//(print()方法的返回值是nil）
```

---

### 7.4 其他运算符

- #：表示获取表或者字符串的长度

- ..：字符串拼接运算，不支持boolean，nil拼接

---

### 7.5 Lua不支持的运算符

- Lua不支持位运算和三目运算符。但是用户可以自己实现。

---

### 7.6 运算符的优先级

- (^) > (not) > (负号) > (*，/，%) > (+，-) > ( . . 拼接符) > (关系运算符) > ( and ) > ( or )

---
