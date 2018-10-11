using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mangos
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameEvent evento = (GameEvent)target;
            if (GUILayout.Button("Play Event"))
            {
                evento.Raise();
            }
        }
    }
}