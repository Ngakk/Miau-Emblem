using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{

    //ES EL MANAGER STATICO ES UN SCRIPT QUE SE COMUNICA CON TODOS LOS DEMAS SCRIPTS SIN IMPORTA SI ESTA EN LA ESCENA

    //ESTE ES UN ENUMERADOR QUE NOS DICE LOS ESTADOS DE LA APLICACION
    public enum AppState
    {
        MAIN_MENU,
        GAMEPLAY,
        PAUSE_MENU,
        GAME_END,
        SCORES,
        CREDITS
    }

    public enum GameState
    {
        PLAYER_TURN,
        ENEMY_TURN,
        TRANSITION
    }

    public enum CharacterMats : int
    {
        PLAYER,
        ENEMY,
        DEFAULT
    }

    //ESTE SE ENCARGARA DE MANTENER A LOS DEMAS MANAGER COMUNICADOS ENTRE ELLOS
    public static class Manager_Static
	{
		public static Manager_Input inputManager;
		public static Manager_App appManager;
		public static Manager_Scene sceneManager;
        public static Manager_Audio audioManager;
        public static Manager_GameState gameStateManager;
        public static Manager_Materials materialsManager;
	}
}
