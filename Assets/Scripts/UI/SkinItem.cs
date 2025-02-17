using UnityEngine;

namespace CubeHopper.UI
{
    [CreateAssetMenu(fileName ="SkinItem", menuName ="ScriptableObjects/SkinItem")]
    public class SkinItem : ScriptableObject
    {
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite IdleSprite { get; private set; }
        [field: SerializeField] public Sprite FlySprite { get; private set; }
        [field: SerializeField] public Gradient TraceColor{ get; private set; }
    }
}
