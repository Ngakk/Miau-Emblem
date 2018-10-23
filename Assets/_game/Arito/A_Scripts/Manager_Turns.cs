using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Manager_Turns : MonoBehaviour
    {
        public GameState currentGameState;

        public void ToggleTurn()
        {

            currentGameState = GameState.TRANSITION;
            
        }

    }
}