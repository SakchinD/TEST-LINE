using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipCreaterAlt : MonoBehaviour
{
    public static event Action<List<Chip>> OnChipsCreateEvent;
    public Color[] colorsList;
    ChipFactoryAlt _factory;
    List<Vector3> _positionList;
    List<Chip> _chipsList = new List<Chip>();
   
    private void OnEnable()
    {
        CreatePointsAlt.OnCreatePointsPosEvent += SetPositionsList;
        _factory = new ChipFactoryAlt();
    }
    private void OnDisable()
    {
        CreatePointsAlt.OnCreatePointsPosEvent += SetPositionsList;
    }
    void SetPositionsList(List<Vector3> posList)
    {
        _positionList = posList;
        CreateChips();
    }
    void CreateChips()
    {
        List<int> posIndexList = TxtConverter.startPosNumList;
        for(int i = 0; i < posIndexList.Count;i++)
        {
            Chip chipObj = _factory.CreateChip(_positionList[posIndexList[i] - 1], 
                colorsList[i],transform);
            
            chipObj.index = i;
            _chipsList.Add(chipObj);
            PositionStatusControler.posStatuses[posIndexList[i]-1].isFree = false;
        }
        OnChipsCreateEvent?.Invoke(_chipsList);
    }
}
