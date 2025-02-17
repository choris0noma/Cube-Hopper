using CubeHopper.Game;
using System;
using UnityEngine;

namespace CubeHopper.CameraModule
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        public static Action<bool> OnCameraStop;
        private float OFFSET;
        private void OnEnable()
        {
            Player.OnLand += LerpToPlayer;
        }
        private void OnDisable()
        {
            Player.OnLand -= LerpToPlayer;
        }
        private void Awake()
        {
            OFFSET = Camera.main.orthographicSize * 1.5f / 3;
        }
        private void LerpToPlayer(Vector2 landPos)
        {
            transform.LeanMove(new Vector2(0, landPos.y + OFFSET), 1f).setEaseOutQuint().setOnComplete(
                    () => OnCameraStop?.Invoke(true)
                );
        }
        
        //want to impose my beatles taste to everyone
    }
}
