using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class EnemieManager : MonoBehaviour
    {
        [Tooltip("SIEMPRE PONER EL HEALER PRIMERO. Aquí pones los prefabs de los enemigos.")]
        public GameObject[] enemies;
        public Grid grid;
        public EnemieStats eStats;
        public GameEvent ETurnEnded;
        public GameEvent Win;

        public int currentEnemy;
        public bool currentState;
        public int minDistance;

        private Vector3Int enemyToAttack;
        private int deadEnemies;

        private void Start()
        {
            eStats = GetComponent<EnemieStats>();
            currentState = false;
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                currentState = true;
                StartEnemyTurn();
            }
        }

        public void StartEnemyTurn()
        {
          currentState = true;
            for(int i = 0; i < enemies.Length; i++)
            {
                if(enemies[i] == null)
                {
                    deadEnemies++;
                }
            }
            if(currentState == true)
            {
                if(deadEnemies >= enemies.Length)
                {
                    Win.Raise();
                }
                else if (enemies[currentEnemy] == null)
                {
                    NextCharacter();
                }
                currentEnemy = 0;
                eStats.CheckForAllies();
                Vector3Int something = grid.WorldToCell(eStats.LookForClosestAlly(enemies[currentEnemy].GetComponent<Character>().transform.position));
                int[,] tempMatrix = eStats.mainA.ViewMove(enemies[currentEnemy].GetComponent<Character>().coordinates.x, enemies[currentEnemy].GetComponent<Character>().coordinates.y);
                if (tempMatrix[something.x, something.y] <= enemies[currentEnemy].GetComponent<Character>().stats.walkRange)
                    moveEnemy(something);
                else
                    NextCharacter();
            }
        }

        public void NextCharacter()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] == null)
                {
                    deadEnemies++;
                }
            }
            if (currentEnemy > enemies.Length)
                turnEnded();
            else if (currentState == true)
            {
                if (deadEnemies >= enemies.Length)
                {
                    Win.Raise();
                } else if (currentEnemy >= enemies.Length && enemies[currentEnemy] == null)
                {
                    turnEnded();
                } else if (enemies[currentEnemy] == null)
                {
                    NextCharacter();
                }
                currentEnemy++;
                if (currentEnemy >= enemies.Length)
                {
                    turnEnded();
                    return;
                }

                eStats.CheckForAllies();
                Vector3Int something = grid.WorldToCell(eStats.LookForClosestAlly(enemies[currentEnemy].GetComponent<Character>().transform.position));
                moveEnemy(something);
            }
        }

        public void moveEnemy(Vector3Int pos)
        {
            enemyToAttack = pos;
            if(currentState == true)
            {
                //Debug.Log("pos: " + pos);
                Vector3[] moveTo = new Vector3[1];
                moveTo[0] = grid.CellToWorld(pos - new Vector3Int(0, 1, 0));
                enemies[currentEnemy].GetComponent<Character>().Move(moveTo);
            }
        }

        public void AttackAlly()
        {
            if(currentState == true)
            {
                if (enemies[currentEnemy].GetComponent<Character>().stats.charClass == CharacterClass.HEALER)
                    Manager_Static.battles.HealItOut(enemies[currentEnemy].GetComponent<Character>(), enemies[Random.Range(1, 2)].GetComponent<Character>());
                else
                    Manager_Static.battles.DukeItOut(enemies[currentEnemy].GetComponent<Character>(), eStats.mainA.GetCharacterDataAt(enemyToAttack.x, enemyToAttack.y).GetComponent<Character>());

                if (currentEnemy >= enemies.Length)
                    turnEnded();
            }
        }

        public void turnEnded()
        {
            currentState = false;
            Manager_Static.turnsManager.ToggleTurn();
        }
    }
}
