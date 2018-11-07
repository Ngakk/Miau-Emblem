using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class EnemieManager : MonoBehaviour
    {
        public GameObject[] enemies;
        public Character[] character;

        public int currentEnemy;
        public bool currentState;

        private void Start()
        {
            currentEnemy = 0;
            if(Manager_Static.gameStateManager.gameState == GameState.ENEMIE_TURN)
            {
                currentState = false; //Si es true entonces es que está en turno de los enemigos.
            }
        }

        public void StartEnemyTurn()
        {

        }

        public void moveEnemy()
        {
            
        }

        public void turnEnded()
        {

        }
    } 
}