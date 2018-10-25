using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public enum CharacterClass : int {
        WARRIOR,
        HEALER,
        ARCHER,
        MAGE
    }

    public enum DamageType
    {
        PHYSiCAL,
        MAGICAL
    }

    [CreateAssetMenu()]
    public class CharacterStats : ScriptableObject {
        public CharacterClass charClass;
        public DamageType damageType;
        public int hp;
        public int atk;
        public int def;
        public int spd;
        public int res;
        public int[] attackRanges; //At wich ranges can it attack
        public int[] counterAttackRanges; //At wich ranges can it counter attack
        public WeaponStats weapon;
        public Team team;
        public int walkRange;
    }
}