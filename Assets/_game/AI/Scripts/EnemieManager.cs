using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class EnemieManager : MonoBehaviour
    {
        public GameObject[] enemies;
        public Character[] character;
        public Grid[] grids;
        public EnemieStats eStats;

        public int currentEnemy;
        public bool currentState;
        public int minDistance;

        private void Start()
        {
            eStats = GetComponent<EnemieStats>();
            if(Manager_Static.gameStateManager.gameState == GameState.ENEMIE_TURN)
            {
                currentState = true; //Si es true entonces es que está en turno de los enemigos.
            } else
            {
                currentState = false;
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                StartEnemyTurn();
            }
        }

        public void StartEnemyTurn()
        {
            currentEnemy = 0;
            eStats.CheckForAllies(character[currentEnemy]);
            eStats.LookForClosestAlly(Mathf.RoundToInt(character[currentEnemy].transform.position.x), Mathf.RoundToInt(character[currentEnemy].transform.position.y));
            moveEnemy(eStats.LookForClosestAlly(Mathf.RoundToInt(character[currentEnemy].transform.position.x), Mathf.RoundToInt(character[currentEnemy].transform.position.y)));
        }

        public void NextCharacter()
        {
            currentEnemy++;
            if (currentEnemy > enemies.Length)
                turnEnded();

            eStats.CheckForAllies(character[currentEnemy]);
            eStats.LookForClosestAlly(Mathf.RoundToInt(character[currentEnemy].transform.position.x), Mathf.RoundToInt(character[currentEnemy].transform.position.y));
            moveEnemy(eStats.LookForClosestAlly(Mathf.RoundToInt(character[currentEnemy].transform.position.x), Mathf.RoundToInt(character[currentEnemy].transform.position.y)));
        }

        public void moveEnemy(Vector3Int pos)
        {
            Vector3[] moveTo = new Vector3[1];
            moveTo[0] = pos;
            character[currentEnemy].Move(moveTo);
        }

        public void turnEnded()
        {
            Debug.Log("Todos terminamos de movernos");
        }
    } 
}