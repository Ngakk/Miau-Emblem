using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class pruebaMovimientoIndividual : MonoBehaviour
    {
        public Main_Algorithm mainA;
        public Character character;
        public Grid grid;

        public Vector3[] myMove;

        void Start()
        {
            character = GetComponent<Character>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                Debug.Log(mainA.GetCharacterDataAt(0, 0));
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector3[] temp = new Vector3[1];
                temp[0] = grid.CellToWorld(new Vector3Int(0, 0, 0)) + (grid.cellSize / 2);
                character.Move(temp);
            }

        }
    }
}