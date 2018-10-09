using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
	public class Manager_App : MonoBehaviour {

		public AppState currentState;
		//En algun momento me tenfgo que asegurar de que el estado cambie conforme al estado del juego

		void Awake()
		{
			Manager_Static.appManager = this;
		}

		public void SetPause()
		{
			Manager_Static.appManager.currentState = AppState.PAUSE_MENU;
			Time.timeScale = 0.0f;
		}

		public void  SetPlay()
		{
			Manager_Static.appManager.currentState = AppState.GAMEPLAY;
			Time.timeScale = 1.0f;
		}

        public void SetWin()
        {
            Manager_Static.appManager.currentState = AppState.GAME_END;
            Time.timeScale = 1.0f;
        }

        public void SetCredits()
        {
            Manager_Static.appManager.currentState = AppState.CREDITS;
            Time.timeScale = 1.0f;
        }

        public void SetScores()
        {
            Manager_Static.appManager.currentState = AppState.SCORES;
            Time.timeScale = 1.0f;
        }
	}
}
