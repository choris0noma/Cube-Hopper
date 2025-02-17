using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeHopper
{
    public class Delay : MonoBehaviour
    {
        float time, currentTime;
        Action action;

        public static Delay Create(float time, Action action)
        {
            Delay del = new GameObject().AddComponent<Delay>();
            del.time = time;
            del.action = action;
            return del;
        }

        private void Update()
        {
            if (currentTime > time)
            {
                action();
                Destroy(gameObject);
            }
        }
        private void FixedUpdate()
        {
            currentTime += Time.deltaTime;
        }
    }
}
