# Unity与Android项目中的AndroidManifest配置表
---
- Android用于记录app的信息，包括包名，appname，icon，设备屏幕信息，用户请求权限，Activity活跃模块等。
- Unity在构建项目的时候会使用Unity/Assets/Pligins/Android下的安卓清单，包括库文件，资源信息，AndroidManifest配置等。
- 在Unity中调用Andriod设备的权限，需要在配置表中注册相应的Permission。

```xml
<manifest xmlns:android="http://schemas.android.com/apk/res/android" package="com.unity3d.player" xmlns:tools="http://schemas.android.com/tools">
	<application android:label="@string/app_name" android:icon="@mipmap/app_icon"><!--application的一些属性，appName，appIcon等-->
		
		<!--activity 入口信息-->
        <activity android:name="com.u2a.unity_javalib.TestActivity_override" android:theme="@style/UnityThemeSelector" android:screenOrientation="fullSensor" android:launchMode="singleTask" android:configChanges="mcc|mnc|locale|touchscreen|keyboard|keyboardHidden|navigation|orientation|screenLayout|uiMode|screenSize|smallestScreenSize|fontScale|layoutDirection|density" android:hardwareAccelerated="false">
            <intent-filter>
                <action android:name="android.intent.action.MAIN" />
                <category android:name="android.intent.category.LAUNCHER" />
            </intent-filter>
            <meta-data android:name="unityplayer.UnityActivity" android:value="true" />
            <meta-data android:name="android.notch_support" android:value="true" />
        </activity>

        <meta-data android:name="unity.splash-mode" android:value="0" />
        <meta-data android:name="unity.splash-enable" android:value="True" />
        <meta-data android:name="notch.config" android:value="portrait|landscape" />
        <meta-data android:name="unity.build-id" android:value="5b30dee8-3541-49a0-bbf7-dbb399bf44d5" />
    </application>

	<!--Android 的一些权限，用户请求--><!--需要的权限需要在application块之后注册-->

	    <!--网络权限--> 
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
    <uses-permission android:name="android.permission.INTERNET" />

    <uses-feature android:glEsVersion="0x00030000" />
    <uses-feature android:name="android.hardware.vulkan.version" android:required="false" />
    <uses-feature android:name="android.hardware.touchscreen" android:required="false" />
    <uses-feature android:name="android.hardware.touchscreen.multitouch" android:required="false" />
    <uses-feature android:name="android.hardware.touchscreen.multitouch.distinct" android:required="false" />
	
</manifest>
```
---
