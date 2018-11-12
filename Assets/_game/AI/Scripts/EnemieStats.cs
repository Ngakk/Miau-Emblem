using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class EnemieStats : MonoBehaviour
    {
        public Main_Algorithm mainA;

        public List<GameObject> enemiesInRange = new List<GameObject>();


        public void CheckForAllies()
        {
            enemiesInRange.Clear();
            for (int i = 0; i < mainA.filas; i++)
            {
                for (int j = 0; j < mainA.columnas; j++)
                {
                    if (mainA.GetCharacterDataAt(i, j) != null)
                    {
                        if(mainA.GetCharacterDataAt(i, j).GetComponent<Character>().team != Team.ENEMY)
                        {
                            enemiesInRange.Add(mainA.GetCharacterDataAt(i, j));
                        }
                    }
                }
            }
            //Debug.Log("Se encontraron " + enemiesInRange.Count + " aliados.");
        }

        public void CheckForEnemies()
        {
            enemiesInRange.Clear();
            for (int i = 0; i < mainA.filas; i++)
            {
                for (int j = 0; j < mainA.columnas; j++)
                {
                    if (mainA.GetCharacterDataAt(i, j) != null)
                    {
                        if (mainA.GetCharacterDataAt(i, j).GetComponent<Character>().team == Team.ENEMY)
                        {
                            enemiesInRange.Add(mainA.GetCharacterDataAt(i, j));
                        }
                    }
                }
            }
        }

        public Vector3 LookForClosestAlly(Vector3 enemyPos)
        {
            float distance = Mathf.Infinity;
            float shortestDistance = Mathf.Infinity;
            int shortestInt = 0;
            if (enemiesInRange.Count == 0)
            {
                return Vector3.zero;
            }
            else if (enemiesInRange.Count > 0)
            {
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    distance = Vector3.Distance(enemyPos, enemiesInRange[i].transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestInt = i;
                        shortestDistance = distance;
                    }
                }
                return enemiesInRange[shortestInt].transform.position;
            }
            return Vector3.zero;
        }

        public Character getClosestCharacter(int x, int y)
        {
            GameObject closestAlly;
            Character allyChar;
            closestAlly = mainA.GetCharacterDataAt(x, y);
            Debug.Log(closestAlly);
            allyChar = closestAlly.GetComponent<Character>();
            return allyChar;
        }

        public void CheckForAllAllies()
        {
            for (int i = 0; i < mainA.filas; i++)
            {
                for (int j = 0; j < mainA.columnas; j++)
                {
                    if (mainA.GetCharacterDataAt(i, j) != null)
                    {
                        Debug.Log(mainA.GetCharacterDataAt(i, j));
                        enemiesInRange.Add(mainA.GetCharacterDataAt(i, j));
                    }
                }
            }
        }
    }
}
