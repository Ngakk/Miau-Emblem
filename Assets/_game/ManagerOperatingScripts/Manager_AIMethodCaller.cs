using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Manager_AIMethodCaller : MonoBehaviour
    {
        private int[] EnemieTeamNumber; //Con esta variable se usará cómo ID de los enemigos
        [Tooltip("Ingresa el evento de charWalkFinish")]
        public GameEvent charWalkFinish;    //Es para avisar de que ya terminó de caminar
        public Grid MapGrid;

        public void Awake()
        {
            Manager_Static.aiMethodCaller = this;
        }

        public void EnemieManager()
        {
            
        }

        public Vector3 PositionToMove(Vector3 _charPos)
        {
            Vector3 myPos = _charPos;
            
            return Vector3.zero;
        }
    }
}
