using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TxtConverter : MonoBehaviour
{
    public TextAsset textAsset;
    static List<Vector3> _posList;
    static List<int[]> _pairList;
    static List<int> _startPosNumList;
    static List<int> _winPosNumList;
    static int _chipCount;

    List<string> _textArr;
    
    private void Awake()
    {
        _posList = new List<Vector3>();
        _pairList = new List<int[]>();
        _startPosNumList = new List<int>();
        _winPosNumList = new List<int>();
        ReadTextFile();
    }

    void ReadTextFile()
    {
        _textArr = textAsset.text.Split('\n').ToList();

        int pointCount = int.Parse(_textArr[0]) + 1;
        for (int i = 1; i < pointCount; i++)
        {
            string[] splitString;
            splitString = _textArr[i].Split(","[0]);
            float x = float.Parse(splitString[0]);
            float y = float.Parse(splitString[1]);
            float z = float.Parse(splitString[2]);
            _posList.Add(new Vector3(x, y, z));
        }

        int pairNum = int.Parse(_textArr[pointCount]);
        int pairCount = pairNum + pointCount + 1;
        for(int i = pointCount + 1; i < pairCount;i++)
        {
            string[] splitString;
            splitString = _textArr[i].Split(","[0]);
            int first = int.Parse(splitString[0]);
            int second = int.Parse(splitString[1]);
            int[] pairs = { first, second };
            _pairList.Add(pairs);
        }

        _chipCount = int.Parse(_textArr[_textArr.Count - 1]);

        int startPosNum = _textArr.Count - 3;
        ListCreate(startPosNum, _startPosNumList);

        int winPosNum = _textArr.Count - 2;
        ListCreate(winPosNum, _winPosNumList);
    }
    void ListCreate(int num, List<int> list)
    {
        string[] startPos = _textArr[num].Split(","[0]);
        foreach (var s in startPos)
        {
            int posNum = int.Parse(s);
            list.Add(posNum);
        }
    }

    public static List<Vector3> GetPositionList()
    {
        return _posList;
    }

    public static List<int[]> GetPairList()
    {
        return _pairList;
    }

    public static List<int> GetStartPosNumList()
    {
        return _startPosNumList;
    }

    public static List<int> GetWinPosNumList()
    {
        return _winPosNumList;
    }

    public static int GetChipCount()
    {
        return _chipCount;
    }
}
