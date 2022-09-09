using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionStatusControler : MonoBehaviour
{
    public static List<PosStatus> posStatuses;
    private void OnEnable()
    {
        CreatePoints.OnCreatePointsEvent += GetPointsStatusList;
    }
    private void OnDisable()
    {
        CreatePoints.OnCreatePointsEvent -= GetPointsStatusList;
    }
    private void GetPointsStatusList(List<PosStatus> list)
    {
        posStatuses = list;
    }
    public static void LockPosition(Vector3 pos)
    {
        foreach(var s in posStatuses)
        {
            if(pos == s.transform.position)
            {
                s.isFree = false;
            }
        }
    }
    public static void UnLockPosition(Vector3 pos)
    {
        foreach (var s in posStatuses)
        {
            if (pos == s.transform.position)
            {
                s.isFree = true;
            }
        }
    }
}
