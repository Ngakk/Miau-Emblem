using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public struct BattleInfo
    {
        public List<int> attackOrder;
        public List<int> damageDealt;
        public List<int> hitOrMiss;
        public int damage1, damage2;
    }

    public class Battles : MonoBehaviour { 
    
        public static void DukeItOut(Character cat1, Character cat2)
        {
            //Calculate damage and attacks to be done
            List<int> attackOrder = CalculateAttackOrder(cat1, cat2);
            List<int> damageDealt = new List<int>();
            List<int> hitOrMiss = GenerateHitsAndMisses(attackOrder, cat1, cat2);
            int damage1 = GetDamageToDeal(cat1, cat2);
            int damage2 = GetDamageToDeal(cat2, cat1);
            int hp1 = cat1.stats.hp;
            int hp2 = cat2.stats.hp;
            //Calculation
            for(int i = 0; i < attackOrder.Count; i++)
            {
                if (attackOrder[i] == 0) //Cat 1 attacks and cat 2 takes damage
                {
                    damageDealt.Add(damage1 * hitOrMiss[i]);
                    hp2 -= damage1 * hitOrMiss[i];
                }
                else //Cat 2 attacks and cat 1 takes damage
                {
                    damageDealt.Add(damage2 * hitOrMiss[i]);
                    hp1 -= damage2 * hitOrMiss[i];
                }
                if (hp1 <= 0 || hp2 <= 0)
                    break;
            }

            BattleInfo battle;
            battle.attackOrder = attackOrder;
            battle.damageDealt = damageDealt;
            battle.hitOrMiss = hitOrMiss;
            battle.damage1 = damage1;
            battle.damage2 = damage2;
            //Start animation

            cat1.fight.StartFight(battle, cat2.fight, 0, true);

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

        public static BattleInfo GetBattleInfo(Character cat1, Character cat2)
        {
            BattleInfo temp;
            temp.atackOrder = CalculateAttackOrder(cat1, cat2);
            temp.damage1 = GetDamageToDeal(cat1, cat2);
            temp.damage2 = GetDamageToDeal(cat2, cat1);

            return temp;
        }

        public static List<int> GenerateHitsAndMisses(List<int> attacks, Character cat1, Character cat2) //0 = miss, 1 = hit, 2 = crit
        {
            List<int> hits = new List<int>();
            Character[] cats = { cat1, cat2 };
            for(int i = 0; i < attacks.Count; i++)
            {
                int result = 1;
                int hitRng = Random.Range(1, 100);
                int critRng = Random.Range(1, 100);
                if (critRng < cats[attacks[i]].stats.crt)
                {
                    result = 2;
                }
                if (hitRng > cats[attacks[i]].stats.acc)
                {
                    result = 0;
                }
                hits.Add(result);

            }
            return hits;
        }
    }
}