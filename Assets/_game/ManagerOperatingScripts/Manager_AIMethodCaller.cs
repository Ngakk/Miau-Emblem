using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Manager_AIMethodCaller : MonoBehaviour
    {
        public Main_Algorithm mainA;

        public void Awake()
        {
            Manager_Static.aiMethodCaller = this;
        }

        public Vector3 PositionToMove(Vector3 _charPos)
        {

        }

        public Vector3 CheckAllyPosition()
        {

        }
    }
}
