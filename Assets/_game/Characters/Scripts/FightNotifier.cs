using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos
{
    public class FightNotifier : MonoBehaviour
    {
        public Fighter listener;
        public Image charaSprite;
        public Text infoText;

        public void OnAttackApex()
        {
            listener.OnAttackApex();
        }

        public void OnAttackEnd()
        {
            listener.OnAttackEnd();
        }

        public void OnHealApex()
        {
            listener.OnHealApex();
        }

        public void OnDieEnd()
        {
            listener.OnDieEnd();
        }

        public void OnHealEnd()
        {
            listener.OnHealEnd();
        }
    }
}