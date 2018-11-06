using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class EnemieManager : MonoBehaviour
    {
        public Character myCharacter;
        public CharacterStats myStats;
        public Main_Algorithm mainA;
        public Character MySelf;

        private List<GameObject> enemiesInRange;

        private void Start()
        {
            MySelf = GetComponent<Character>();
        }

        public void CheckForAllies()
        {
            for(int i = 0; i < myStats.walkRange; i++)
            {
                for(int j = 0; j < myStats.walkRange; j++)
                {
                    if(mainA.GetCharacterDataAt(i, j) != null)
                    {
                        enemiesInRange.Add(mainA.GetCharacterDataAt(i,j));
                    }
                }
            }

            if(enemiesInRange.Count == 0)
            {
                CheckForAllAllies();
                LookForClosestAlly();
            }
        }

        public void SendMoveToNearestAlly()
        {

        }

        public Vector3 LookForClosestAlly()
        {
            float distance = Mathf.Infinity;
            float shortestDistance = Mathf.Infinity;
            int shortestInt = 0;
            Vector3 position = transform.position;
            if (enemiesInRange.Count == 0)
            {
                Debug.Log("No detecto a nadie. Pfff! SOY CIEGO!");
                return Vector3.zero;
            }
            else if (enemiesInRange.Count > 0)
            {
                for(int i = 0; i < enemiesInRange.Count; i++)
                {
                    distance = Vector3.Distance(transform.position, enemiesInRange[i].transform.position);
                    if(distance < shortestDistance)
                    {
                        shortestInt = i;
                        shortestDistance = distance;
                    } else
                    {
                        shortestInt = 0;
                        Debug.Log("Por alguna razón 'shortestInt' es 0");
                    }
                }
                return enemiesInRange[shortestInt].transform.position;
            }
            Debug.Log("Meh, que hueva, no entró a nada así que regresare Vector3.zero");
            return Vector3.zero;
        }

        public void CheckForAllAllies()
        {
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
        }

        public void SendMoveToManager()
        {

        }
    }
}