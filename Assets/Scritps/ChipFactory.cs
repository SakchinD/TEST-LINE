using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipFactory 
{
    ChipNMAgentCotnrol _template;

    public ChipFactory()
    {
        string chipPrefabPath = "Prefabs/chip";
        _template = Resources.Load<ChipNMAgentCotnrol>(chipPrefabPath);
    }

    public ChipNMAgentCotnrol CreateChip(Vector3 pos ,Color color,Transform parent)
    {
        ChipNMAgentCotnrol chip = MonoBehaviour.Instantiate(this._template) as ChipNMAgentCotnrol;
        chip.GetComponent<MeshRenderer>().material.color = color;
        chip.transform.position = pos;
        chip.transform.SetParent(parent);

        return chip;
    }
}
