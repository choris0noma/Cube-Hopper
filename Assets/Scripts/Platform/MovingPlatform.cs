using UnityEngine;

namespace CubeHopper.Platform
{
    public class MovingPlatform : SimplePlatform
    {
        [SerializeField] private Transform _platform, _start, _end;
        [SerializeField] private float _speed;
        private int _direction = 1;
        
        private void Update()
        {
            Vector2 target = GetTarget();
            
            _platform.position = Vector2.MoveTowards(_platform.position, target, _speed * Time.deltaTime);
            float distance = (target - (Vector2)_platform.position).magnitude;

            if (distance <= 0.1) _direction *= -1;
        }
        
        private Vector2 GetTarget()
        {
            if (_direction > 0) return _start.position;
            else return _end.position;
        }

        private void OnDrawGizmos()
        {
            if (_platform != null && _end != null & _start != null) 
            { 
                Gizmos.DrawLine(_platform.position, _start.position);
                Gizmos.DrawLine(_platform.position, _end.position);
            }
        }

    }
}
