# lua实现简单的面向对象

---

## 1> 面向对象--封装

- Lua中的面向对象都是基于table来实现的，table像是C#中的Static类

---

### 1.1 Lua封装一个对象

```lua
object= {}
object.id = 1
function object:Test()
      print(self.id)
 end
function object:new()----冒号表示将表自身作为参数传入方法中。
     -----self 代表的是，我们默认传入的第一个参数
     -----对象就是变量，返回一个新的变量
     local obj = {} 
     ----利用元表的关联，将新表和父表联系起来，子表就可以利用元方法调用父表的方法
     setmetatable(obj,self)
     self.__index = self---------关联索引
    
     return obj
end
--------------------------------------
```

---

### 1.2 创建一个对象

```lua
local myobj = object:new()
print(myobj.id)--------查找父表，id = 1
myobj:Test(----将自己调入到Test方法中，子表没有Test索引，进入到父表中调用Test，并把自己传入。
myobj.id  = 2------创建自己表的成员。
myobj：Test()------得到自己的id = 2 
```

---

## 2> 面向对象--继承

---

### 2.1 创建父表的继承方法

```lua
function object:subClass(className)
       --利用_G总表的所有的全局变量，通过键值对应的方式存储
      _G[className] = {} --在全局创建一个新的表
      ---继承的相关规则需要用到元表
      local obj = _G[className]
      self.__index = self
      setmetatable(obj,self)
 end
 ------------------------------------
```

---

### 2.2 声明一个继承表

```lua
object:subclass("NextClass")
NextClass.id = 1-------已关联
---------------------------------------------
--创建一个继承表的对象
local p = NextClass:new()---------调用父类的new方法创建NextClass的对象，实质上是将p表关联到NextClass表，而表NextClass关联object表，
```

## 3> 面向对象--多态

### 3.1 多态的实现

- 相同行为，不同表现，就是多态，形同方法，不同执行逻辑，就是多态

- 先创建一个表继承基表

```lua
object:subclass("GameObject")
GameObject.posX = 0
GameObject.posY = 0
function GameObject:Move()
  self.posX = self.posX+1
  self.posY = self.posY+1
  print(self.posX)
  print(self.posY)
end
          
```

- 继承父类的方法需要构建一个base.方法名的方法保留父类中的同名方法实现重写
回到object表中构建同名方法关联

```lua
function object:subclass(className)
       --利用_G总表的所有的全局变量，通过键值对应的方式存储
      _G[className] = {} --在全局创建一个新的表
      ---继承的相关规则需要用到元表
      local obj = _G[className]
      self.__index = self
      setmetatable(obj,self)
      --------------------------------------------
      --子类：定义一个base属性，base属性代表父类
      obj.base = sel------自定义base关联到父类      
 end
```

- 创建该表的一个继承表和优化实现多态

```lua
GameObject:sunclass("Player")
function Player:Move()
    --self.base:Move()----调用Move方法时，会返回到GameObject中调用父表的move方法，这里存在一个错误，要实现多态，我们需要让Player的各个对象的行为呈现多态性，但是调用move方法时，将基表的GameObject自身传入到了Move，再每次调用p1,p2时，实质上是在更改父表中的posX和posY，多个继承表是引用的方式调用Move方法，并没有创造自身的posX、posY,此时需要进行优化。
    self.base.Move(self)
    -----将Player的实例自身传入到GameObject的move方法中,会将父基的变量赋值给子表对象，因此调用方法时，不会多个对象修改同一个引用。
    --[=[  重写父类方法或者补充的逻辑语句  ]=]
end
local player1 = Player:new()-----创建Player的一个实例
local player2 = Player:new()-----第二个实例对象
player1:Move()-----1,1
player2:Move()-----1,1
------------当内部:self.base:Move()时,会输出 <1,1> <2,2>
```

## 4> 面向对象汇总

```lua
----《封装- -----创建一个基类
object = {} 
object.id = 1
function object:new()----创建声明类的实例方法
     local obj = {} 
     setmetatable(obj,self)
     self.__index = self---------创建声明类与基类的继承关联
     return obj
end
function object:subClass(className)---创建子类继承方法
      _G[className] = {}
      local obj = _G[className]
      self.__index = self
      setmetatable(obj,self)
 end
----------------创建一个类的对象
local myclass = object:new()
-----------------------------------------------------------------
----《继承- -----创建一个继承object的子类对象
object:subclass("GameObject")
GameObject.posX = 0
GameObject.posY = 0
function GameObject:Move()
  self.posX = self.posX+1
  self.posY = self.posY+1
  print(self.posX)
  print(self.posY)
end
----《多态- ------创建GameObject的子类重写它的方法
GameObject:sunclass("Player")
function Player:Move()
    self.base.Move()---使用点不用冒号防止将父类的成员修改
    --[[ 重写方法 ]]
end
local player1 = Player:new()
local player2 = Player:new()
player2.Move()
```

---
