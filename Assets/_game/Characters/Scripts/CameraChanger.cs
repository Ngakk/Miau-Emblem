using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mangos
{
    public class CameraChanger : MonoBehaviour
    {

        private Animator anim;

        private void Awake()
        {
            Battles.cameraChanger = this;
        }

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
            Battles.OnTransitionToBattleEnd();
        }

        public void OnTransitionToTopDownEnd()
        {
            Battles.OnTransitionToTopDownEnd();
        }
    }
}