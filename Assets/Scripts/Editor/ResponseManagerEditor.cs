using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResponseControl;
using UnityEditor;

[CustomEditor(typeof(ResponseManager))]
public class ResponseManagerEditor : Editor
{
	ResponseManager t;
	SerializedObject GetTarget;
	SerializedProperty ThisList;
	int ListSize;

	public bool showInputList = true;
	public bool showCurveList = true;

	void OnEnable()
	{
		t = (ResponseManager)target;
		GetTarget = new SerializedObject(t);
		ThisList = GetTarget.FindProperty("inputs"); // Find the List in our script and create a refrence of it
	}

	public override void OnInspectorGUI()
	{
		GetTarget.Update();
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		EditorGUILayout.BeginHorizontal ();

		//Foldout
		showInputList = EditorGUILayout.Foldout (showInputList, "Inputs");

		//New input button
		if(GUILayout.Button("Add New Input", GUILayout.ExpandWidth (false)))
		{
			t.AddNew ();
		}

		EditorGUILayout.EndHorizontal ();

		if(showInputList)
		{	
			
			EditorGUILayout.Space ();

			//For each input...
			for (int i = 0; i < ThisList.arraySize; i++) 
			{
				SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex (i);
				SerializedProperty name = MyListRef.FindPropertyRelative ("name");
				SerializedProperty axis = MyListRef.FindPropertyRelative ("axis");
				SerializedProperty presetList = MyListRef.FindPropertyRelative ("presets");
				SerializedProperty isDependent = MyListRef.FindPropertyRelative ("isStateDependent");
				SerializedProperty curves = MyListRef.FindPropertyRelative ("curves");
				SerializedProperty usePreset = presetList.FindPropertyRelative ("usePreset");
				SerializedProperty controlScript = presetList.FindPropertyRelative ("controlScript");

				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("'" + t.inputs[i].name + "' input", EditorStyles.boldLabel);
				//Remove this input from the List
				if (GUILayout.Button ("Delete input", GUILayout.ExpandWidth (false)))
				{
					t.Remove (i); 
					break;
				}
				EditorGUILayout.EndHorizontal ();
				EditorGUI.indentLevel = 1;

				//INPUT NAME
				EditorGUILayout.PropertyField (name);

				//INPUT PRESETS
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (usePreset);
				EditorGUI.BeginDisabledGroup (!usePreset.boolValue);
				InputPresetsEnum newPreset = (InputPresetsEnum)EditorGUILayout.EnumPopup (t.inputs[i].presets.presets, GUILayout.ExpandWidth (false));
				if(newPreset != t.inputs [i].presets.presets)
				{
					t.inputs [i].presets.presets = newPreset;
					t.ChangeInputPreset (newPreset, i);
				}
				EditorGUILayout.EndHorizontal ();
				EditorGUI.EndDisabledGroup ();
				EditorGUILayout.ObjectField (controlScript, typeof(MovementControl));

				//THE AXIS...
				EditorGUILayout.BeginHorizontal ();
				AmountOfAxes newValue = (AmountOfAxes)EditorGUILayout.EnumPopup (t.inputs [i].axis.amount, GUILayout.ExpandWidth (false));
				if(newValue != t.inputs [i].axis.amount)
				{
					t.inputs [i].axis.amount = newValue;
					t.inputs [i].axis.ChangeState (newValue);
				}
				EditorGUI.BeginDisabledGroup (newValue == AmountOfAxes.ThreeAxes);
				EditorGUILayout.PropertyField (axis);
				EditorGUI.EndDisabledGroup ();
				EditorGUILayout.EndHorizontal ();

				if(t.inputs[i].axis.IsInvalid (newValue))
				{
					EditorGUILayout.HelpBox ("Invalid amount of axes selected. Please check.", MessageType.Error);
				}
					
				EditorGUILayout.PropertyField (isDependent);

				EditorGUILayout.BeginHorizontal ();
				EditorGUI.indentLevel = 1;
				showCurveList = EditorGUILayout.Foldout (showCurveList, "Response curve(s)");
				if(isDependent.boolValue)
				{
					if (GUILayout.Button ("Add New Curve", GUILayout.ExpandWidth (false)))
					{
						t.AddCurve (i);
					}
				}
				EditorGUILayout.EndHorizontal ();

				if (showCurveList) 
				{
					//always draw the first curve...
					SerializedProperty firstCurve = curves.GetArrayElementAtIndex (0);
					SerializedProperty firstTransitionCurve = firstCurve.FindPropertyRelative ("transitionCurve");

					EditorGUILayout.LabelField ("'" + t.inputs[i].curves[0].name + "' response curve", EditorStyles.boldLabel);

					EditorGUILayout.PropertyField (firstCurve);

					if (t.inputs [i].curves [0].curve != null && t.inputs [i].curves [0].curve.length > 1)
					{
						if(t.inputs [i].curves [0].isInvalid ())
						{
							EditorGUILayout.BeginHorizontal ();

							EditorGUILayout.HelpBox ("There is a problem with the curve.", MessageType.Warning);
							if (GUILayout.Button ("Adjust Curve", GUILayout.Height (40f)))
							{
								t.inputs [i].curves [0].AdjustCurve ();
							}
							EditorGUILayout.EndHorizontal ();
						}
					} else
						EditorGUILayout.HelpBox ("Please draw a curve.", MessageType.Warning);
					
					EditorGUILayout.PropertyField (firstTransitionCurve);
					EditorGUILayout.Space ();


					//if dependent, draw the curve list.
					if (isDependent.boolValue) 
					{
						for (int c = 1; c < curves.arraySize; c++) 
						{
							SerializedProperty curve = curves.GetArrayElementAtIndex (c);
							SerializedProperty transitionCurve = curve.FindPropertyRelative ("transitionCurve");

							EditorGUILayout.BeginHorizontal ();
							EditorGUILayout.LabelField ("'" + t.inputs[i].curves[c].name + "' response curve", EditorStyles.boldLabel);
							//Remove this curve from the List
							if (GUILayout.Button ("Delete curve", GUILayout.ExpandWidth (false)))
							{
								t.RemoveCurve (i, c);
								break;
							}
							EditorGUILayout.EndHorizontal ();

							EditorGUILayout.PropertyField (curve);

							if (t.inputs[i].curves[c].curve != null &&t.inputs [i].curves [c].curve.length >1)
							{
								if (t.inputs [i].curves [c].isInvalid ())
								{
									EditorGUILayout.BeginHorizontal ();

									EditorGUILayout.HelpBox ("There is a problem with the curve.", MessageType.Warning);
									if (GUILayout.Button ("Adjust Curve")) 
									{
										t.inputs [i].curves [c].AdjustCurve ();
									}
									EditorGUILayout.EndHorizontal ();
								}
							} else
								EditorGUILayout.HelpBox ("Please draw a curve.", MessageType.Warning);

							EditorGUILayout.PropertyField (transitionCurve);

							EditorGUILayout.Space ();
						}
					}
				}
				EditorGUI.indentLevel = indent;
			}

		}

		//Apply the changes to our list
		GetTarget.ApplyModifiedProperties();
	}
}
