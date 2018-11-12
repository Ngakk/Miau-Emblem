﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Manager_Turns : MonoBehaviour
    {
        public GameState currentGameState;
        public GameObject enemyManager;
        public Main_Algorithm matrix;
        private GameState previousGamestate;

        private void Awake()
        {
            Manager_Static.turnsManager = this;
        }

        public void ToggleTurn()
        {
            nextTurn(currentGameState); 
        }

        public void SetGameState(GameState _gamestate)
        {
            currentGameState = _gamestate;
        }

        public void nextTurn(GameState _state)
        {
            if (_state == GameState.PLAYER_TURN)
            {
                currentGameState = GameState.ENEMY_TURN;
                enemyManager.GetComponent<EnemieManager>().StartEnemyTurn();
            }
            else
            {
                currentGameState = GameState.PLAYER_TURN;
                for (int x = 0; x < matrix.matrix.GetLength(0); x++)
                {
                    for (int y = 0; y < matrix.matrix.GetLength(1); y++)
                    {
                        if (matrix.GetCharacterDataAt(x, y) != null)
                        {
                            if (matrix.GetCharacterDataAt(x, y).CompareTag("Ally"))
                            {
                                matrix.GetCharacterDataAt(x, y).GetComponent<Character>().canMove = true;
                            }
                        }
                    }
                }
            }
        }
    }
}