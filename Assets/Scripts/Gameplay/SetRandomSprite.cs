using UnityEngine;
using Random = UnityEngine.Random;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class SetRandomSprite : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] sprites = new Sprite[0];

        private void Start()
        {
            if (TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            }
        }
    }
}