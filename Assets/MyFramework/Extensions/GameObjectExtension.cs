using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    public static bool IsOnMouse(this GameObject go,Camera camera)
    {
        if (!camera)
        {
            Debug.LogError("没有合适的相机！");
            return false;
        }
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.Equals(go) || hitInfo.transform.IsChildOf(go.transform))
                return true;
        }
        return false;
    }
	
}
