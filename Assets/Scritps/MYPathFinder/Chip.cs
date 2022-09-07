using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChipMove))]
public class Chip : MonoBehaviour
{
    public event Action<Vector3, Chip> OnPositionEvent;
    public int index;
    List<Vector3> _freePoses;
    ChipMove _move;
    PathFinder _pathFinder;
    PathFinderCreatePath _finderCreatePath;

    public ChipMove move { get { return _move = _move ?? GetComponent<ChipMove>(); } }
    public PathFinder pathFinder{ get { return _pathFinder = _pathFinder ?? FindObjectOfType<PathFinder>();}}
    public PathFinderCreatePath finderCreatePath { get { return _finderCreatePath = _finderCreatePath ?? FindObjectOfType<PathFinderCreatePath>();}}

    private void OnEnable()
    {
        move.OnCompleteMoveEvent += OnComplete;
    }
    private void OnDisable()
    {
        move.OnCompleteMoveEvent -= OnComplete;
    }

    public void Move(Vector3 pos)
    {
        if (_freePoses.Contains(pos))
        {
            var s = finderCreatePath.FindPath(transform.position, pos, _freePoses);
            move.MoveChip(s);
        }
    }

    public void Selected()
    {
        PositionStatusControler.UnLockPosition(transform.position);
        _freePoses = pathFinder.StartFinding(transform.position);
        foreach (var s in _freePoses)
        {
            HightLight.HightLightObject(s);
        }
    }

    public void Deselected()
    {
        PositionStatusControler.LockPosition(transform.position);
    }

    void OnComplete()
    {
        OnPositionEvent?.Invoke(transform.position, this);
    }
}
