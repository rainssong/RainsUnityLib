using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using UnityEngine;

namespace com.rainssong.io
{
    public class FileCore
    {

        public static byte[] LoadFile (string path)
        {
            FileStream fileStream;
            try
            {
                fileStream = new FileStream (path, FileMode.Open, FileAccess.Read);
            }
            catch (System.Exception)
            {
                return null;
            }

            fileStream.Seek (0, SeekOrigin.Begin);
            //创建文件长度缓冲区
            byte[] bytes = new byte[fileStream.Length];
            //读取文件
            fileStream.Read (bytes, 0, (int) fileStream.Length);
            //释放文件读取流
            fileStream.Close ();
            fileStream.Dispose ();
            fileStream = null;

            return bytes;
        }

        public static string LoadTxt(string path)
        {
            StreamReader reader = new StreamReader(path);
            string content = reader.ReadToEnd();
            return content;
        }

        public static string GetStreamingAssetsPath (string filename)
        {
//             string path =
// #if UNITY_ANDROID && !UNITY_EDITOR
//                 Application.streamingAssetsPath + "/" + filename;
// #elif UNITY_IPHONE && !UNITY_EDITOR
//             "file://" + Application.streamingAssetsPath + "/" + filename;
// #elif UNITY_STANDLONE_WIN || UNITY_EDITOR
//             "file://" + Application.streamingAssetsPath + "/" + filename;
// #else
//             string.Empty;
// #endif
            var path=new System.Uri(Application.streamingAssetsPath+"/"+filename).AbsoluteUri;

            return path;
        }

        //加前缀安卓下File类就不能读取
        public static string GetPersistentDataPath(string filename)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                return @"file://"+Application.persistentDataPath + "/" + filename;
#endif

            return Application.persistentDataPath + "/" + filename;
        }

        public static List<DirectoryInfo> GetDictionarys (string path)
        {
            DirectoryInfo dir = new DirectoryInfo (path);
            if (Directory.Exists(path) == false)  
            {
               Directory.CreateDirectory(path);
            }

            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos ();
            List<DirectoryInfo> result = new();
            for (int i = fileinfo.Length - 1; i >= 0; i--)
            {
                if (fileinfo[i] is DirectoryInfo)
                    result.Add ((DirectoryInfo) fileinfo[i]);
            }
            return result;
        }


        public static List<string> GetAllFiles (string path)
        {
            return GetAllFiles(new DirectoryInfo (path));
        }
        public static List<string> GetAllFiles (DirectoryInfo di)
        {
            List<string> result=new List<string>();
            FileSystemInfo[] fileinfo = di.GetFileSystemInfos (); //初始化一个FileSystemInfo类型的实例

            foreach (FileSystemInfo i in fileinfo) //循环遍历fileinfo下的所有内容
            {
                if (i is DirectoryInfo) //当在DirectoryInfo中存在i时
                {
                    result.AddRange(GetAllFiles ((DirectoryInfo)i)); //获取i下的所有文件
                    //Debug.Log("Dictionary:"+i.FullName);
                }
                else
                {
                    result.Add(i.FullName);
                }
            }
            return result;
        }
    }
}