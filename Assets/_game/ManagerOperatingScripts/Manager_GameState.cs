using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos
{
    public class Manager_GameState : MonoBehaviour
    {
        public GameState gameState;

        private void Awake()
        {
            Manager_Static.gameStateManager = this;
        }

        public void OnWin()
        {
            Manager_Static.gameStateManager.gameState = GameState.WIN;
        }

        public void OnLose()
        {
            Manager_Static.gameStateManager.gameState = GameState.LOSE;
        }

        public void PlayerTurn()
        {
            Manager_Static.gameStateManager.gameState = GameState.PLAYER_TURN;
        }

        public void EnemieTurn()
        {
            Manager_Static.gameStateManager.gameState = GameState.ENEMIE_TURN;
        }

        public void Cinematic()
        {
            Manager_Static.gameStateManager.gameState = GameState.CINEMATIC;
        }
    }
}