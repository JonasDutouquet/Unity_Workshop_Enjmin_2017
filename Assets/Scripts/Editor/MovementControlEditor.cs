using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovementControl))]
public class MovementControlEditor : Editor
{
	MovementControl t;
	SerializedObject GetTarget;
	public bool ShowInputList = false;

	void OnEnable()
	{
		t = (MovementControl)target;
		GetTarget = new SerializedObject (t);
	}

	public override void OnInspectorGUI()
	{
		GetTarget.Update ();
		//DrawDefaultInspector ();
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		SerializedProperty input = GetTarget.FindProperty ("associatedInput");
		SerializedProperty xBool = GetTarget.FindProperty ("xChecked");
		SerializedProperty yBool = GetTarget.FindProperty ("yChecked");
		SerializedProperty zBool = GetTarget.FindProperty ("zChecked");
		SerializedProperty xInput = GetTarget.FindProperty ("xInput");
		SerializedProperty yInput = GetTarget.FindProperty ("yInput");
		SerializedProperty zInput = GetTarget.FindProperty ("zInput");
		SerializedProperty useRb = GetTarget.FindProperty ("_useRb");
		SerializedProperty allowRot = GetTarget.FindProperty ("_allowRotation");
		SerializedProperty rotSpeed = GetTarget.FindProperty ("_rotateSpeed");
		SerializedProperty speed = GetTarget.FindProperty ("_currentSpeed");

		EditorGUILayout.PropertyField (input);

		EditorGUI.indentLevel = 1;

		ShowInputList = EditorGUILayout.Foldout (ShowInputList, "Input setup");
		if(ShowInputList) 
		{
			EditorGUI.BeginDisabledGroup (!xBool.boolValue);
			EditorGUILayout.PropertyField (xInput);
			if (xBool.boolValue && xInput.stringValue == "")
				EditorGUILayout.HelpBox ("Please assign an input for the X axis.", MessageType.Warning);
			EditorGUI.EndDisabledGroup ();
			EditorGUI.BeginDisabledGroup (!yBool.boolValue);
			EditorGUILayout.PropertyField (yInput);
			if (yBool.boolValue && yInput.stringValue == "")
				EditorGUILayout.HelpBox ("Please assign an input for the Y axis.", MessageType.Warning);
			EditorGUI.EndDisabledGroup ();
			EditorGUI.BeginDisabledGroup (!zBool.boolValue);
			EditorGUILayout.PropertyField (zInput);
			if (zBool.boolValue && zInput.stringValue == "")
				EditorGUILayout.HelpBox ("Please assign an input for the Z axis.", MessageType.Warning);
			EditorGUI.EndDisabledGroup ();
		}
		else if(xBool.boolValue && xInput.stringValue == "" || yBool.boolValue && yInput.stringValue == "" || zBool.boolValue && zInput.stringValue == "")
			EditorGUILayout.HelpBox ("Some inputs are not assigned!", MessageType.Error);
		

		EditorGUI.indentLevel = 0;

		EditorGUILayout.PropertyField (useRb, new GUIContent ("Use Rigidbody"));
		if (!useRb.boolValue)
			EditorGUILayout.HelpBox ("Transform.Translate will be used instead.", MessageType.Info);

		var lWidth = EditorGUIUtility.labelWidth;

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (allowRot, new GUIContent ("Avatar rotation"));
		EditorGUI.BeginDisabledGroup (!allowRot.boolValue);
		EditorGUIUtility.labelWidth = 50f;
		EditorGUILayout.PropertyField (rotSpeed, new GUIContent ("Speed"));
		EditorGUI.EndDisabledGroup ();
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.Space ();
		EditorGUIUtility.labelWidth = lWidth;
		EditorGUILayout.LabelField ("Current avatar speed", speed.floatValue.ToString ());

		GetTarget.ApplyModifiedProperties ();
	}
}