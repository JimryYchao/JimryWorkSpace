# Lua的require和lua特殊语法

---

## 14> 全局变量和局部变量

- 在Lua中的声明的变量没有修饰符时都是全局变量，用 local 修饰的变量是局部变量，在局部空间内有效。

---

## 15> 多脚本执行Require

- require（”脚本名“），当申请脚本调用时，就会执行其他脚本中的内容。

```lua
require("脚本名")
require '脚本名'
--当require调用脚本成功时，就可以调用该脚本的全局变量和方法，
```

- require加载过一次该脚本后再次加载也不会执行该脚本输出内容，可以通过测试查看该脚本是否被加载。

```lua
package.loaded["脚本名"] 
--返回一个boolean，当为true时表示该脚本已被成功加载过。
```

- 卸载require再执行

```lua
package.loaded["脚本名"]  = nil
require '脚本名'
```

---

## 16> Lua中的大G表

- （ _G ）G表是一个总表（table类型），它将我们声明的所有全局变量都存储在_G表中

```lua
for k,v in pairs(_G) do
    print(k,v)
 end
```

- 本地局部变量，不会存储在_G表中。

---

## 17> return的特殊用法

- 在一个Lua文本中，可以用在文件代码的最后一行return 变量名，将该Lua文件的一个变量返回到全局，可以将本地变量返回到**require方法中，当require调用加载其他Lua文件时，用一个变量接收require方法的返回值。**

```lua
--Test1.Lua
local a = 666
print("hahahah")
return a
-------------------------------------------
--Test2.Lua
local num = require("Test1")---hahahah
print(num )----666
```

---

## 18> and 和 or 的特殊用法

- 在lua中进行条件判断时，nil和false返回false，而非nil值和true返回true

- and和or和C#的&&，||都有短路的性质，因此，使用此性质可以有一些特殊用法

```lua
print(1 and 0)-----0//true and true ，返回第二个变量
print(nil and 1)---nil//false 短路and，返回第一个变量，第二个不执行
print(1 or 0)------1//true短路or，返回第一个变量
print(nil or 1)----1//false or true 返回第二个元素
```

>**执行右侧操作数就返回右侧，不执行就返回左侧**

- 利用这种特质创造lua三目运算符

```lua
local res = (a>b)and a or b
---当a>b and a ---true  --> a
      ---a or b ---true 短路b，返回 a
---当a>b and a ---false --> a>b
      ---a>b or b ---右侧true ，返回 b
```

## 19> goto 一般在for循环种进行标签跳跃

```lua
---定义标签
::finish::##这里有错误

```

---
