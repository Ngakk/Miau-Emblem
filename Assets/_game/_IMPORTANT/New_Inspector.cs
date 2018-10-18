using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Main_Algorithm))]
public class ObjectBuilderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Main_Algorithm mainCosa = (Main_Algorithm)target;
		if(GUILayout.Button("Resize Matrix"))
		{
			mainCosa.resizeMatrix();
		}
		if(GUILayout.Button("Flood Map"))
		{
			mainCosa.makeMap();
		}
		if(GUILayout.Button("Print Map"))
		{
			mainCosa.ViewMap();
		}
	}
}
