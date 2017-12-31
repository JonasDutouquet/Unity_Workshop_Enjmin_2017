using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResponseControl
{
	[System.Serializable]
	public class Input
	{
		public string name = "New Input";
		public Axes axis;
		public bool isStateDependent = false;
		public List<ResponseCurve> curves = new List<ResponseCurve>();

		public Input()
		{
			curves.Add (new ResponseCurve());
		}
	}
}