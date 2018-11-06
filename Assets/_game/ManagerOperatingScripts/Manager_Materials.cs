using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Manager_Materials : MonoBehaviour
    {

        public Material[] mats;
        public Material defaultMat;

        private void Awake()
        {
            Manager_Static.materialsManager = this;
        }

        public Material GetMaterial(int _index)
        {
            if (_index < mats.Length)
                return mats[_index];
            else
                return defaultMat;
        }

        public Material GetMaterial(CharacterMats _index)
        {
            if ((int)_index < mats.Length)
                return mats[(int)_index];
            else
                return defaultMat;
        }
    }
}