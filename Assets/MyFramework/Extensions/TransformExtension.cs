using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{

    public static void SetPosX(this Transform t, float x)
    {
        Vector3 v = t.position;
        v.x = x;
        t.position = v;
    }
    public static void SetPosY(this Transform t, float y)
    {
        Vector3 v = t.position;
        v.y = y;
        t.position = v;
    }
    public static void SetPosZ(this Transform t, float z)
    {
        Vector3 v = t.position;
        v.z = z;
        t.position = v;
    }

    /// <summary>
    /// 获取第level级父物体
    /// </summary>
    /// <param name="self"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static Transform GetParentByLevel(this Transform self, int level)
    {
        Transform t = self;
        while (level-- > 0)
        {
            if (t.parent == null)
            {
                return null;
            }
            t = t.parent;
        }
        return t;
    }
}
