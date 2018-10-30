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

            }
        }

        public void SendMoveToNearestAlly()
        {

        }

        public void SendMoveToManager()
        {

        }
    }
}