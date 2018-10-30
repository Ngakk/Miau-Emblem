using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos {
    public class Fighter : MonoBehaviour {
        Animator anim;
        public Fighter foe;
        public Character controller;

        private int dmg = 0;

        // Use this for initialization
        void Start() {
            anim = GetComponent<Animator>();
        }

        public void StartFight(BattleInfo battle, Fighter foe, int step, bool attacker)
        {
            if(step < battle.attackOrder.Count)
            {
                if(attacker && battle.attackOrder[step] == 0)
                {
                    //Is attacking
                }
                else if(!attacker && battle.attackOrder[step] == 1)
                {
                    //Is counter attacking
                }
                else
                {
                    //Has no movements, dont do step++ and
                }
            }
            else
            {
                //Battle ended
            }
        }

        public void Attack(int _dmg)
        {
            dmg = _dmg;
            anim.SetTrigger("Attack");
        }

        public void Squirm(int _dmg)
        {
            controller.stats.hp -= _dmg;
            anim.SetTrigger("Squirm");
        }

        public void Dodge()
        {
            anim.SetTrigger("Dodge");
        }

        public void Die()
        {
            anim.SetTrigger("Die");
        }

        public void OnAttackApex()
        {
            foe.Squirm(dmg);
        }

        public void OnDieEnd()
        {
            controller.OnDead();
        }
    }
}