using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    [CreateAssetMenu()]
    public class EnemieStats : MonoBehaviour
    {
        public int hp;
        public int atk;
        public int def;
        public int spd;
        public int res;
        public WeaponStats weapon;
        public int team;
        public int walkRange;
    }
}
