using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Manager_AIMethodCaller : MonoBehaviour
    {
        private int[] EnemieTeamNumber; //Con esta variable se usará cómo ID de los enemigos
        private bool[] isDead;  //Sirve para saber si el enemigo con el ID está muerto o vivo
        public int numberOfEnemies;   //El numero de enemigos que se desean.
        List<EnemieManager> Enemie;

        public void Awake()
        {
            Manager_Static.aiMethodCaller = this;
        }

        public void EnemieManager()
        {
            
        }

        public Vector3 PositionToMove(Vector3 _charPos)
        {

            return Vector3.zero;
        }
    }
}
