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
            Debug.Log("StartEnemyTurn");
            if (currentEnemy < enemies.Length)
            {
                if (enemies[currentEnemy] == null)
                {
                    NextCharacter();
                    return;
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
            else
            {
                turnEnded();
            }
        }

        public void NextCharacter()
        {
            Debug.Log("Next Character Start");
            currentEnemy++;
            if (currentEnemy >= enemies.Length)
            {
                turnEnded();
                return;
            }
            if (enemies[currentEnemy] == null)
            {
                NextCharacter();
                return;
            }

            eStats.CheckForAllies();
            Vector3Int something = grid.WorldToCell(eStats.LookForClosestAlly(enemies[currentEnemy].GetComponent<Character>().transform.position));
            int[,] tempMatrix = eStats.mainA.ViewMove(enemies[currentEnemy].GetComponent<Character>().coordinates.x, enemies[currentEnemy].GetComponent<Character>().coordinates.y);
            if (tempMatrix[something.x, something.y] <= enemies[currentEnemy].GetComponent<Character>().stats.walkRange)
                moveEnemy(something);
            else
                NextCharacter();
        }

        public void moveEnemy(Vector3Int pos)
        {
            enemyToAttack = pos;
            if(currentState == true)
            {
                //Debug.Log("pos: " + pos);
                Character temp = enemies[currentEnemy].GetComponent<Character>();
                Vector3[] moveTo = new Vector3[1];
                moveTo[0] = grid.GetCellCenterLocal(pos + (temp.stats.charClass == CharacterClass.WARRIOR ? new Vector3Int(0, 1, 0) : new Vector3Int(0, 2, 0)));
                temp.Move(moveTo);
            }
        }

        public void AttackAlly()
        {
            if(currentState == true)
            {
                if (enemies[currentEnemy].GetComponent<Character>().stats.charClass == CharacterClass.HEALER)
                    Manager_Static.battles.HealItOut(enemies[currentEnemy].GetComponent<Character>(), enemies[Random.Range(1, 2)].GetComponent<Character>());
                else
                {

                    Manager_Static.battles.DukeItOut(enemies[currentEnemy].GetComponent<Character>(), eStats.mainA.GetCharacterDataAt(enemyToAttack.x, enemyToAttack.y).GetComponent<Character>());
                }
                if (currentEnemy >= enemies.Length)
                    turnEnded();
            }
        }

        public void turnEnded()
        {
            currentState = false;
            Manager_Static.turnsManager.ToggleTurn();
            Debug.Log("Turn ended");
        }
    }
}
