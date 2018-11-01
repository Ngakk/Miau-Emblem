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
                Battles.DukeItOut(controller, enemy);
            }
        }

        public void ContinueFight(BattleInfo _battle, Fighter _foe, int _step, bool _attacker)
        {
            Debug.Log(gameObject.name + " continue battle on step " + _step + " from " + _battle.attackOrder.Count);
            if(_step < _battle.attackOrder.Count)
            {
                battle = _battle;
                step = _step;
                attacker = _attacker;
                foe = _foe;
                if (_attacker && _battle.attackOrder[_step] == 0)
                {
                    Attack();
                    Debug.Log("Attacker deals " + _battle.damageDealt[_step] + " to his foe");
                }
                else if(!_attacker && _battle.attackOrder[_step] == 1)
                {
                    Attack();
                    Debug.Log("Counter attacker deals " + _battle.damageDealt[_step] + " to his foe");
                }
                else
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
    }
}