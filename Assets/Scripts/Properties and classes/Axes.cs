using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmountOfAxes {OneAxis, TwoAxes, ThreeAxes}

[System.Serializable]
public class Axes 
{

	public AmountOfAxes amount;
	public bool xAxis = true;
	public bool yAxis = false;
	public bool zAxis = false;

	public void ChangeState(AmountOfAxes newState)
	{
		switch (newState)
		{
		case AmountOfAxes.OneAxis:
			xAxis = true;
			yAxis = false;
			zAxis = false;
			break;
		case AmountOfAxes.TwoAxes:
			xAxis = true;
			yAxis = false;
			zAxis = true;
			break;
		case AmountOfAxes.ThreeAxes:
			xAxis = true;
			yAxis = true;
			zAxis = true;
			break;
		}
	}

	public bool IsInvalid(AmountOfAxes state)
	{
		bool[] axesChecked = { xAxis, yAxis, zAxis };
		int bools = 0;
		for (int i = 0; i < axesChecked.Length; i++) 
		{
			if (axesChecked [i])
				bools += 1;
		}
		bool isInvalid = false;
		switch (state)
		{
		case AmountOfAxes.OneAxis:
			isInvalid = (bools != 1) ? true:false;
			break;
		case AmountOfAxes.TwoAxes:
			isInvalid = (bools != 2) ? true:false;
			break;
		case AmountOfAxes.ThreeAxes:
			isInvalid = (bools != 3) ? true:false;
			break;
		}
		return isInvalid;
	}
}
