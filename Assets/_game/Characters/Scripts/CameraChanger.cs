using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mangos
{
    public class CameraChanger : MonoBehaviour
    {

        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void ChangeToBattleScene()
        {
            anim.SetTrigger("BattleOn");
        }

        public void ChangeToTopDownScene()
        {
            anim.SetTrigger("BattleOff");
        }

        public void OnTransitionToBattleEnd()
        {
            Manager_Static.battles.OnTransitionToBattleEnd();
        }

        public void OnTransitionToTopDownEnd()
        {
            Manager_Static.battles.OnTransitionToTopDownEnd();
        }
    }
}