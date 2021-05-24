# lua协程

- 协程概述
  - 协程具有协同的性质，它允许两个或多个方法以某种可控的方式协同工作。在任何一个时刻，都只有一个协程在运行，只有当正在运行的协程主动挂起时它的执行才会被挂起（暂停）。
  - 假设我们有两个方法，一个是主程序方法，另一个是一个协程。当我们使用 resume 函数调用一个协程时，协程才开始执行。当在协程调用 yield 函数时，协程挂起执行。再次调用 resume 函数时，协程再从上次挂起的地方继续执行。这个过程一直持续到协程执行结束为止。
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

## 5> 协程中可用的函数

|S.N.|方法和功能|
|:----|:----|
|1|	coroutine.create(f):用函数 f 创建一个协程，返回 thread 类型对象。|
|2|	coroutine.resume(co[,val1,...]): 传入参数（可选），重新执行协程 co。此函数返回执行状态，也可以返回其它值。|
|3|	coroutine.running():返回正在运行的协程，如果在主线程中调用此函数则返回 nil。|
|4|	coroutine.status(co):返回指定协程的状态，状态值允许为：正在运行(running)，正常(normal)，挂起(suspended)，结束(dead)。|
|5|	coroutine.wrap(f):与前面 coroutine.create 一样，coroutine.wrap 函数也创建一个协程，与前者返回协程本身不同，后者返回一个函数。当调用该函数时，重新执行协程。|
|6|	coroutine.yield(...):挂起正在执行的协程。为此函数传入的参数值作为执行协程函数 resume 的额外返回值（默认会返回协程执行状态）。|

---
