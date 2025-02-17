using CubeHopper.Game;
using UnityEngine;

namespace CubeHopper.GameFlow
{
    public class DeathZoneFollow: MonoBehaviour
    {
        private float OFFSET;
        private void OnEnable()
        {
            Player.OnLand += CalculatePosition;
        }
        private void OnDisable()
        {
            Player.OnLand -= CalculatePosition;
        }

        private void Awake()
        {
            OFFSET = -(UnityEngine.Camera.main.orthographicSize + 2f);
        }

        private void CalculatePosition(Vector2 landPosition)
        {
            transform.position = new Vector3(0, landPosition.y + OFFSET, transform.position.z);
        }
    }
}
