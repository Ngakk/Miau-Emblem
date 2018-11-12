using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class EnemieStats : MonoBehaviour
    {
        public CharacterStats myStats;
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
                        enemiesInRange.Add(mainA.GetCharacterDataAt(i, j));
                    }
                }
            }
            Debug.Log("Se encontraron " + enemiesInRange.Count + " aliados.");
        }

        public void SendMoveToNearestAlly()
        {

        }

        public Vector3Int LookForClosestAlly(int x, int y)
        {
            Vector3Int enemyPos = new Vector3Int(x, y, 0);
            float distance = Mathf.Infinity;
            float shortestDistance = Mathf.Infinity;
            int shortestInt = 0;
            if (enemiesInRange.Count == 0)
            {
                return Vector3Int.zero;
            }
            else if (enemiesInRange.Count > 0)
            {
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    distance = Vector3Int.Distance(Vector3Int.RoundToInt(transform.position),Vector3Int.RoundToInt(enemiesInRange[i].transform.position));
                    if (distance < shortestDistance)
                    {
                        shortestInt = i;
                        shortestDistance = distance;
                    }
                    else
                    {
                        shortestInt = 0;
                    }
                }
                return Vector3Int.RoundToInt(enemiesInRange[shortestInt].transform.position);
            }
            Debug.Log("Meh, que hueva, no entró a nada así que regresare Vector3.zero");
            return Vector3Int.zero;
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
