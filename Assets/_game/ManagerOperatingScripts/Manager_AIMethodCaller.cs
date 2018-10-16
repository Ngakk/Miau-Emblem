using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Manager_AIMethodCaller : MonoBehaviour
    {

        public void Awake()
        {
            Manager_Static.aiMethodCaller = this;
        }
    }
}
