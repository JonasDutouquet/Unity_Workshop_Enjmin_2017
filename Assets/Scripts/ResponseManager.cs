using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResponseControl;

[ExecuteInEditMode]
public class ResponseManager : MonoBehaviour 
{
	public List<ResponseControl.Input> inputs = new List<ResponseControl.Input> (1);
	public static List<Terrain> terrains = new List<Terrain>();

	public void AddNew()
	{
		inputs.Add (new ResponseControl.Input());
	}

	public void Remove(int index)
	{
		inputs.RemoveAt (index);
		MovementControl control = GetComponent<MovementControl> ();
		if (control.associatedInput == inputs [index].name)
			DestroyImmediate (control);
	}

	public void AddCurve(int inputIndex)
	{
		inputs [inputIndex].curves.Add (new ResponseCurve ());
	}

	public void RemoveCurve(int inputIndex, int curveIndex)
	{
		inputs [inputIndex].curves.RemoveAt(curveIndex);
	}

	public void ChangeInputPreset(InputPresetsEnum newPreset, int inputIndex)
	{
		switch (newPreset)
		{
		case InputPresetsEnum.ChoosePreset:
			break;
		case InputPresetsEnum.AvatarMovement:
			if (!GetComponent<MovementControl> ())
			{
				MovementControl control = gameObject.AddComponent<MovementControl> ();
				inputs [inputIndex].presets.controlScript = control;
			}
			break;
		case InputPresetsEnum.CameraRotation:
			Debug.Log ("This preset is not implemented yet...");
			break;
		case InputPresetsEnum.AvatarJump :
			Debug.Log ("This preset is not implemented yet...");
			break;
		case InputPresetsEnum.AvatarShoot:
			Debug.Log ("This preset is not implemented yet...");
			break;
		}
	}

	void Start()
	{
		for(int i = 0 ; i < inputs.Count ; i++)
		{
			if (inputs [i].presets.presets == InputPresetsEnum.AvatarMovement && !GetComponent<MovementControl> ())
				Debug.LogWarning ("No Movement Control script attached to the gameobject " + name + ". \nPlease note that the input '" + inputs [i].name + "' won't have any effect.");
		}
	}
		
	#if UNITY_EDITOR
	void Update()
	{
		for(int i = 0 ; i < inputs.Count ; i++)
		{
			MovementControl control = (GetComponent<MovementControl> ()) ? GetComponent<MovementControl> () : null;
			if (control != null) 
			{
				control.associatedInput = inputs [i].name;
				control.xChecked = inputs [i].axis.xAxis;
				control.yChecked = inputs [i].axis.yAxis;
				control.zChecked = inputs [i].axis.zAxis;
			}
		}
	}
	#endif
}
