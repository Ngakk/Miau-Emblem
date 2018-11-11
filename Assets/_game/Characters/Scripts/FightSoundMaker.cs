using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class FightSoundMaker : MonoBehaviour
    {
        public void OnSwordSwing()
        {
            Manager_Static.audioManager.PlaySoundGlobal(sounds.SWORD);
        }

        public void OnFireFired()
        {
            Manager_Static.audioManager.PlaySoundGlobal(sounds.FIRE);
        }

        public void OnHeal()
        {
            Manager_Static.audioManager.PlaySoundGlobal(sounds.HEALING);
        }

        public void OnMiss()
        {
            Manager_Static.audioManager.PlaySoundGlobal(sounds.STEP);
        }

        public void OnNoDamage()
        {
            Manager_Static.audioManager.PlaySoundGlobal(sounds.BREAK);
        }
    }
}