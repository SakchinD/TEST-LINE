using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    List<Vector3> _posList = new List<Vector3>();
    List<Vector3> _availablePosList = new List<Vector3>();
    List<Vector3> _checkedPos = new List<Vector3>();
    List<Vector3> _canMovePosList = new List<Vector3>();
    List<PosStatus> _posStatuses = new List<PosStatus>();
    List<int[]> _pairList;

    private void Start()
    {
        _posList = TxtConverter.posList;
        _pairList = TxtConverter.pairList;
    }
    public List<Vector3> StartFinding(Vector3 pos)
    {
        _canMovePosList.Clear();
        _checkedPos.Clear();
        _canMovePosList.Add(pos);
        _posStatuses = PositionStatusControler.posStatuses;
        List<Vector3> list = PathFind(pos);
        return list;
    }    
    public List<Vector3> PathFind(Vector3 curPos)
    {        
        int index = _posList.IndexOf(curPos);
        foreach (var s in _pairList)
        {
            if (index == s[0] - 1 && _posStatuses[s[0] - 1].isFree)
            {
                if (!_checkedPos.Contains(_posList[s[1] - 1]) && !_availablePosList.Contains(_posList[s[1] - 1]))
                {
                    if (_posStatuses[s[1] - 1].isFree)
                    {
                        _availablePosList.Add(_posList[s[1] - 1]);
                    }
                }
                if (!_canMovePosList.Contains(_posList[s[1] - 1]))
                {
                    if (_posStatuses[s[1] - 1].isFree)
                    {
                        _canMovePosList.Add(_posList[s[1] - 1]);
                    }
                }
            }
            if (index == s[1] - 1 && _posStatuses[s[1] - 1].isFree)
            {
                if (!_checkedPos.Contains(_posList[s[0] - 1]) && !_availablePosList.Contains(_posList[s[0] - 1]))
                {
                    if (_posStatuses[s[0] - 1].isFree)
                    {
                        _availablePosList.Add(_posList[s[0] - 1]);
                    }
                }
                if (!_canMovePosList.Contains(_posList[s[0] - 1]))
                {
                    if (_posStatuses[s[0] - 1].isFree)
                    {
                        _canMovePosList.Add(_posList[s[0] - 1]);
                    }
                }
            }
        }
        _checkedPos.Add(curPos);
        if(_availablePosList.Contains(curPos))
        {
            _availablePosList.Remove(curPos);
        }

        Next();

        if(_availablePosList.Count == 0)
        {
            return _canMovePosList;
        }
        return null;
    }
    void Next()
    {
        if(_availablePosList.Count > 0 )
        {
            PathFind(_availablePosList[0]);            
        }
    }
}
