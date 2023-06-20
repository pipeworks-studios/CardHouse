using UnityEngine;

namespace CardHouse.SampleGames.MemoryMatch
{
    public class MemoryCard : MonoBehaviour
    {
        public SpriteRenderer MySpriteRenderer;
        [HideInInspector]
        public Sprite MySprite;

        MemoryGame MyGame;

        void Start()
        {
            MyGame = MemoryGame.Instance;
            if (MyGame == null)
            {
                Debug.LogError("MemoryCards need MemoryGame component to exist in scene!");
                return;
            }
        }

        public void Apply(Sprite sprite)
        {
            MySprite = sprite;
            MySpriteRenderer.sprite = sprite;
        }

        public void OnFlippedUp()
        {
            MyGame.Flip(this);
        }
    }
}
