using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipFactoryAlt 
{
    private Chip _template;

    public ChipFactoryAlt()
    {
        string chipPrefabPath = "Prefabs/chip";
        _template = Resources.Load<Chip>(chipPrefabPath);
    }

    public Chip CreateChip(Vector3 pos ,Color color,Transform parent)
    {
        Chip chip = MonoBehaviour.Instantiate(this._template) as Chip;
        chip.GetComponent<MeshRenderer>().material.color = color;
        chip.transform.position = pos;
        chip.transform.SetParent(parent);

        return chip;
    }
}
