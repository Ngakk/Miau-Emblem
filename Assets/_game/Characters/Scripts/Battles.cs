using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos
{
    public struct BattleInfo
    {
        public List<int> attackOrder;
        public List<int> damageDealt;
        public List<int> hitOrMiss;
        public int damage1, damage2;
    }

    /*
    Para que un gato ataque a otro, llamen la función DukeItOut mandandole como parametros el gato atacante (cat1) y el gato atacado (cat2)
    La función se encarga de calcular todos los daños y llevar a cabo las animaciones de la pelea, no discrimina por clase, un healer puede atacar si se llama la funcion.

    Para tener un preview de la pelea usen GetBattleInfo, este regresa un struct tipo BattleInfo que contiene lo sigiente:
        - attackOrder: guarda ints con valores de 0 o 1, 0 significa que el gato atacante hace su movimiento y 1 significa que el otro gato hace el suyo.
        - damageDealt: no sirve en el preview, pero en si, simula la pelea y guarda los daños que se hacen en cada turno, incluyendo misses y crits.
        - hitOrMiss: Guarda ints de valor 0, 1 y 2, 0 significa que el ataque falló, 1 significa que el ataque acertó y 2 significa que el ataque fue critico.
        - damage1: El daño que el gato atacante le haría al gato atacado en un hit normal.
        - damage1: El daño que el gato atacado le haría al gato atacante en un hit normal.

    Para que un gato cure a otro, usen HealItOut mandandole como parametros el healer y el heleado. La funcion no discrimina por clase, un warrior podria curar a otro si se llama esta funcion.
    */

    public class Battles : MonoBehaviour {

        [Header("Settings")]
        [Tooltip("Tiempo que espera para mostrar los personajes despues de iniciar la transicion a batalla o a juego")]
        public float delay;
        [Header("Sprites de cada clase")]
        public Sprite warriorHealthy;
        public Sprite warriorDamaged;
        public Sprite mageHealthy, mageDamaged, healerHealthy, healerDamaged;

        [Header("Referencias necesarias")]
        public CameraChanger cameraChanger;
        public GameObject leftFighter, rightFighter;

        private Character fighter1, fighter2;
        private BattleInfo currentBattleInfo;

        [HideInInspector]
        private bool isFight = true;
        public bool canStartAction = true;

        private void Awake()
        {
            Manager_Static.battles = this;
        }

        public void DukeItOut(Character cat1, Character cat2)
        {
            if (!canStartAction)
                return;
            isFight = true;
            canStartAction = false;
            currentBattleInfo = GetBattleInfo(cat1, cat2);
            SetUpFighters(cat1, cat2);
            //Start animation
            Invoke("ShowFighters", delay);
            cameraChanger.ChangeToBattleScene();
            //End animation

        }

        private void SetUpFighters(Character cat1, Character cat2)
        {
            cat1.fight.controller = cat1;
            cat2.fight.controller = cat2;
            cat1.fight.PrepareForFight(leftFighter);
            cat2.fight.PrepareForFight(rightFighter);
            cat1.fight.foe = cat2.fight;
            cat2.fight.foe = cat1.fight;

            fighter1 = cat1;
            fighter2 = cat2;
            //Start animation
            Invoke("ShowFighters", delay);
            cameraChanger.ChangeToBattleScene();
            //End animation

        }

        private void ShowFighters()
        {
            Manager_Static.uiManager.getDataCombat(fighter1.gameObject, fighter2.gameObject);
            fighter1.fight.anim.SetTrigger("FadeIn");
            fighter2.fight.anim.SetTrigger("FadeIn");
        }

        private void HideFighters()
        {
            fighter1.fight.anim.SetTrigger("FadeOut");
            fighter2.fight.anim.SetTrigger("FadeOut");
        }

        //TODO: Hacer que el sprite aparezca, y tener el animator para el sprite con los eventos necesarios en ataque y daño

        public void OnTransitionToBattleEnd()
        {
            if (isFight)
                fighter1.fight.ContinueFight(currentBattleInfo, 0, true);
            else
                fighter1.fight.Heal();
        }

        public void OnTransitionToTopDownEnd()
        {
            canStartAction = true;
            fighter1 = null;
            fighter2 = null;
        }

        public void OnFightEnd()
        {
            cameraChanger.ChangeToTopDownScene();
            Invoke("HideFighters", delay);
        }

        public List<int> CalculateAttackOrder(Character cat1, Character cat2) //Regresa una lista de ints de el orden de ataque, 0 significa que ataca el gato que inició el ataque y 1 el gato que esta siendo atacado
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

        public int GetDistanceBetweenCharas(Character cat1, Character cat2) //Regresa la distancia entre 2 gatos
        {
            Debug.Log("Distance between chars is " + (Mathf.Abs(cat1.coordinates.x - cat2.coordinates.x) + Mathf.Abs(cat1.coordinates.y - cat2.coordinates.y)));
            return Mathf.Abs(cat1.coordinates.x - cat2.coordinates.x) + Mathf.Abs(cat1.coordinates.y - cat2.coordinates.y);
        }

        public int GetDamageToDeal(Character cat1, Character cat2) //Regresa el daño que le debe de hacer el gato1 al gato2
        {

            int temp = ((cat1.stats.atk+cat1.weapon.mt) - (cat1.stats.damageType == DamageType.MAGICAL ? cat2.stats.res : cat2.stats.def));
            if (temp < 0)
                temp = 0;
            return temp;
        }

        public BattleInfo GetBattleInfo(Character cat1, Character cat2)
        {
            //Calculate damage and attacks to be done
            List<int> attackOrder = CalculateAttackOrder(cat1, cat2);
            List<int> damageDealt = new List<int>();
            List<int> hitOrMiss = GenerateHitsAndMisses(attackOrder, cat1, cat2);
            int damage1 = GetDamageToDeal(cat1, cat2);
            int damage2 = GetDamageToDeal(cat2, cat1);
            int hp1 = cat1.hp;
            int hp2 = cat2.hp;
            //Calculation
            for (int i = 0; i < attackOrder.Count; i++)
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
                {
                    attackOrder.RemoveRange(i+1, attackOrder.Count-(i+1));
                    hitOrMiss.RemoveRange(i + 1, hitOrMiss.Count - (i + 1));
                    break;
                }
            }

            BattleInfo battle;
            battle.attackOrder = attackOrder;
            battle.damageDealt = damageDealt;
            battle.hitOrMiss = hitOrMiss;
            battle.damage1 = damage1;
            battle.damage2 = damage2;

            return battle;
        }

        public List<int> GenerateHitsAndMisses(List<int> attacks, Character cat1, Character cat2) //0 = miss, 1 = hit, 2 = crit
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
                if (hitRng < cats[attacks[i] == 1 ? 0 : 1].stats.evs)
                    result = 0;
                hits.Add(result);

            }
            return hits;
        }

        public void HealItOut(Character healer, Character healed)
        {
            if (!canStartAction)
                return;
            isFight = false;
            canStartAction = false;
            SetUpFighters(healer, healed);
            Invoke("ShowFighters", delay);
            cameraChanger.ChangeToBattleScene();
        }

        public Sprite GiveMeMyFuckingSprite(Character cat)
        {
            switch (cat.stats.charClass)
            {
                case CharacterClass.WARRIOR:
                    return (cat.hp / cat.stats.maxHp > 0.3f) ? warriorHealthy : warriorDamaged;
                case CharacterClass.MAGE:
                    return (cat.hp / cat.stats.maxHp > 0.3f) ? mageHealthy : mageDamaged;
                case CharacterClass.HEALER:
                    return (cat.hp / cat.stats.maxHp > 0.3f) ? healerHealthy : healerDamaged;
                default:
                    break;
            }
            return warriorHealthy;
        }
    }
}
