using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

[RequireComponent(typeof(NavMeshAgent)),RequireComponent(typeof(NavMeshObstacle))]
public class ChipNMAgentCotnrol : MonoBehaviour
{
    public event Action<Vector3, ChipNMAgentCotnrol> OnPositionEvent;
    public int index;
    NavMeshAgent _agent;
    NavMeshObstacle _obstacle;
    bool _isStay;
    Vector3 _targetPos;
    List<Vector3> _posList = new List<Vector3>();
    List<Vector3> _availablePosList = new List<Vector3>();

    public NavMeshAgent agent { get { return _agent = _agent ?? GetComponent<NavMeshAgent>(); } }
    public NavMeshObstacle obstacle { get { return _obstacle = _obstacle ?? GetComponent<NavMeshObstacle>(); } }
    
    private void Start()
    {
        _posList = TxtConverter.GetPositionList();       
    }
    private void Update()
    {
        if (agent.enabled && !_isStay)
        {
            if (agent.hasPath && agent.remainingDistance < 0.1f)
            {
                _isStay = true;
                InputControl.SetBoolFalse();
                OnPositionEvent?.Invoke(_targetPos, this);
            }
        }
    }
    public void DeselectChip()
    {
        agent.enabled = false;
        obstacle.enabled = true;
    }
    public void StartSelect()
    {
        StartCoroutine(Select());
    }
    IEnumerator Select()
    {
        obstacle.enabled = false;
        yield return !obstacle.enabled;
        agent.enabled = true;
        foreach (Vector3 s in _posList)
        {
            NavMeshPath path = new NavMeshPath();
            bool isCan =  agent.CalculatePath(s, path);
            if (isCan && path.status == NavMeshPathStatus.PathComplete)
            {
                _availablePosList.Add(s);
                HightLight.HightLightObject(s);
            }
        }
    }
    public void Move(Vector3 pos)
    {
        foreach(Vector3 s in _availablePosList)
        {
            if(s == pos && agent.enabled)
            {
                _targetPos = s;
                agent.SetDestination(s);
                _isStay = false;
            }
        }
    }
}
