using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class EnemieManager : MonoBehaviour
    {
        public GameObject[] enemies;
        public Character[] character;
        public Grid grid;
        public EnemieStats eStats;
        public GameEvent ETurnEnded;

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
            if(currentState == true)
            {
                currentEnemy = 0;
                eStats.CheckForAllies();
                Vector3Int something = grid.WorldToCell(eStats.LookForClosestAlly(character[currentEnemy].transform.position));
                moveEnemy(something);
            }
        }

        public void NextCharacter()
        {
            if(currentState == true)
            {
                currentEnemy++;
                if (currentEnemy >= enemies.Length)
                    turnEnded();

                eStats.CheckForAllies();
                Vector3Int something = grid.WorldToCell(eStats.LookForClosestAlly(character[currentEnemy].transform.position));
                moveEnemy(something);
            }
        }

        public void moveEnemy(Vector3Int pos)
        {
            enemyToAttack = pos;
            if(currentState == true)
            {
                Debug.Log("pos: " + pos);
                Vector3[] moveTo = new Vector3[1];
                moveTo[0] = grid.CellToWorld(pos - new Vector3Int(0, 1, 0));
                character[currentEnemy].Move(moveTo);
            }
        }

        public void AttackAlly()
        {
            if(currentState == true)
            {
                Manager_Static.battles.DukeItOut(character[currentEnemy], eStats.mainA.GetCharacterDataAt(enemyToAttack.x, enemyToAttack.y).GetComponent<Character>());
                if (currentEnemy <= enemies.Length)
                    NextCharacter();
                else
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