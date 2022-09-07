using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TxtConverter : MonoBehaviour
{
    public TextAsset textAsset;
    public static List<Vector3> posList { get; private set; }
    public static List<int[]> pairList { get; private set; }
    public static List<int> startPosNumList { get; private set; }
    public static List<int> winPosNumList { get; private set; }
    public static int chipCount { get; private set; }

    List<string> _textArr;
    
    private void Awake()
    {
        posList = new List<Vector3>();
        pairList = new List<int[]>();
        startPosNumList = new List<int>();
        winPosNumList = new List<int>();
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
            posList.Add(new Vector3(x, y, z));
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
            pairList.Add(pairs);
        }

        chipCount = int.Parse(_textArr[_textArr.Count - 1]);

        int startPosNum = _textArr.Count - 3;
        ListCreate(startPosNum, startPosNumList);

        int winPosNum = _textArr.Count - 2;
        ListCreate(winPosNum, winPosNumList);
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
}
