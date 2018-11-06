using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Mangos
{
	public class Manager_Input : MonoBehaviour {


		void Awake()
		{
            //SE OCUOPA DECIRLEA AL MANAGER STATIC QUIEN ES SI MANAGER DE INPUTS
			Manager_Static.inputManager = this;

            
		}

		void Update()
  	{
            //CODIGO DE LOS INPUTS DEPENDIENDO DEL ESTADO DEL JUEGO

            //ENTRA EN ESTE IF SI EL ESTADO DE LA APLICACION ESTA EN GAMEPLAY
            if (Manager_Static.appManager.currentState == AppState.MAIN_MENU)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Manager_Static.appManager.SetPause();
                }
            }

            //ENTRA EN ESTE IF SI EL ESTADO DE LA APLICACION ESTA EN PAUSA
            else if (Manager_Static.appManager.currentState == AppState.PAUSE_MENU)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Manager_Static.appManager.SetPlay();
                }
            }

            //ENTRA EN ESTE IF SI EL ESTADO DE LA APLICACION ESTA EN GAME END
            else if (Manager_Static.appManager.currentState == AppState.GAME_END)
            {
                if (Input.anyKeyDown)
                {
                    Manager_Static.appManager.SetScores();
                    Manager_Static.sceneManager.LoadScene("PLACEHOLDER_SCORES", false);
                }
            }

            //ENTRA EN ESTE IF SI EL ESTADO DE LA APLICACION ESTA EN EL MENU PRINCIPAL
            else if (Manager_Static.appManager.currentState == AppState.GAMEPLAY)
			{
                
			}

	        //ENTRA EN ESTE IF SI EL ESTADO DE LA APLICACION DE LA APLIACION ESTA EN FIN DEL JUEGO
	        else if (Manager_Static.appManager.currentState == AppState.GAME_END)
	        {
	        }

            else if (Manager_Static.appManager.currentState == AppState.SCORES)
            {

            }
        }
    }
}
