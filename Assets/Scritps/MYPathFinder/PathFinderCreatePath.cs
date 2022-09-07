using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderCreatePath : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 tPos;
    List<int> _preIndex = new List<int>();
    List<int> _blockIndex = new List<int>();
    List<Vector3> _posList;
    List<Vector3> _canMovePosList = new List<Vector3>();
    List<int> _canMoveIndex = new List<int>();
    List<int[]> _pairList;
    public List<Vector3> _pathPosList = new List<Vector3>();

    public List<Vector3> CreatePath(Vector3 curPos, Vector3 targetPos)
    {
        int cunPosIndex = _posList.IndexOf(curPos);
        int targetPosIndex = _posList.IndexOf(targetPos);

        // проверяем можно ли дойти от этой позици
        foreach (var pair in _pairList)
        {
            if (pair[0] - 1 == cunPosIndex && pair[1] - 1 == targetPosIndex)
            {
                _pathPosList.Add(_posList[targetPosIndex]);
                return _pathPosList;
            }
            if (pair[1] - 1 == cunPosIndex && pair[0] - 1 == targetPosIndex)
            {
                _pathPosList.Add(_posList[targetPosIndex]);
                return _pathPosList;
            }
        }
        
        // находим соседнию позицию и затем с начала метода проверяем её
        foreach (var pair in _pairList)
        {
            if (cunPosIndex == pair[0] - 1 && _canMoveIndex.Contains(pair[1]-1))
            {
                if( !_preIndex.Contains(pair[1] - 1) && !_blockIndex.Contains(pair[1]-1))
                {
                    _pathPosList.Add(_posList[pair[1] - 1]);
                    _preIndex.Add(cunPosIndex);
                    int f = pair[1] - 1;
                    return CreatePath(_posList[f], targetPos);
                }
            }
            if (cunPosIndex == pair[1] - 1 && _canMoveIndex.Contains(pair[0]-1))
            {
                if (!_preIndex.Contains(pair[0] - 1) && !_blockIndex.Contains(pair[0]-1))
                {
                    _pathPosList.Add(_posList[pair[0] - 1]);
                    _preIndex.Add(cunPosIndex);
                    int f = pair[0] - 1;
                    return CreatePath(_posList[pair[0] - 1], targetPos);
                }
            }
        }

        // если путь не найден, добавляем эту позицию в блок и возврошаемся к предедушей
        _blockIndex.Add(cunPosIndex);

        if (_preIndex.Count > 0)
        {
            if(_pathPosList.Count >0)
            {
                _pathPosList.RemoveAt(_preIndex.Count - 1);
            }
            int i = _preIndex[_preIndex.Count - 1];
            _preIndex.Remove(i);
            return CreatePath(_posList[i], tPos);
        }

        return null;
    }
    
    public List<Vector3> FindPath(Vector3 curPos, Vector3 targetPos,List<Vector3> list)
    {
        _posList = TxtConverter.posList;
        _pairList = TxtConverter.pairList;
        _preIndex.Clear();
        _blockIndex.Clear();
        _pathPosList.Clear();
        _canMoveIndex.Clear();
        _canMovePosList = list;
        tPos = targetPos;
        
        foreach(var pos in _canMovePosList)
        {
            _canMoveIndex.Add(_posList.IndexOf(pos));
        }
        List<Vector3> pathList =  CreatePath(curPos, tPos);
        return pathList;
    }
}
