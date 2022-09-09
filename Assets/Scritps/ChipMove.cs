using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ChipMove : MonoBehaviour
{
    public event Action OnCompleteMoveEvent;
    [SerializeField] float _duration;
    public void MoveChip(List<Vector3> path)
    {
        var seq = DOTween.Sequence();
        foreach(var s in path)
        {
            seq.Append(transform.DOMove(s, _duration));
        }
        seq.AppendCallback(() => OnPosition());
    }
    void OnPosition()
    {
        InputControl.SetBoolFalse();
        PositionStatusControler.LockPosition(transform.position);
        OnCompleteMoveEvent?.Invoke();
    }
}