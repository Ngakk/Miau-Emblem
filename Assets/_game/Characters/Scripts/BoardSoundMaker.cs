using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class BoardSoundMaker : MonoBehaviour
    {
        public void OnStep()
        {
            Manager_Static.audioManager.PlaySoundAt(transform.position, sounds.STEP);
        }
    }
}