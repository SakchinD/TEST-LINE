using System;
using System.Collections.Generic;
using UnityEngine;

public class LineCreater : MonoBehaviour
{
    public GameObject line,position;
    public static event Action OnLineCreateEvent;
    public static event Action<List<Transform>> OnPointsCreateEvent;
    List<Transform> _pointsList = new List<Transform>();
    [SerializeField] float _yOffSet,_horizontalOffSet;
    private void Start()
    {
        CreateLine();
    }
    
    void CreateLine()
    {
        List<Vector3> posList = TxtConverter.GetPositionList();
        for (int i = 0; i < posList.Count; i++)
        {
            GameObject point =  Instantiate(position, posList[i],
                Quaternion.identity,transform);

            _pointsList.Add(point.transform);
        }
        OnPointsCreateEvent?.Invoke(_pointsList);

        List<int[]> pairList = TxtConverter.GetPairList();
        for (int i = 0; i < pairList.Count; i++)
        {
            int first = pairList[i][0] - 1;
            int second = pairList[i][1] - 1;
            Vector3 fPos = _pointsList[first].position;
            Vector3 sPos = _pointsList[second].position;
            fPos.y -= _yOffSet;
            sPos.y -= _yOffSet;

            var dis = Vector3.Distance(fPos,sPos);

            GameObject lineObj = Instantiate(line, transform.position,
                Quaternion.identity, transform);

            lineObj.transform.position = Vector3.Lerp(fPos,sPos, 0.5f);

            lineObj.transform.LookAt(sPos);
            lineObj.transform.localScale = new Vector3(2, 0.1f, dis + _horizontalOffSet);
        }
        OnLineCreateEvent?.Invoke();
    }
}