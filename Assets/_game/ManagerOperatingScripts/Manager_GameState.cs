﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos
{
    public class Manager_GameState : MonoBehaviour
    {
        public Main_Algorithm mainMatrix;

        private void Awake()
        {
            Manager_Static.gameStateManager = this;
            mainMatrix.ResizeMatrix();
        }

        private void Start()
        {
            
        }

        void Update()
        {
            
        }

        public void OnWin()
        {
            
        }

        public void OnLose()
        {
            
        }
    }
}