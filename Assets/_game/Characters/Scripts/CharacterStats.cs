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

    [CreateAssetMenu()]
    public class CharacterStats : ScriptableObject {
        public CharacterClass charClass;
        public int hp;
        public int atk;
        public int def;
        public int spd;
        public int res;
        public int[] attackRanges; //At wich ranges can it attack
        public int[] counterAttackRanges; //At wich ranges can it counter attack
        public WeaponStats weapon;
        public int team;
        public int walkRange;
        public int[,] position;
    }
}