using UnityEngine;

public class SelectChip : MonoBehaviour
{
	public LayerMask layerMask;

	static Chip _chip;
	static GameObject _curObj;
	LayerMask _layerMaskInvert;

	void Awake()
	{
		_layerMaskInvert = layerMask.value | 1 << 2;
		_layerMaskInvert = ~_layerMaskInvert; 
	}

	public static GameObject current
	{
		get { return _curObj; }
	}

	void Select()
	{
		GameObject obj = GetObject();
		if (obj == null)
		{
			if (_chip)
			{
				_chip.Deselected();
			}
			HightLight.ClearAllHightLights();
			_curObj = null;
			return;
		}
		if (_curObj != obj)
		{
			if (_chip && obj.CompareTag("Chip"))
			{
				_chip.Deselected();
				HightLight.ClearAllHightLights();
			}
			_curObj = obj;
			if (_curObj.CompareTag("Chip"))
			{
				_chip = _curObj.GetComponent<Chip>();
				_chip.Selected();
			}
			else if (_chip)
			{
				InputControl.SetBoolTrue();
				_chip.Move(_curObj.transform.position);
				HightLight.ClearAllHightLights();
				_curObj = null;
			}
		}
	}

	GameObject GetObject() 
	{
		GameObject obj = null;
	
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, layerMask);
		foreach(RaycastHit s in hits)
        {
			if(s.collider.CompareTag("Chip"))
            {
				obj = s.transform.gameObject;
				return obj;
			}
			else if(s.collider.CompareTag("Point"))
            {
				obj = s.transform.gameObject;
			}
        }
		return obj;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !InputControl.isBloked)
		{
			Select();
		}
	}
}
