using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    [SerializeField] GameObject _gameClear;

    public void SetActivGameClear()
    {
        _gameClear.SetActive(true);
    }
}
