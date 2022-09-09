using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightLight 
{
    static List<GameObject> _lightList = new List<GameObject>();

	public static void ClearAllHightLights()
    {
		if(_lightList.Count >0)
        {
			for(int i = 0; i < _lightList.Count; i++)
            {
				_lightList[i].SetActive(false);	
            }
            _lightList.Clear();
        }
    }
	public static void HightLightObject(Vector3 pos)
    {
		GameObject markObj = ObjectPool.Instence.GetPooledObject("light");
		markObj.SetActive(true);
        markObj.transform.position = pos;
		_lightList.Add(markObj);
    }
}
