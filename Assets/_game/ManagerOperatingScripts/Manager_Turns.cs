using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Manager_Turns : MonoBehaviour
    {
        public GameState currentGameState;
        public GameObject enemyManager;
        public GameObject bearer;
        public Main_Algorithm matrix;
        private GameState previousGamestate;

        private void Awake()
        {
            Manager_Static.turnsManager = this;
        }

        public void ToggleTurn()
        {
            Invoke("nextTurn", 2.5f); 
        }

        public void SetGameState(GameState _gamestate)
        {
            currentGameState = _gamestate;
        }

        public void nextTurn()
        {
            if (currentGameState == GameState.PLAYER_TURN)
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
                                bearer.GetComponent<PlayerSelection>().ChangeColor(matrix.GetCharacterDataAt(x, y), Color.gray);
                            }
                        }
                    }
                }
            }
        }
    }
}