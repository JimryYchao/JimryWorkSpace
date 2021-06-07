# 如何调用Toast
---
## 1. Toast——Android系统中一种消息框类型

- Toast是包含用户点击消息。Toast类会帮助你创建和显示这些。
- Android中的Toast是一种简易的消息提示框。

> 简介
- 当视图显示给用户，在应用程序中显示为浮动。和Dialog不一样的是，它永远不会获得焦点，无法被点击。
- 用户将可能是在中间键入别的东西。Toast类的思想就是尽可能不引人注意，同时还向用户显示信息，希望他们看到。
- 而且Toast显示的时间有限，Toast会根据用户设置的显示时间后自动消失。
  
> Java中Toast使用方法

```java
import android.widget.Toast;

//----在当前的activity中，为某点击事件加入下列代码：

Toast.makeText(getApplicationContext(), "你想提示的信息",Toast.LENGTH_SHORT).show();

//----点击时会出现浮框提示
```
---

## 2. Unity中调用Toast
```csharp
private void toastText(object str)
{
#if UNITY_ANDROID
    //获取Unity提供给安卓的UnityPlayer
    AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //获取安卓平台当前活跃Activity
    AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        //反射Toast Java类
    AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        //Toast.makeText()方法的第一个参数getApplicationContext()
    AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
    currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
    {
        //Toast.makeText()方法的第二个参数是Java.String
    AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", str.ToString());
        //调用toast.maketext().show()方法，将str显示在设备中
    Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT")).Call("show");
        }
));
#endif
    }
```
---
## 3. 在Unity中为string拓展方法

```csharp
public static class Toast
{
#if UNITY_ANDROID
    /// <summary>
    /// Show String as Toast.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <param name="activity">Activity.</param>
    public static void showAsToast(this string text, AndroidJavaObject activity = null)
    {
        Debug.Log(text);
        if (activity == null)
        {
            AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", text);
            Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT")).Call("show");
        }));
    }
    /// <summary>
    /// CSharp string to JavaString
    /// </summary>
    /// <param name="CSharpString"></param>
    /// <returns></returns>
    public static AndroidJavaObject toJavaString(this string CSharpString)
    {
        return new AndroidJavaObject("java.lang.String", CSharpString);
    }
#endif
}
```
---

http://www.manew.com/home.php?mod=space&uid=175038&do=thread&view=me&from=space