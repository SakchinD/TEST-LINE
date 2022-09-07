using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipCreater : MonoBehaviour
{
    public static event Action<List<ChipNMAgentCotnrol>> OnChipsCreateEvent; 
    public Color[] colorsList;

    ChipFactory _factory;
    List<Transform> _positionList;
    List<ChipNMAgentCotnrol> _chipsList = new List<ChipNMAgentCotnrol>();
   
    private void OnEnable()
    {
        LineCreater.OnPointsCreateEvent += SetPositionsList;
        FildControl.OnFildCreate += CreateChips;
        _factory = new ChipFactory();
    }
    private void OnDisable()
    {
        LineCreater.OnPointsCreateEvent -= SetPositionsList;
        FildControl.OnFildCreate -= CreateChips;
    }
    void SetPositionsList(List<Transform> posList)
    {
        _positionList = posList;
    }
    void CreateChips()
    {
        List<int> posIndexList = TxtConverter.GetStartPosNumList();
        for(int i = 0; i < posIndexList.Count;i++)
        {
            ChipNMAgentCotnrol chipObj = _factory.CreateChip(_positionList[posIndexList[i] - 1].position, 
                colorsList[i],transform);
            
            chipObj.DeselectChip();
            chipObj.index = i;
            _chipsList.Add(chipObj);
        }
        OnChipsCreateEvent?.Invoke(_chipsList);
    }
}
