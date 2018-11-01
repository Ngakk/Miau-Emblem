using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mangos
{
    public enum WeaponType
    {
        SWORD,
        BOW,
        STAFF
    }

    [CreateAssetMenu()]
    public class WeaponStats : ScriptableObject
    {
        public WeaponType weaponType;
        public int mt;
        public int wt;
    }
}