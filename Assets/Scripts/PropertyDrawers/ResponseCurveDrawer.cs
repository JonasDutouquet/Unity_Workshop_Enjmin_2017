using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer (typeof (ResponseCurve))]
public class ResponseCurveDrawer : PropertyDrawer
{
	public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
	{
		SerializedProperty name = prop.FindPropertyRelative ("name");
		SerializedProperty curve = prop.FindPropertyRelative ("curve");
		SerializedProperty endAttack = prop.FindPropertyRelative ("key");
		SerializedProperty speed = prop.FindPropertyRelative ("speedFactor");
		//SerializedProperty transition = prop.FindPropertyRelative ("transitionCurve");

		EditorGUI.BeginProperty (pos, label, prop);

		//draw label
		//pos = EditorGUI.PrefixLabel (pos, GUIUtility.GetControlID (FocusType.Passive), label);

		//don't make the child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 1;

		//Calculate rects
		Rect nameRect = new Rect(pos.x, pos.y, pos.width/2, pos.height/3);
		Rect curveRect = new Rect (pos.x + pos.width/2, pos.y, pos.width/2, pos.height);
		Rect endRect = new Rect (pos.x, pos.y + 2*pos.height/3, pos.width /2, pos.height/3);
		Rect speedRect = new Rect (pos.x, pos.y + pos.height/3, pos.width/2, pos.height/3);

		//Labels
		GUIContent nameLabel = new GUIContent ("Name");
		GUIContent endLabel = new GUIContent("Attack keys", "How many keys in the attack phase?");
		GUIContent speedLabel = new GUIContent("Speed Factor", "The speed factor applied to the curve.");

		//Draw fields (GUIContent.none -> no labels)
		var width = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 60f;
		EditorGUI.PropertyField (nameRect, name, nameLabel);
		EditorGUIUtility.labelWidth = width;
		EditorGUI.PropertyField (curveRect, curve, GUIContent.none);
		EditorGUI.PropertyField (speedRect, speed, speedLabel);
		//EditorGUIUtility.labelWidth = 80f;
		EditorGUI.PropertyField (endRect, endAttack, endLabel);
		//EditorGUIUtility.labelWidth = 55f;


		//EditorGUI.PropertyField (transitionRect, transition, GUIContent.none);

		//Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty ();
	}

	public override float GetPropertyHeight(SerializedProperty property,
		GUIContent label)
	{
		// Height is two times the standard height plus 20 pixels
		return base.GetPropertyHeight(property, label) * 3;
	}
}
