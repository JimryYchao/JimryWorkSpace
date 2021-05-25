|*指令类型*|*命令行指令*|
|:----|:----|
|查看连接设备|adb devices [-1]|
|查看设备安装的应用程序包|adb shell pm list packages|
|安装应用|adb [-s\|-d\|-e] install path_to_apk|
|卸载应用|adb [-s\|-d\|-e] uninstall [-k] package_name|
|查看运行的进程|adb shell ps|
|查看应用运行情况|adb shell dumpsys meminfo package_name|
|查看已安装的全部应用|adb shell pm list packages|
|查看应用详细信息|adb shell dumpsys package <packagename>|
|查看正在运行的services|adb shell dumpsys activity services [<packagename>]|
|清除应用数据和缓存|adb shell pm clear <packagename>|
|停止adb服务器|adb kill-server|
|开启adb服务器|adb start-server|


---


|*缩写指令*|*指令含义*|
|:----|:----|
|-e|表示发送指令时，多个设备中只有一个模拟器|
|-d|表示发送指定时，有多个设备，但只连接了一个硬件设备|
|-s|结合-s与adb命令来指定序列号。<br>（adb -s emulator-5555 install helloWorld.apk）|
|    |    |

