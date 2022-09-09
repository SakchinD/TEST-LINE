using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatePoints : MonoBehaviour
{
    public static event Action<List<Vector3>> OnCreatePointsPosEvent;
    public static event Action<List<PosStatus>> OnCreatePointsEvent;
    public PosStatus posPref;
    List<Vector3> _posList;
    List<PosStatus> _statusList = new List<PosStatus>();
    private void Start()
    {
        _posList = TxtConverter.GetPositionList();
        foreach(Vector3 pos in _posList)
        {
            var s = Instantiate(posPref, pos, Quaternion.identity,transform);
            s.isFree = true;
            _statusList.Add(s);
        }
        OnCreatePointsEvent?.Invoke(_statusList);
        OnCreatePointsPosEvent?.Invoke(_posList);
        
    }
}