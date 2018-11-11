using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos {
    public class Fighter : MonoBehaviour {
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Fighter foe;
        [HideInInspector]
        public Character controller;

        [Header("Cosas para testing (borrar despues)")]
        public KeyCode fightButton;
        public KeyCode healButton;
        public Character testSubject;

        private FightNotifier fightNotifier;
        private BattleInfo battle;
        private int step;
        private bool attacker;
        private Text infoText;

        // Use this for initialization
        void Start() {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(fightButton))
            {
                Debug.Log("Debug1 button pressed");
                Manager_Static.battles.DukeItOut(controller, testSubject);
            }
            if (Input.GetKeyDown(healButton))
            {
                Debug.Log("Debug2 button pressed");
                Manager_Static.battles.HealItOut(controller, testSubject);
            }
        }

        public void PrepareForFight(GameObject sprite)
        {
            fightNotifier = sprite.GetComponent<FightNotifier>();
            fightNotifier.charaSprite.sprite = Manager_Static.battles.GiveMeMyFuckingSprite(controller);
            fightNotifier.listener = this;
            infoText = fightNotifier.infoText;
            anim = sprite.GetComponent<Animator>();
            switch (controller.stats.charClass)
            {
                case CharacterClass.MAGE:
                case CharacterClass.ARCHER:
                    anim.SetInteger("ActionId", 1);
                    break;
                case CharacterClass.WARRIOR:
                    anim.SetInteger("ActionId", 0);
                    break;
                case CharacterClass.HEALER:
                    anim.SetInteger("ActionId", 2);
                    break;
                default:
                    break;
            }
        }

        public void ContinueFight(BattleInfo _battle, int _step, bool _attacker)
        {
            /*Debug.Log(gameObject.name + " continue battle on step " + _step + " from " + _battle.attackOrder.Count);
            Debug.Log("attacks count: " + _battle.attackOrder.Count);
            Debug.Log("damages count: " + _battle.damageDealt.Count);*/
            if (_step < _battle.attackOrder.Count)
            {
                battle = _battle;
                step = _step;
                attacker = _attacker;
                fightNotifier.transform.SetAsLastSibling();
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
                    foe.ContinueFight(_battle, _step, !_attacker);
                }
            }
            else
            {
                //Battle ended
                Debug.Log("Fight finished");
                if (controller.hp <= 0)
                    Die();
                else
                    Manager_Static.battles.OnFightEnd();
            }
        }

        public void Attack()
        {
            anim.SetTrigger("Action");
        }

        public void OnAttackApex()
        {

            switch (battle.hitOrMiss[step])
            {
                case 0:             //Miss hit
                    foe.Dodge();
                    break;
                case 1:             //Normal hit
                    if (battle.damageDealt[step] == 0)
                    {
                        foe.Laugh();
                    }
                    else
                        foe.Squirm(battle.damageDealt[step]);
                    break;
                case 2:             //Critical strike
                    if (battle.damageDealt[step] == 0)
                    {
                        foe.Laugh();
                    }
                    else
                        foe.Scream(battle.damageDealt[step]);
                    break;
            }
            if(battle.attackOrder[step] == 0)
                Manager_Static.uiManager.getDataCombat(controller.gameObject, foe.controller.gameObject);
            else
                Manager_Static.uiManager.getDataCombat(foe.controller.gameObject, controller.gameObject);
        }

        public void OnAttackEnd()
        {
            foe.ContinueFight(battle, step + 1, !attacker);
        }

        public void Squirm(int _dmg)
        {
            infoText.color = Color.red;
            infoText.fontStyle = FontStyle.Normal;
            infoText.text = _dmg.ToString();
            controller.hp -= _dmg;
            if (controller.hp < 0)
                controller.hp = 0;
            anim.SetTrigger("Squirm");
            fightNotifier.charaSprite.sprite = Manager_Static.battles.GiveMeMyFuckingSprite(controller);
        }

        public void Scream(int _dmg)
        {
            infoText.color = Color.red;
            infoText.fontStyle = FontStyle.BoldAndItalic;
            infoText.text = "Crit! " + _dmg.ToString();
            controller.hp -= _dmg;
            if (controller.hp < 0)
                controller.hp = 0;
            anim.SetTrigger("Scream");
            fightNotifier.charaSprite.sprite = Manager_Static.battles.GiveMeMyFuckingSprite(controller);
        }

        public void Dodge()
        {
            infoText.color = Color.white;
            infoText.fontStyle = FontStyle.Normal;
            infoText.text = "Miss!";
            anim.SetTrigger("Dodge");
        }

        public void Die()
        {
            anim.SetTrigger("Die");
            Debug.Log("Character died");
        }

        public void OnDieEnd()
        {
            Manager_Static.battles.OnFightEnd();
            controller.OnDead();
        }

        public void OnParentDeadAnimEnd()
        {
            controller.OnDeadAnimEnd();
        }

        public void Heal()
        {
            Debug.Log("Entered Heal");
            infoText.color = Color.green;
            infoText.fontStyle = FontStyle.Normal;
            infoText.text = "+" + (Mathf.FloorToInt(controller.stats.atk / 2.0f).ToString());
            anim.SetInteger("ActionId", 2);
            anim.SetTrigger("Action");
        }

        public void OnHealApex()
        {
            //Do heal animation stuff
            foe.controller.hp += Mathf.FloorToInt(controller.stats.atk / 2.0f);
            if (foe.controller.hp > foe.controller.stats.maxHp)
                foe.controller.hp = foe.controller.stats.maxHp;
            foe.Purr();
            Manager_Static.uiManager.getDataCombat(controller.gameObject, foe.controller.gameObject);
        }

        public void Purr()
        {
            anim.SetTrigger("Purr");
            fightNotifier.charaSprite.sprite = Manager_Static.battles.GiveMeMyFuckingSprite(controller);
        }

        public void OnHealEnd()
        {
            Manager_Static.battles.OnFightEnd();
        }

        public void Laugh()
        {
            infoText.color = Color.blue;
            infoText.fontStyle = FontStyle.Normal;
            infoText.text = "No Damage!";
            anim.SetTrigger("Laugh");
        }
    }
}