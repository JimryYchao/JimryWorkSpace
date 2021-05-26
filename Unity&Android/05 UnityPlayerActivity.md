# UnityPlayerActivity

---
## 1> UnityPlayerActivity的生命周期函数

1. OnCreate() 进入Unity时，被调用
2. OnDestroy() 退出时调用
3. OnPause() 切换到后台，进程被挂起
4. OnResume() 从后台切换回来时被调用
5. 
---
## 2> 重写一个类继承UnityPlayerActivity

- 在相同包下创建一个新的类继承UnityPlayerActivity

- 重写super(表示Java父类，超类)的一些生命周期函数
  
```java
public class TestActivity_override extends UnityPlayerActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Log.d("LOG","onCreate:TestActivity_override");
    }

    @Override
    protected void onPause() {
        super.onPause();
        Log.d("LOG","onPause:TestActivity_override");

    }

    @Override
    protected void onResume() {
        super.onResume();
        Log.d("LOG","onResume:TestActivity_override");
    }
}
```
---
## 3> 切换进入Unity项目的入口

- 需要在AndroidManifest.xml中修改入口的类名。
  
![](assets/531.png)

- 注意在当前的activity块中必须有action与category

- 打包运行
- 在切换应用退出桌面，返回应用，可以在Logcat中捕捉到打印信息

---

## 4> C#反射获取UnityPlayer下的currentActivity类变量

- currentActivity代表的是安卓下打开的Unity工程当前处于活跃状态的UnityPlayerActivity。

```csharp
AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
```
## 5> 在Unity中调用java的方法，并把Unity中的输入用Java方法显示出来

- 首先在安卓工程中创建一个java类，在该java类下创建一个ShowMessage的方法。

```java
    public void ShowMessage(String message){
        Toast.makeText(UnityPlayer.currentActivity,message,Toast.LENGTH_LONG).show();
    }

```
- 创建完仍需要打包后导入到Unity中Classes.jar
- 当Unity调用该方法并把参数传入时，会将该字符串打印在安卓设备中。
- 在Unity中调用javaClass_ShowMessage 方法。

```csharp
AndroidJavaObject javaObject = new AndroidJavaObject("Java包名.类名");

javaObject.Call("ShowMessage","Str:参数类型要与反射的方法参数一致");
//当Unity使用Call时会将str的信息传入java中并反馈到安卓设备中显示出来。
```

---
