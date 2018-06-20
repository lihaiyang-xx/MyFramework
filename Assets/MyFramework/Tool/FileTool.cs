/*
	功能描述：
	
	时间：
	
	作者：李海洋
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class FileTool
{
     /// <summary>
    /// 写入字符串到文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="contents"></param>
    public static void WriteStringToFile(string fileName, string contents)
    {
        string dirctry = GetPrefix(fileName, '/');
        if (!Directory.Exists(dirctry))
        {
            Directory.CreateDirectory(dirctry);
        }
        StreamWriter sWriter = null;
        FileStream fileStream = null;
        try
        {
            fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite,FileShare.None);
            sWriter = new StreamWriter(fileStream);
            sWriter.Write(contents);
        }
        finally
        {
            if (sWriter != null)
            {
                sWriter.Close();
            }
            if(fileStream != null)
            {
                fileStream.Close();
            }
        }
    }

    public static string ReadStringFromFile(string fileName,string encodeing = "utf-8")
    {
        StreamReader sReader = null;
        FileStream fileStream = null;
        try
        {
            fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            sReader = new StreamReader(fileStream,Encoding.GetEncoding(encodeing));
            return sReader.ReadToEnd();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            if (sReader != null)
            {
                sReader.Close();
            }
            if(fileStream != null)
            {
                fileStream.Close();
            }
        }
        return "";
    }

    public static Texture2D ReadTextureFromFile(string fileName)
    {
        if (!System.IO.File.Exists(fileName))
        {
            Debug.LogError(fileName + "不存在！");
            return null;
        }
        byte[] bytes = System.IO.File.ReadAllBytes(fileName);
        Texture2D texture = new Texture2D(0, 0);
        texture.LoadImage(bytes);
        return texture;
    }
	
    static string GetPrefix(string source, char flag)
    {
        int position = source.LastIndexOf(flag);
        return source.Remove(position);
    }
}
