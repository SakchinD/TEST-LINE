using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] CanvasControl _canvas;
    List<int> _winPosInsexs = new List<int>();
    List<Vector3> _posList = new List<Vector3>();
    List<ChipNMAgentCotnrol> _chipsList;
    private void Start()
    {
        _winPosInsexs = TxtConverter.winPosNumList;
        _posList = TxtConverter.posList;
        ChipCreater.OnChipsCreateEvent += GetChips;
    }
    private void OnDestroy()
    {
        ChipCreater.OnChipsCreateEvent -= GetChips;
    }
    void CheckPositions(Vector3 pos,ChipNMAgentCotnrol chip)
    {
        int index = _posList.IndexOf(pos);
        for (int i = 0; i < _winPosInsexs.Count; i++)
        {
            if(index == _winPosInsexs[i] - 1 && i == chip.index)
            {
                _chipsList.Remove(chip);
                chip.OnPositionEvent -= CheckPositions;
            }
        }

        if (_chipsList.Count == 0)
        {
            ClearGame();
            _canvas.SetActivGameClear();
        }
    }

    void ClearGame()
    {
        InputControl.SetBoolTrue();
    }

    void GetChips(List<ChipNMAgentCotnrol> list)
    {
        _chipsList = list;
        foreach(var s in _chipsList)
        {
            s.OnPositionEvent += CheckPositions;
        }
    }
}
