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
        public GameEvent ETurnEnded;

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
            eStats.CheckForAllies();
            eStats.LookForClosestAlly(Mathf.RoundToInt(character[currentEnemy].transform.position.x), Mathf.RoundToInt(character[currentEnemy].transform.position.y));
            moveEnemy(eStats.LookForClosestAlly(Mathf.RoundToInt(character[currentEnemy].transform.position.x), Mathf.RoundToInt(character[currentEnemy].transform.position.y)));
        }

        public void NextCharacter()
        {
            currentEnemy++;
            if (currentEnemy >= enemies.Length)
                turnEnded();

            eStats.CheckForAllies();
            moveEnemy(eStats.LookForClosestAlly(Mathf.RoundToInt(character[currentEnemy].transform.position.x), Mathf.RoundToInt(character[currentEnemy].transform.position.y)));
        }

        public void moveEnemy(Vector3Int pos)
        {
            Vector3[] moveTo = new Vector3[1];
            moveTo[0] = pos - new Vector3Int(0, 1, 0);
            character[currentEnemy].Move(moveTo);
        }

        public void AttackAlly()
        {
            Manager_Static.Battles.DukeItOut(character[currentEnemy], eStats.getClosestCharacter(character[currentEnemy].coordinates.x, character[currentEnemy].coordinates.y + 1));
        }

        public void turnEnded()
        {
            ETurnEnded.Raise();
        }
    } 
}