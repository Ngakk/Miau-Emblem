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
        [Tooltip("Classe de este personaje")]
        public CharacterClass charClass;
        [Tooltip("Tipo de daño que hace")]
        public DamageType damageType;
        [Tooltip("Vida máxima que puede tener")]
        public int maxHp;   //Max Health points
        [Tooltip("Vida que tiene actualmente (se resetea a el valor máximo al inicio del juego)")]
        public int hp;      //Current health points
        [Tooltip("Valor de ataque")]
        public int atk;     //Attack damage
        [Tooltip("Valor de defensa")]
        public int def;     //Defense from physical attacks
        [Tooltip("Valor de velocidad")]
        public int spd;     //Speed
        [Tooltip("Valor de resistencia")]
        public int res;     //Resistance for magical attacks
        [Tooltip("Probabilidad de evadir")]
        [Range(0, 100)]
        public int evs;     //Evasion
        [Tooltip("Probabilidad de acertar el golpe")]
        [Range(0, 100)]
        public int acc;     //Accuaracy
        [Tooltip("Probabilidad de dar golpe critico")]
        [Range(0, 100)]
        public int crt;     //Critical chance
        [Tooltip("Los diferentes rangos a los que puede iniciar un ataque")]
        public int[] attackRanges; //At wich ranges can it attack
        [Tooltip("Los diferentes rangos en los que puede contra atacar")]
        public int[] counterAttackRanges; //At wich ranges can it counter attack
        [Tooltip("Equipo al que pertenece")]
        public Team team;
        [Tooltip("Espacios que puede caminar")]
        public int walkRange;
    }
}