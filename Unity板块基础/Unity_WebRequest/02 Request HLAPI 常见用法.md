# UnityWebRequest 高级API常用的操作
---
## 1. 接收文件或二进制数据

- 从标准的Http或者Https服务端接收简单的文本或二进制数据，可以使用UnityWebRequest.GET方法，这个方法只需要传入获取数据的服务端的URL即可，这个功能和标准的WWW构造方法类似，WWW已弃用。

```csharp
  UnityWebRequest www = UnityWebRequest.Get(url);
```
- 此构造的详细流程是：
  - 创建一个UnityWebRequest 对象，并设置目标URL作为字符串参数，但是没有设置自定义的数据和包头信息。
  - UnityWebRequest 会默认一个标准的DownloadHandlerBuffer 对象，当请求完成之后，这个Handler缓存从服务端接收到的数据，这些数据可以在代码中直接使用。
  - UnityWebRequest 默认不会绑定一个 UploadHandler 对象，但是可以手动绑定。

## 2. 案例 Get()

```csharp
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace UnityTEST.UnityWeb
{
    public class UnityWebRequest_Get_Test : MonoBehaviour
    {
        void Start()
        {
            //开启协程GetText()
            // Debug顺序(1->0->2->4),说明Web.get到数据text并打印到Console
            StartCoroutine(GetText());
            Debug.Log(0);

        }

        IEnumerator GetText()
        {
            // 创建网络请求URL,但没有设置自定义的包头信息与数据,默认会绑定一个DownLoadHandlerBuffer对象.
            UnityWebRequest www = UnityWebRequest.Get("http://www.my-server.com");
            Debug.Log(1);

            // 发送网络请求,并挂起协程.
            Debug.Log(2);
            yield return www.SendWebRequest();

            // 判定是否网络错误,或者http地址错误
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(3);

                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(4);

                // 接收文本数据，并打印到日志中
                Debug.Log(www.downloadHandler.text);

                // 接收二进制数据
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}
```
---
## 3. 接收 Texture()

- 使用 UnityWebRequest.Texture方法，从远程服务端获取一个Texture文件。
- 这个功能和UnityWebRequest.GET方法非常类似，但是它对下载和存储Texture文件做了优化，提高了处理效率。
- UnityWebRequest.Texture同样需要一个字符串参数，这个参数就是要下载的资源的URL地址。

> 这个处理过程是：

  - 创建一个UnityWebRequest对象，并设置目标URL做为字符串参数，但是没有设置自定义的数据和包头信息。
  - UnityWebRequest会默认绑定一个标准的DownloadHandlerTexture对象，DownloadHandlerTexture是一个特定的Download Handler，它会优化在Unity引擎中使用的图片的存储，相对来说，在下载二进制数据和创建在代码中手动创建Texture对象时，它可以大大的减少内存的再分配。
  - UnityWebRequest默认不会绑定一个UploadHandler对象，如果你需要的话，可以手动操作。

---
## 4. 案例 Texture() 

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityTEST.UnityWeb
{
    public class UnityWebRequest_Texture_Test : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(GetTexture());
        }

        /// <summary>
        /// 从网络服务器获取Texture
        /// </summary>
        IEnumerator GetTexture()
        {
            //创建UnityWeb对象,并发送web请求.
            UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://www.my-server.com/image.png");
            yield return www.SendWebRequest();

            // 判定是否网络错误,或者http地址错误
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //获取并创建Texture
                Texture mTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }
    }
}
```

> 第二种方法

```csharp
 IEnumerator GetTexture2()
 {
     UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://www.my-server.com/image.png");
     yield return www.SendWebRequest();

     //获取 Texture 对象
    
 Texture myTexture = DownloadHandlerTexture.GetContent(www); 
```
---
## 5. 下载AssetBundle

- 使用 GetAssetBundle 方法从远程服务器下载一个AssetBundle，这个方法会在工作线程中，将下载的数据流解码，解压缩，并放到一个内部的Buffer之中，这个方法有多种的参数形式。
- 简单的方式有只需要一个URL地址，也可以选择性的对下载的数据进行 checksum 校验数据的完整性。
- 另外想使用AssetBundle的缓存系统，需要提供一个版本号或者一个128位的Hash结构

```csharp
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityTEST.UnityWeb
{
    public class UnityWebRequest_GetAssetBundle_Test : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(GetAssetBundle());
        }

        IEnumerator GetAssetBundle()
        {
            //原有的Request***的方法已从UnityWebRequest类中剥离出去
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("http://www.my-server.com/myData.unity3d");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            }
        }
    }
}
```
---
## 6. 向服务端发送form数据

- 在UnityWebRequest中，有两种基本的方法可以向服务端发送HTML form格式的数据，在web开发中，这种方式叫**提交一个表单数据**。

---
### 6.1 第一种方式：使用 IMultipartFormSection
- 为了更好的控制form数据，UnityWebRequest系统提供了一个用户可以实现的接口：IMultipartFormSection，对于一般的应用程序。
- Unity也提供了两个默认实现的类，MultipartFormDataSection，用于提交form表单数据，MultipartFormFileSection，用于上传文件。

> UnityWebRequest.POST的一个重载方式，接收一个List参数，做为该方法的第二个参数，但是这个List的成员必须是IMultipartFormSections的子类，如下面代码所示：

```csharp
UnityWebRequest.Post(string url, List<IMultipartFormSection> formSections);
```

- 此方法的详细过程：
  - 这个方法会创建一个新的UnityWebRequest对象，并设置目标URL做为第一个参数，也可以给这次本提交的IMultipartFormSection 列表form数据在Header中设置合适的Content-Type值。
  - 这个方法默认会给UnityWebRequest绑定一个DownloadHandlerBuffer，这样做是为了方便获取服务器响应的数据。
  - 和WWWForm Post方法类型，这个HLAPI方法也是按顺序调每一个IMultipartFormSection ，将它们组装成一个符合RFC 2616标准的multipart form格式的数据。
  - 这些预定义好的Form数据会被存储在UploadHanlderRaw对象中，然后把这个对象绑定到UnityWebRequest中，所以，当调用UnityWebRequest.POST方法之后，再修改IMultipartFormSection 对象的数据，将不会影响到发送给服务器的数据。

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityTEST.UnityWeb
{
    public class UnityWebRequest_POST_Test:MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(Upload());
        }

        IEnumerator Upload()
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
            formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

            UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", formData);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }


    }
}
```
---
### 6.2 第二种方式使用WWWForm（遗留的方法）

- 为了方便从 WWW 系统中迁移，UnityWebRequest允许你使用WWWForm 对象提供form数据，如下面代码所示：

```csharp
  UnityWebRequest.Post(string url, WWWForm formData);
```

> 详细过程是：

  - 这个方法会创建一个新的UnityWebRequest对象，并设置目标URL做为第一个参数，它也会读取WWWForm中自定义的包头信息，并将它们复制到UnityWebRequest中
  - 这个方法默认会给UnityWebRequest绑定一个DownloadHandlerBuffer，这样做是为了方便获取服务器响应的数据。
  - 这个方法会读取WWWForm对象中原始的数据，并将它们缓存到一个UploadHandlerRaw对象中，然后将它绑定到UntiyWebRequest中，因此，在调用UnityWebRequest.POST方法之后，再修改原来WWWForm中的数据，将不会改变UntiyWebRequest中的内容。

```csharp
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityTEST.UnityWeb
{
    public class UnityWebRequest_PostForm_WWWForm:MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(Upload());
        }

        IEnumerator Upload()
        {
            WWWForm form = new WWWForm();
            form.AddField("myField", "myData");

            UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
```