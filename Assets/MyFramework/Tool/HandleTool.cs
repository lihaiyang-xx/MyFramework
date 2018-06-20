/*
	功能描述：
	
	时间：
	
	作者：李海洋
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTool
{

	public static void AddMeshCollider(GameObject model)
    {
        MeshFilter filter = model.GetComponent<MeshFilter>();
        if(filter != null && filter.mesh != null && model.GetComponent<Collider>() == null)
        {
            model.AddComponent<MeshCollider>();
        }
        if(model.transform.childCount > 0)
        {
            foreach(Transform child in model.transform)
            {
                AddMeshCollider(child.gameObject);
            }
        }
    }

    public static bool IsMouseOn(GameObject model,Camera camera)
    {
        if(camera == null)
        {
            Debug.LogError("没有合适的相机！");
            return false;
        }
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo))
        {
            if (hitInfo.collider.transform == model.transform || hitInfo.collider.transform.IsChildOf(model.transform))
                return true;
        }
        return false;
    }

    public static T GetComponentFromParent<T>(GameObject model) where T:Component
    {
        T compoent = null;
        while((compoent = model.GetComponent<T>()) == null && model.transform.parent != null)
        {
            model = model.transform.parent.gameObject;
        }
        return compoent;
    }
}
