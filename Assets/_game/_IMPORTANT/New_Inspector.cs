using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mangos
{
    [CustomEditor(typeof(Main_Algorithm))]
    public class ObjectBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Main_Algorithm mainCosa = (Main_Algorithm)target;
            if (GUILayout.Button("Resize Matrix"))
            {
                mainCosa.ResizeMatrix();
            }
            if (GUILayout.Button("Flood Map"))
            {
                mainCosa.MakeMap();
            }
            if (GUILayout.Button("Print Map"))
            {
                mainCosa.ViewMap();
            }
        }
    }
}