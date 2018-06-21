using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetworkTool
{
    /// <summary>
    /// 获取本机Ipv4地址,如果存在多个,获取第一个
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once InconsistentNaming
    public static string GetLocalIP()
    {
        IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddr = ipe.AddressList
            .First(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        return ipAddr.ToString();
    }
}
