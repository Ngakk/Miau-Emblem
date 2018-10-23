using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Battles : MonoBehaviour { 
    
        public static void DukeItOut(Character cat1, Character cat2)
        {
            //Calculate damage and attacks to be done
            int[] attackOrder = CalculateAttackOrder(cat1, cat2);
            //Start animation
            //End animation
        }

        private static int[] CalculateAttackOrder(Character cat1, Character cat2)
        {
            int[] temp = { 0, 1 };
            return temp;
        }


    }
}