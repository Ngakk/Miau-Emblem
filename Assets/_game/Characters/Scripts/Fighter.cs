﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos {
    public class Fighter : MonoBehaviour {
        public Animator anim;
        public Fighter foe;
        public Character controller;
        public Sprite healty, damaged;

        public KeyCode debug;
        public KeyCode debug2;
        public Character enemy;

        private BattleInfo battle;
        private int step;
        private bool attacker;

        // Use this for initialization
        void Start() {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(debug))
            {
                Manager_Static.battles.DukeItOut(controller, enemy);
            }
            if (Input.GetKeyDown(debug2))
            {
                Manager_Static.battles.HealItOut(controller, enemy);
            }
        }

        public void PrepareForFight(GameObject sprite)
        {
            sprite.GetComponent<Image>().sprite = (controller.hp / controller.stats.maxHp > 0.3f) ? healty : damaged;
            anim = sprite.GetComponent<Animator>();
        }

        public void ContinueFight(BattleInfo _battle, Fighter _foe, int _step, bool _attacker)
        {
            /*Debug.Log(gameObject.name + " continue battle on step " + _step + " from " + _battle.attackOrder.Count);
            Debug.Log("attacks count: " + _battle.attackOrder.Count);
            Debug.Log("damages count: " + _battle.damageDealt.Count);*/
            if (_step < _battle.attackOrder.Count)
            {
                battle = _battle;
                step = _step;
                attacker = _attacker;
                foe = _foe;
                if (_attacker && _battle.attackOrder[_step] == 0) //This fighter is the attacker and can attack
                {
                    Attack();
                    Debug.Log("Attacker deals " + _battle.damageDealt[_step] + " to his foe");
                }
                else if(!_attacker && _battle.attackOrder[_step] == 1) //This fighter is counter attacker and can counter attack
                {
                    Attack();
                    Debug.Log("Counter attacker deals " + _battle.damageDealt[_step] + " to his foe");
                }
                else //Cant do an action, passes the 'turn' to his foe
                {
                    foe.ContinueFight(_battle, this, _step, !_attacker);
                }
            }
            else
            {
                //Battle ended
                Debug.Log("Fight finished");
                if (controller.hp <= 0)
                    Die();
                Manager_Static.battles.OnFightEnd();
            }
        }

        public void Attack()
        {
            anim.SetTrigger("Attack");
        }

        public void OnAttackApex()
        {

            switch (battle.hitOrMiss[step])
            {
                case 0:
                    foe.Dodge();
                    break;
                case 1:
                    if (battle.damageDealt[step] == 0)
                    {
                        //Do stuff for no damage
                    }
                    else
                        foe.Squirm(battle.damageDealt[step]);
                    break;
                case 2:
                    foe.Squirm(battle.damageDealt[step]);
                    //critical strike stuff
                    break;
            }
            if(battle.attackOrder[step] == 0)
                Manager_Static.uiManager.getDataCombat(gameObject, foe.gameObject);
            else
                Manager_Static.uiManager.getDataCombat(foe.gameObject, gameObject);
        }

        public void OnAttackEnd()
        {
            foe.ContinueFight(battle, this, step + 1, !attacker);
        }

        public void Squirm(int _dmg)
        {
            controller.hp -= _dmg;
            anim.SetTrigger("Squirm");
        }

        public void Dodge()
        {
            anim.SetTrigger("Dodge");
        }

        public void Die()
        {
            anim.SetTrigger("Die");
            Debug.Log("Character died");
        }

        public void OnDieEnd()
        {
            controller.OnDead();
        }

        public void Heal()
        {
            anim.SetTrigger("Heal");
        }

        public void OnHealApex()
        {
            //Do heal animation stuff
            foe.controller.hp += Mathf.FloorToInt(controller.stats.atk / 2.0f);
            if (foe.controller.hp > foe.controller.stats.maxHp)
                foe.controller.hp = foe.controller.stats.maxHp;
        }
    }
}