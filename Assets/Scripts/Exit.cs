using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeHopper
{
    public class Exit : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape)) 
            {
                Quit();
            }
        }

        private void Quit()
        {
            Application.Quit();

        }
    }
}
