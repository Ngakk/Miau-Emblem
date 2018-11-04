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
        PHYSICAL,
        MAGICAL
    }

    [CreateAssetMenu()]
    public class CharacterStats : ScriptableObject {
        public CharacterClass charClass;
        public DamageType damageType;
        public int maxHp;   //Max Health points
        public int hp;      //Current health points
        public int atk;     //Attack damage
        public int def;     //Defense from physical attacks
        public int spd;     //Speed
        public int res;     //Resistance for magical attacks
        [Range(0, 100)]
        public int evs;     //Evasion
        [Range(0, 100)]
        public int acc;     //Accuaracy
        [Range(0, 100)]
        public int crt;     //Critical chance
        public int[] attackRanges; //At wich ranges can it attack
        public int[] counterAttackRanges; //At wich ranges can it counter attack
        public Team team;
        public int walkRange;
    }
}