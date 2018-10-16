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

    public class WeaponStats : ScriptableObject
    {
        WeaponType weaponType;
        public int mt;
        public int[] ranges;
    }
}