using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeHopper
{
    [CreateAssetMenu(fileName = "WeightData", menuName = "ScriptableObjects/WeightData")]
    public class WeightData : ScriptableObject
    {
        [SerializeField] private int _weight, _rate;
        public int Weight
        {
            get { return _weight; }
        }
        public int Rate
        {
            get { return _rate; }
        }
    }
}
