using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeHopper.CameraModule
{
    public class CameraScaler : MonoBehaviour
    {
        public const float X_SIZE = 5.625f;

        private void Awake()
        {
            float orthoSize = X_SIZE * Screen.height / Screen.width * 0.5f;
            UnityEngine.Camera.main.orthographicSize = orthoSize;
        }
    }
}
