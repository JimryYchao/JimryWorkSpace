# lua协程

---

## 1> Coroutine的创建

- 协程的本质是一个线程对象

---

### 1.1 声明的两种方式

```lua
function fun = ()---先声明一个方法
    print("这是一个方法")
 end
 
------------第一种
co = coroutine.create(fun)---创建一个thread类型的协程
------------第二种
co2= coroutine.wrap(fun)-----创建一个function类型的协程 
```

---

## 2> 协程的运行

```lua
coroutine.resume(co)------用来运行thread类型的协程
co2()---------------------function类型的协程像函数一样发布
```

---

## 3> 协程的挂起

```lua
fun2 = function()------先声明一个可协程挂起函数
  local i = 1
  while true do
      print(i)
      i= i+1
      coroutine.yield(i)
  end
end  
---------------------------------
co3 = coroutine.create(fun2)
a = coroutine.resume(co3)-----1
--//a = true，表示resume方法启用协程成功并返回true
b，c = coroutine.resume(co3)-----2
--//b = true，c = 2，c为方法内yield方法返回的值，yield可以像return样返回值
coroutine.resume(co3)-----3
coroutine.resume(co3)-----4
------协程被挂起用yield保留了运行时状态，线程被保留
```

- resume与wrap方法的区别：
  - resume启用thread协程并返回是否启用成功

  - wrap不会返回是否启用信息。

---

## 4> 协程的状态

- 用coroutine.status(协程对象)返回状态
  - dead----已结束
  - suspended---暂停
  - running----进行中

- 用coroutine.running() 方法，可以返回正在运行的协程编号，即type（协程变量）。

---
