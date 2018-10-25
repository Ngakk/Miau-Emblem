using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Battles : MonoBehaviour { 
    
        public static void DukeItOut(Character cat1, Character cat2)
        {
            //Calculate damage and attacks to be done
            List<int> attackOrder = CalculateAttackOrder(cat1, cat2);
            int distance = GetDistanceBetweenCharas(cat1, cat2);
            int damage1 = GetDamageToDeal(cat1, cat2);
            int damage2 = GetDamageToDeal(cat2, cat1);
            //Calculation, perhaps temp
            for(int i = 0; i < attackOrder.Count; i++)
            {
                if (attackOrder[i] == 0)
                    cat2.stats.hp -= damage1;
                else
                    cat1.stats.hp -= damage2;
            }
            //Start animation

            //End animation
        }

        public static List<int> CalculateAttackOrder(Character cat1, Character cat2) //Regresa una lista de ints de el orden de ataque, 0 significa que ataca el gato que inició el ataque y 1 el gato que esta siendo atacado
        {
            List<int> temp = new List<int>();
            bool canCounter = false;

            temp.Add(0);
            for(int i = 0; i < cat2.stats.counterAttackRanges.Length; i++)
            {
                if(GetDistanceBetweenCharas(cat1, cat2) == cat2.stats.counterAttackRanges[i])
                {
                    temp.Add(1);
                    canCounter = true;
                    break;
                }
            }
            if (cat1.stats.spd - cat2.stats.spd >= 5)
                temp.Add(0);
            else if (cat2.stats.spd - cat1.stats.spd >= 5 && canCounter)
                temp.Add(1);

            return temp;
        }

        public static int GetDistanceBetweenCharas(Character cat1, Character cat2) //Regresa la distancia entre 2 gatos
        {
            return Mathf.Abs(cat1.coordinates.x - cat2.coordinates.x) + Mathf.Abs(cat1.coordinates.y - cat2.coordinates.y);
        }

        public static int GetDamageToDeal(Character cat1, Character cat2) //Regresa el daño que le debe de hacer el gato1 al gato2
        {
            int temp = (cat1.stats.atk - cat1.stats.damageType == DamageType.MAGICAL ? cat2.stats.res : cat2.stats.def);
            if (temp < 0)
                temp = 0;
            return temp;
        }
    }
}