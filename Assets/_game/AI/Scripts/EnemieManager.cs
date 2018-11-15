using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class EnemieManager : MonoBehaviour
    {
        [Tooltip("SIEMPRE PONER EL HEALER PRIMERO. Aquí pones los prefabs de los enemigos.")]
        public Character[] enemies;
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
            currentEnemy = 0;

            if (currentEnemy < enemies.Length)
            {
                if (enemies[currentEnemy].gameObject.activeSelf == false)
                {
                    NextCharacter();
                    return;
                }
                eStats.CheckForAllies();
                Character temp = enemies[currentEnemy];
                enemyToAttack = grid.WorldToCell(eStats.LookForClosestAlly(temp.transform.position));
                Vector3Int something =  enemyToAttack + (temp.stats.charClass == CharacterClass.WARRIOR ? new Vector3Int(0, 1, 0) : new Vector3Int(0, 2, 0));
                int[,] tempMatrix = eStats.mainA.ViewMove(temp.coordinates.x, temp.coordinates.y);
                Debug.Log("Something: " + something.x + ", " + something.y);
                Debug.Log("WalkToNeeded: " + tempMatrix[something.x, something.y]);
                Debug.Log("Can walk: " + temp.stats.walkRange);
                if (tempMatrix[something.x, something.y] <= temp.stats.walkRange)
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
            if (enemies[currentEnemy].gameObject.activeSelf == false)
            {
                NextCharacter();
                return;
            }

            eStats.CheckForAllies();
            Character temp = enemies[currentEnemy];
            enemyToAttack = grid.WorldToCell(eStats.LookForClosestAlly(temp.transform.position));
            Vector3Int something = enemyToAttack + (temp.stats.charClass == CharacterClass.WARRIOR ? new Vector3Int(0, 1, 0) : new Vector3Int(0, 2, 0));
            int[,] tempMatrix = eStats.mainA.ViewMove(temp.coordinates.x, temp.coordinates.y);
            Debug.Log("Something: " + something.x + ", " + something.y);
            Debug.Log("WalkToNeeded: " + tempMatrix[something.x, something.y]);
            Debug.Log("Can walk: " + temp.stats.walkRange);
            if (tempMatrix[something.x, something.y] <= temp.stats.walkRange)
                moveEnemy(something);
            else
                NextCharacter();
        }

        public void moveEnemy(Vector3Int pos)
        {
            Debug.Log("Current enemy on move: " + currentEnemy);
            Debug.Log("Current state: " + currentState);
            if(currentState == true)
            {
                Debug.Log("moveEnemy ");
                Character temp = enemies[currentEnemy];
                Vector3[] moveTo = new Vector3[1];
                moveTo[0] = grid.GetCellCenterLocal(pos);
                temp.Move(moveTo);
            }
        }

        public void AttackAlly()
        {
            if(currentState == true)
            {
                if (enemies[currentEnemy].stats.charClass == CharacterClass.HEALER)
                    Manager_Static.battles.HealItOut(enemies[currentEnemy], enemies[Random.Range(1, 2)]);
                else
                {
                    Debug.Log("Current enemy before dukeitout: " + currentEnemy);
                    Manager_Static.battles.DukeItOut(enemies[currentEnemy], eStats.mainA.GetCharacterDataAt(enemyToAttack.x, enemyToAttack.y).GetComponent<Character>());
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
