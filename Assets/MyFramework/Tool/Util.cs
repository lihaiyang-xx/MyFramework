using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Xml;
using UnityEngine .UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public static class Util
{

    public static int Int(object o)
    {
        return Convert.ToInt32(o);
    }

    public static float Float(object o)
    {
        return (float)Math.Round(Convert.ToSingle(o), 2);
    }

    public static long Long(object o)
    {
        return Convert.ToInt64(o);
    }

    public static int Random(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static float Random(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static string GetPostfix(string source,char flag)
    {
        int position = source.LastIndexOf(flag);
        return source.Remove(0, position + 1);
    }

    

    public static long GetTime()
    {
        TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
        return (long)ts.TotalMilliseconds;
    }

    /// <summary>
    /// 搜索子物体组件-GameObject版
    /// </summary>
    public static T Get<T>(GameObject go, string subnode) where T : Component
    {
        if (go != null)
        {
            Transform sub = go.transform.Find(subnode);
            if (sub != null) return sub.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 搜索子物体组件-Transform版
    /// </summary>
    public static T Get<T>(Transform go, string subnode) where T : Component
    {
        if (go != null)
        {
            Transform sub = go.Find(subnode);
            if (sub != null) return sub.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 搜索子物体组件-Component版
    /// </summary>
    public static T Get<T>(Component go, string subnode) where T : Component
    {
        return go.transform.Find(subnode).GetComponent<T>();
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    public static T Add<T>(GameObject go) where T : Component
    {
        if (go != null)
        {
            T[] ts = go.GetComponents<T>();
            for (int i = 0; i < ts.Length; i++)
            {
                if (ts[i] != null) GameObject.Destroy(ts[i]);
            }
            return go.gameObject.AddComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    public static T Add<T>(Transform go) where T : Component
    {
        return Add<T>(go.gameObject);
    }

    /// <summary>
    /// 扩展方法 查找所有父物体
    /// 若未找到，返回-1
    /// 若找到，返回相隔层数（临近子物体于其父物体相隔1层）
    /// </summary>
    /// <param name="self"></param>
    /// <param name="parentName"></param>
    /// <returns></returns>
    public static int FindParents(this Transform self,string parentName)
    {
        Transform t = null;
        return self.FindParents(parentName, out t);
    }
    /// <summary>
    /// 扩展方法 查找所有父物体
    /// 若未找到，返回-1
    /// 若找到，返回相隔层数（临近子物体于其父物体相隔1层）
    /// outNearChild表示找到的父物体的与其相隔1层的子物体
    /// </summary>
    /// <param name="self"></param>
    /// <param name="parentName"></param>
    /// <param name="NearChild"></param>
    /// <returns></returns>
    public static int FindParents(this Transform self, string parentName, out Transform NearChild)
    {
        NearChild = null;
        Transform t = self;
        int level = 0;
        while (t.parent != null)
        {
            level++;
            t = t.parent;
            if (t.name == parentName)
            {
                return level;
            }
            NearChild = t;
        }
        return -1;
    }
    public static int FindParents(this Transform self, Transform parent)
    {
        Transform t = null;
        return self.FindParents(parent, out t);
    }
    public static int FindParents(this Transform self, Transform parent, out Transform NearChild)
    {
        NearChild = null;
        Transform t = self;
        int level = 0;
        while (t.parent != null)
        {
            level++;
            t = t.parent;
            if (t.Equals(parent))
            {
                return level;
            }
            NearChild = t;
        }
        return -1;
    }


    /// <summary>
    /// 查找子对象
    /// </summary>
    public static GameObject Child(GameObject go, string subnode)
    {
        return Child(go.transform, subnode);
    }

    /// <summary>
    /// 查找子对象
    /// </summary>
    public static GameObject Child(Transform go, string subnode)
    {
        Transform tran = go.Find(subnode);
        if (tran == null) return null;
        return tran.gameObject;
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    public static GameObject Peer(GameObject go, string subnode)
    {
        return Peer(go.transform, subnode);
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    public static GameObject Peer(Transform go, string subnode)
    {
        Transform tran = go.parent.Find(subnode);
        if (tran == null) return null;
        return tran.gameObject;
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <returns></returns>
    public static T Peer<T>(this Transform self)
    {
        foreach (Transform trans in self.parent)
        {
            T t = trans.GetComponent<T>();
            if (t != null)
            {
                return t;
            }
        }
        return default(T);
    }

    /// <summary>
    /// 手机震动
    /// </summary>
    public static void Vibrate()
    {
        //int canVibrate = PlayerPrefs.GetInt(Const.AppPrefix + "Vibrate", 1);
        //if (canVibrate == 1) iPhoneUtils.Vibrate();
    }

    /// <summary>
    /// Base64编码
    /// </summary>
    public static string Encode(string message)
    {
        byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(message);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Base64解码
    /// </summary>
    public static string Decode(string message)
    {
        byte[] bytes = Convert.FromBase64String(message);
        return Encoding.GetEncoding("utf-8").GetString(bytes);
    }

    /// <summary>
    /// 判断数字
    /// </summary>
    public static bool IsNumeric(string str)
    {
        if (str == null || str.Length == 0) return false;
        for (int i = 0; i < str.Length; i++)
        {
            if (!Char.IsNumber(str[i])) { return false; }
        }
        return true;
    }

    /// <summary>
    /// HashToMD5Hex
    /// </summary>
    public static string HashToMD5Hex(string sourceStr)
    {
        byte[] Bytes = Encoding.UTF8.GetBytes(sourceStr);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] result = md5.ComputeHash(Bytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                builder.Append(result[i].ToString("x2"));
            return builder.ToString();
        }
    }

    

    /// <summary>
    /// 清除所有子节点
    /// </summary>
    public static void ClearChild(Transform go,bool immediate = false)
    {
        if (go == null) return;
        for (int i = go.childCount - 1; i >= 0; i--)
        {
            if (immediate)
            {
                GameObject.DestroyImmediate(go.GetChild(i).gameObject);
            }
            else
            {
                GameObject.Destroy(go.GetChild(i).gameObject);
            }
        }
    }


    /// <summary>
    /// 清理内存
    /// </summary>
    public static void ClearMemory()
    {
        GC.Collect(); Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 是否为数字
    /// </summary>
    public static bool IsNumber(string strNumber)
    {
        Regex regex = new Regex("[^0-9]");
        return !regex.IsMatch(strNumber);
    }

    /// <summary>
    /// 取得行文本
    /// </summary>
    public static string GetFileText(string path)
    {
        #if UNITY_STANDALONE
                        return File.ReadAllText(path);
        #endif
                return "";
    }

    /// <summary>
    /// 网络可用
    /// </summary>
    public static bool NetAvailable
    {
        get
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }

    /// <summary>
    /// 是否是无线
    /// </summary>
    public static bool IsWifi
    {
        get
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }
    }

    

    /// <summary>
    /// 载入资源,首先从Resource目录载入资源,如果资源不存在
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static UnityEngine.Object LoadAssets(string name)
    {
        UnityEngine.Object obj = Resources.Load(name);
        if (obj != null)
        {
            return obj;
        }
        LogError(name + "not exist!");
        return null;
    }

    /// <summary>
    /// 从网络路径异步加载资源
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static IEnumerator InstantiateFromUrl(string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                AssetBundle assetbundle = www.assetBundle;
                string name = assetbundle.GetAllAssetNames()[0];
                UnityEngine.Object obj = assetbundle.LoadAsset(name);
                GameObject go = UnityEngine.Object.Instantiate(obj) as GameObject;
                go.name = go.name.Replace("(Clone)","");
                assetbundle.Unload(false);
            }
            else
            {
                Debug.LogError(www.error);
            }
        }
            
    }

   public static IEnumerator LoadLevelFromUrl(string url,LoadSceneMode loadModel = LoadSceneMode.Single)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                AssetBundle assetbundle = www.assetBundle;
                //assetbundle.LoadAllAssets();
                string sceneName = url.Substring(url.LastIndexOf('/')+1).Replace(".unity3d","");
                Debug.Log("即将加载场景：" + sceneName);
                SceneManager.LoadScene(sceneName,loadModel);

                yield return new WaitUntil(() =>
                {
                    return SceneManager.GetSceneByName(sceneName).isLoaded;
                });
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
                assetbundle.Unload(false);
            }
            else
            {
                Debug.LogError(www.error);
            }
        }
    }

    public static UnityEngine.GameObject Instantiate(string name, Transform parent = null)
    {
        UnityEngine.Object obj = LoadAssets(name);
        if(obj != null)
        {
            return UnityEngine.Object.Instantiate(obj, parent) as GameObject;
        }
        return null;
    }

    public static UnityEngine.GameObject Instantiate(string name,Vector3 position,Quaternion rotation, Transform parent = null)
    {
        UnityEngine.Object obj = LoadAssets(name);
        if (obj != null)
        {
            return UnityEngine.Object.Instantiate(obj, position, rotation, parent) as GameObject;
        }
        return null;
    }


    public static void Log(string str)
    {
        #if UNITY_EDITOR
                Debug.Log(str);
        #endif
    }

    public static void LogWarning(string str)
    {
        #if UNITY_EDITOR
                Debug.LogWarning(str);
        #endif
    }

    public static void LogError(string str)
    {
        Debug.LogError(str);
    }

    /// <summary>
    /// 是不是苹果平台
    /// </summary>
    /// <returns></returns>
    public static bool isApplePlatform
    {
        get
        {
            return Application.platform == RuntimePlatform.IPhonePlayer ||
                   Application.platform == RuntimePlatform.OSXEditor ||
                   Application.platform == RuntimePlatform.OSXPlayer;
        }
    }

    

    public static string DataPath
    {
        get { return Application.streamingAssetsPath; }
    }

    public static string GetLocalIP()
    {
        IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddr = ipe.AddressList.Where(_ip => _ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        .First();
        return ipAddr.ToString();
    }

    /// <summary>
    /// 三点贝塞尔曲线
    /// </summary>
    /// <param name="P0"></param>
    /// <param name="P1"></param>
    /// <param name="P2"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector3 BezierCurve(Vector3 P0, Vector3 P1, Vector3 P2, float t)
    {
        Vector3 B = Vector3.zero;
        float t1 = (1 - t) * (1 - t);
        float t2 = t * (1 - t);
        float t3 = t * t;
        B = P0 * t1 + 2 * t2 * P1 + t3 * P2;
        return B;
    }

    public static GameObject[] FindGameobjects(string name)
    {
        List<GameObject> goList = new List<GameObject>();
        Transform[] trs = GameObject.FindObjectsOfType<Transform>();
        foreach(Transform tr in trs)
        {
            if(tr.name == name)
            {
                goList.Add(tr.gameObject);
            }
        }

        return goList.ToArray();
    }

    /// <summary>
    /// 从路径中读取图片
    /// </summary>
    /// <param name="image"></param>
    /// <param name="path"></param>
    public static  void LoadImage(Image image, string path)
    {
        Texture2D texture = new Texture2D(200, 200);
        texture.LoadImage(File.ReadAllBytes(path));
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }
    public static T[] Merge<T>(T[] arr, T[] other)
    {
        T[] buffer = new T[arr.Length + other.Length];
        arr.CopyTo(buffer, 0);
        other.CopyTo(buffer, arr.Length);
        return buffer;
    }

    public class Event
    {
        public delegate void Handler(params object[] args);

        public static void Listen(string message, Handler action)
        {
            var actions = listeners[message] as Handler;
            if (actions != null)
            {
                listeners[message] = actions + action;
            }
            else
            {
                listeners[message] = action;
            }
        }

        public static void Remove(string message, Handler action)
        {
            var actions = listeners[message] as Handler;
            if (actions != null)
            {
                listeners[message] = actions - action;
            }
        }

        public static void Clean(string message)
        {
            var actions = listeners[message] as Handler;
            if(actions != null)
            {
                listeners[message] = null;
            }
        }

        public static void Send(string message, params object[] args)
        {
            Debug.Log("广播消息：" + message);
            var actions = listeners[message] as Handler;
            if (actions != null)
            {
                actions(args);
            }
        }



        private static Hashtable listeners = new Hashtable();
    }

    
}

public class ReadXml
{

    /// <summary>
    /// XML 文档实例
    /// </summary>
    XmlDocument xmlDocument;

    public ReadXml()
    {
        xmlDocument = new XmlDocument();
    }

    #region 数据加载
    /// <summary>
    /// 加载指定路径的xml文件
    /// </summary>
    public bool Load(string filePath)
    {
        if (xmlDocument == null)
        {
            throw new ArgumentNullException("xmlDocument");
        }

        if (filePath == null || filePath.Length == 0)
        {
            throw new ArgumentNullException("filePath");
        }
        try
        {
            xmlDocument.Load(filePath);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        return true;
    }

    /// <summary>
    /// 加载 XML的文本文件
    /// </summary>
    /// <param name="xmlFile"></param>
    public bool LoadXml(string xmlFile)
    {
        if (xmlDocument == null)
        {
            throw new ArgumentNullException("xmlDocument");
        }

        if (xmlFile == null || xmlFile.Length == 0)
        {
            throw new ArgumentNullException("xmlFile");
        }
        TextReader tr = new StringReader(xmlFile);
        xmlDocument.Load(tr);

        return true;
    }
    #endregion

    public void Remove()
    {
        xmlDocument.RemoveAll();
    }

    public XmlNodeList getNodeList(string path)
    {
        try
        {
            XmlNodeList childNodeList = xmlDocument.DocumentElement.SelectSingleNode(path).ChildNodes;
            return childNodeList;
        }
        catch (Exception ex)
        {
            Debug.LogError("error: getNodeList:" + path + "//" + ex);
            return null;
        }
    }

    public string getValue(string path)
    {
        string[] splitStr = path.Split('/');
        XmlNode childNode = null;
        try
        {
            foreach (string str in splitStr)
            {

                if (childNode == null)
                {
                    childNode = xmlDocument.DocumentElement.SelectSingleNode(str);
                }
                else
                {
                    childNode = childNode.SelectSingleNode(str);
                }
            }
            return childNode.InnerText;
        }
        catch (Exception ex)
        {
            Debug.LogError("error: getValue: " + path + "   " + ex);
            return "";
        }

    }
}

public static class IOExtension
{
    public static string GetUpLevelPath(string path)
    {
        int index1 = path.LastIndexOf('/');
        int index2 = path.LastIndexOf("\\");

        int index = (index1 > index2) ? index1 : index2;

        return path.Remove(index);

    }

    public static int GetPathDepth(string path)
    {
        int num1 = Regex.Matches(path, @"/").Count;
        int num2 = Regex.Matches(path, @"\").Count;

        return num1 + num2;
    }

    public static void GetFileSystemEntries(string path,ref string[] paths)
    {
        string[] strs = Directory.GetFileSystemEntries(path);
        paths = Util.Merge<string>(paths, strs);
        foreach(string str in strs)
        {
            if(Directory.Exists(str))
            {
                GetFileSystemEntries(str,ref paths);
            }
        }
    }
}

