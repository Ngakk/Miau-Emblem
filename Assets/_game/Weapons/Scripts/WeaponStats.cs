using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mangos
{
    public enum WeaponType
    {
        SWORD,
        BOW,
        STAFF,
        TOME
    }

    [CreateAssetMenu()]
    public class WeaponStats : ScriptableObject
    {
        public WeaponType weaponType;
        [Tooltip("Might, le añade puntos al stat de ataque del personaje")]
        public int mt;
        [Tooltip("Weight, de momento no hace nada, pero podria llegar a usarse para decrementar el acc de el personaje que la porta")]
        public int wt;
    }
}