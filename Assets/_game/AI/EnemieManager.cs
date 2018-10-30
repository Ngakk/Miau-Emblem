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

        private List<GameObject> enemiesInRange;

        private void Start()
        {
            
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
                SendMoveToNearestAlly();
            }
        }

        public void SendMoveToNearestAlly()
        {
            
        }

        public Vector3 LookForClosestAlly()
        {
            Vector3 closest;
            float distance = Mathf.Infinity;
            float shortestDistance = Mathf.Infinity;
            int shortestInt;
            Vector3 position = transform.position;
            Vector3[] allDistances;
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
                    }
                }
                return enemiesInRange[shortestInt].transform.position;
            }
            Debug.Log("Meh, que hueva, no entró a nada así que regresare Vector3.zero");
            return Vector3.zero;
        }

        public void CheckForAllAllies()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
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