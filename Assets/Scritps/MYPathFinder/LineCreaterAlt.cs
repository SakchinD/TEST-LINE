using System;
using System.Collections.Generic;
using UnityEngine;

public class LineCreaterAlt : MonoBehaviour
{
    public GameObject line;
    public static event Action OnLineCreateEvent;
    List<Vector3> _pointsList = new List<Vector3>();
    [SerializeField] float _yOffSet,_horizontalOffSet;
    private void Start()
    {
        CreateLine();
    }
    
    void CreateLine()
    {
        _pointsList = TxtConverter.posList;

        List<int[]> pairList = TxtConverter.pairList;
        for (int i = 0; i < pairList.Count; i++)
        {
            int first = pairList[i][0] - 1;
            int second = pairList[i][1] - 1;
            Vector3 fPos = _pointsList[first];
            Vector3 sPos = _pointsList[second];
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