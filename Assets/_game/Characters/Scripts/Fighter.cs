using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos {
    public class Fighter : MonoBehaviour {
        Animator anim;
        public Fighter foe;
        public Character controller;

        public KeyCode debug;
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
                Battles.HealItOut(controller, enemy);
            }
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
                if (controller.stats.hp <= 0)
                    Die();
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

        }

        public void OnAttackEnd()
        {
            foe.ContinueFight(battle, this, step + 1, !attacker);
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
            foe.controller.stats.hp += Mathf.FloorToInt(controller.stats.atk / 2.0f);
            if (foe.controller.stats.hp > foe.controller.stats.maxHp)
                foe.controller.stats.hp = foe.controller.stats.maxHp;
        }
    }
}