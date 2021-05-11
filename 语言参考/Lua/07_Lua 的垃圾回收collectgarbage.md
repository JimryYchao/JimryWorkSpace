# lua的垃圾回收

---

- GC垃圾收集器

- 垃圾收集器间歇率

- 垃圾收集步进倍率

- 垃圾收集元方法

---

- 垃圾回收关键字------collectgarbage

```lua
collectgarbage("count")----获取当前lua占用内存数，K字节，用返回值*1024就可以得到具体的内存占用字节
```

- lua中有自动定时进行GC的方法，和C#中的垃圾回收机制很类似，当解除值与变量之间的联系时，值会变成垃圾。

```lua
test = 15
print(collectgarbage("count"))------19.99609375
test = nil
collectgarbage("collect")--手动回收
print(collectgarbage("count"))------19.1826171875
```

---

//lua和c#的垃圾回收机制近期详细研究后汇总——2021.5.9
