using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Manager_Turns : MonoBehaviour
    {
        public GameState currentGameState;
        public GameObject enemyManager;
        public GameObject matrix;
        private GameState previousGamestate;

        private void Awake()
        {
            Manager_Static.turnsManager = this;
        }

        public void ToggleTurn()
        {
            StartCoroutine(nextTurn(currentGameState));
            currentGameState = GameState.TRANSITION; 
        }

        public void SetGameState(GameState _gamestate)
        {
            currentGameState = _gamestate;
        }

        IEnumerator nextTurn(GameState _state)
        {
            yield return new WaitForSeconds(2.5f);
            if (_state == GameState.PLAYER_TURN)
            {
                enemyManager.GetComponent<EnemieManager>().StartEnemyTurn();
                currentGameState = GameState.ENEMY_TURN;
            }
            else
            {
                for (int x = 0; x < matrix.GetComponent<Main_Algorithm>().matrix.GetLength(0); x++)
                {
                    for (int y = 0; y < matrix.GetComponent<Main_Algorithm>().matrix.GetLength(1); y++)
                    {
                        if (matrix.GetComponent<Main_Algorithm>().GetCharacterDataAt(x, y).CompareTag("Ally"))
                        {
                            matrix.GetComponent<Main_Algorithm>().GetCharacterDataAt(x, y).GetComponent<Character>().canMove = true;
                        }
                    }
                }
                
                currentGameState = GameState.PLAYER_TURN;
            }
        }
    }
}