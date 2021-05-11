# Lua中的模块概念和元表

---

## 12> 模块与包--建立在table上

- 模块类似于一个封装库，从 Lua 5.1 开始，Lua 加入了标准的模块管理机制，可以把一些公用的代码放在一个文件里，以 API 接口的形式在其他地方调用，有利于代码的重用和降低代码耦合度。

- Lua 的模块是由变量、函数等已知元素组成的 table，因此创建一个模块很简单，就是创建一个 table，然后把需要导出的常量、函数放入其中，最后返回这个 table 就行。

---

### 12.1 定义一个module模块

```lua
--------------创建自定义模块 module.lua-----------------
-- 文件名为 module.lua
-- 定义一个名为 module 的模块
module = {}
 
-- 定义一个常量
module.constant = "这是一个常量"
 
-- 定义一个函数
function module.func1()
    io.write("这是一个公有函数！\n")
end
 
local function func2()
    print("这是一个私有函数！")
end
 
function module.func3()
    func2()
end
return module
-----func2在外部不能访问
```

---

### 12.2 外部访问require

- Lua提供了一个名为require的函数用来加载模块。要加载一个模块，只需要简单地调用就可以了。例如：  require("<模块名>")       或       require "<模块名>"

```lua
require("module")
print(module.constant)--这是一个常量
module.func3()----------这是一个私有函数！
```

- 也可以给加载的模块创建一个别名

```lua
-- 别名变量 m
local m = require("module") 
print(m.constant)
m.func3()
```

---

### 12.3 模块的加载机制

- 对于自定义的模块，模块文件不是放在哪个文件目录都行，函数 require 有它自己的文件路径加载策略，它会尝试从 Lua 文件或 C 程序库中加载模块。

- require 用于搜索 Lua 文件的路径是存放在全局变量 package.path 中，当 Lua 启动后，会以环境变量 LUA_PATH 的值来初始这个环境变量。**如果没有找到该环境变量，则使用一个编译时定义的默认路径来初始化。**

---

### 12.4 DLL包

- Lua和C是很容易结合的，使用 C 为 Lua 写包。

- 与Lua中写包不同，C包在使用以前必须首先加载并连接，在大多数系统中最容易的实现方式是通过动态连接库机制。

- Lua在一个叫loadlib的函数内提供了所有的动态连接的功能。这个函数有两个参数:

>**库的绝对路径和初始化函数**。

```lua
local path = "/usr/local/lua/lib/libluasocket.so"
local f = loadlib(path, "luaopen_socket")
```

- loadlib 函数加载指定的库并且连接到 Lua，然而它并不打开库（也就是说没有调用初始化函数），反之他返回初始化函数作为 Lua 的一个函数，这样我们就可以直接在Lua中调用他。
- 如果加载动态库或者查找初始化函数时出错，loadlib 将返回 nil 和错误信息。我们可以修改前面一段代码，使其检测错误然后调用初始化函数：

```lua
local path = "/usr/local/lua/lib/libluasocket.so"
-- 或者 path = "C:\\windows\\luasocket.dll"，这是 Window 平台下
local f = assert(loadlib(path, "luaopen_socket"))
f()  -- 真正打开库
```

---

## 13> 元表（Metatable）

- 在 Lua table 中我们可以访问对应的key来得到value值，但是却无法对两个 table 进行操作。因此 Lua 提供了元表(Metatable)，允许我们改变table的行为，每个行为关联了对应的元方法。

- 使用元表我们可以定义Lua如何计算两个table的相加操作a+b。

  1. 当Lua试图对两个表进行相加时，先检查两者之一是否有元表，
  2. 之后检查是否有一个叫"__add"的字段，若找到，则调用对应的值。
  3. "__add"等即时字段，其对应的值（往往是一个函数或是table）就是"元方法"。

---

### 13.1 setmetatable(table，metatable)

- setmetatable(table,metatable): 对指定 table 设置元表(metatable)，如果元表(metatable)中存在 __metatable 键值，setmetatable 会失败。

- getmetatable(table): 返回对象的元表(metatable)

```lua
-----setmetatable(table,metatable)//表示将metatable表，放到table中作为嵌套表
mytable = {}                          --子表
metat = {}       -- 元表
setmetatable(mytable,metat)  --第一个参数子表，第二个表为元表
```

---

### 13.2 元方法

- 当子表table执行一些特定操作时，会调用其元表中的特殊操作

- 当table子表做一些特殊操作时，调用特定元表中的元方法，当元方法中存在参数时，第一个参数默认为其子表自身，

---

#### 13.2.1 __tostring元方法

- 需要在元表中设置_tostring名称的函数，当**子表当作字符串使用时，会**调用其元表的__tostring方法

- 当元表的_tostring方法中参数是table，  会将自己作为第一个参数传入到元表对应的__tostring的函数中去，并调用其元表的_tostring方法，在方法中可以访问子表的元素

```lua
metat.__tostring = function()
          return "调用了tostring元方法"
      end
print（mytable）-------"调用了tostring元方法"
-------------------------------------
metat.__tostring = function(t)
          return t.name
      end
mytable.name = "Ychao"
print(mytable)--------"Ychao"
```

---

#### 13.2.2 __call元方法

- 当子表作为一个函数时被使用时，调用元表的_call 方法

```lua
metat.__call = function(a,b)
        print(a)----a是子表本身，作为字符串，又调用一次_tostring方法
        print(b)----b为 mytable（11）传入的参数
        return "调用了call元方法"
 end
 --------
 mytable（11）
 -----Ychao
 -----11
 -----调用了call元方法
```

---

#### 13.2.3 运算符重载

- 当多个子表进行+运算时，调用__add元方法

```lua
metat.__add = function(a,b)
      return a.age + b.age
 end
 mytable.age = 15
 table2 = {age = 16}
 print(mytable + table2)----31
```

- 运算符重载时，元素运算要求两个表中必须分别有对应同名的可以相加或者运算的类型。以返没有找到时将返回nil，调用元方法时会异常
- 其他运算符重载的方法

  - sub（-），mul（*），div（/），mod（%），pow（^），unm（负号），concat（ . . ），eq（==），lt（< ），le（<=）

- 其中算术运算符要求其中一个表是元表的一个子表。

- 进行逻辑运算时，要求两个均为调用元表的子表，当一个不是时，均返回false

### 13.2.4 __index和__newindex元方法

- 当子表中，找不到某一个属性或值时，会到元表中找到__index，在__index指定的表中去找索引，一般__index在元表外部赋值，指定备用表。

- 当多个表之间存在元表连接时，像C#类一样单一继承。各自指定index为其父表，可以将多个表串联起来，但父表没有找到索引时会调到父表的元表中继续查找

```lua
metat.__index = {score = 15}
print(mytable.score)----子表中没有score索引，但调用__index中找到score，返回15
```

- __newindex方法表示，当子表中赋值一个不存在索引的值时，会找父表__newindex指向的表中的对应索引，（父表不存在时，也可以指定关联父表的父表，并进行查找），此方法会修改掉找到索引的值。

---

#### 13.2.5 返回得到子表的元表

- getmetatable（table）返回该表对应的元表

---

#### 13.2.6 查找和修改自身指定索引

- rawget（table，" 索引名 "）---不会去查找其元表是否拥有此索引，会返回自身中查找的结果，存在返回值，不存在返回nil

- rawset（table，”索引名“，value）---修改自身索引下的值，而不会调用newindex元方法

---
