using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResponseControl;

[ExecuteInEditMode]
public class ResponseManager : MonoBehaviour 
{
	public List<ResponseControl.Input> inputs = new List<ResponseControl.Input> (1);
	private List<MovementControl> movements = new List<MovementControl>();

	public void AddNew()
	{
		inputs.Add (new ResponseControl.Input());
		movements.Add (gameObject.AddComponent<MovementControl> ());
	}

	public void Remove(int index)
	{
		inputs.RemoveAt (index);
		DestroyImmediate (movements[index]);
		//GetComponent<MovementControl> ().terrains.RemoveAt (index);
	}

	public void AddCurve(int inputIndex)
	{
		inputs [inputIndex].curves.Add (new ResponseCurve ());
		//if(GetComponent<MovementControl>()) GetComponent<MovementControl>().terrains.Add (new Terrain("", ));
	}

	public void RemoveCurve(int inputIndex, int curveIndex)
	{
		inputs [inputIndex].curves.RemoveAt(curveIndex);
		//if (GetComponent<MovementControl> ())
		//	GetComponent<MovementControl> ().terrains.RemoveAt (curveIndex);
	}

	#if UNITY_EDITOR
	void Update()
	{
		for(int i = 0 ; i < inputs.Count ; i++)
		{
			//inputs [i].curves [0] = inputs [i].curve;

			/*if(inputs[i].inputType == InputTypeEnum.TwoAxes)
			{
				MovementControl control;
				if (!GetComponent<MovementControl> ()) 
				{
					control = gameObject.AddComponent<MovementControl> ();
					control.associatedInput = inputs [i].name;
					control.terrains.Add (inputs [i].curve.name);
				}
				else
				{
					control = GetComponent<MovementControl> ();
					for (int c = 0; c < inputs [i].curves.Count; c++)
						if (control.terrains [c] == null)
							control.terrains.Add (inputs [i].curves [c].name);
						else
							control.terrains [c] = inputs [i].curves [c].name;

					//if(!inputs[i].axis.xAxis)

				}

			}*/

		}
	}
	#endif
}
