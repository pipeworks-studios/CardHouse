using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse.Tutorial
{
    public class SplayTutorial : MonoBehaviour
    {
        public Slider XScaleSlider;
        public TMP_Text XScaleText;
        public Slider ArcMarginSlider;
        public TMP_Text ArcMarginText;
        public Slider XOffsetSlider;
        public TMP_Text XOffsetText;
        public Slider YOffsetSlider;
        public TMP_Text YOffsetText;

        public CardGroup Deck;
        public SplayLayout Splay;
        public SpriteRenderer Reticle;
        CardGroup Group;

        bool HasAdjustedOffset;

        void Start()
        {
            Group = Splay.GetComponent<CardGroup>();
        }

        public void AdjustXScale()
        {
            XScaleText.text = $"X Scale: {XScaleSlider.value:0.0}";
            Splay.transform.localScale += Vector3.right * (XScaleSlider.value - Splay.transform.localScale.x);
            Splay.Apply(Group.MountedCards);
        }

        public void AdjustArcMargin()
        {
            ArcMarginText.text = $"Arc Margin: {ArcMarginSlider.value:0.0}";
            Splay.ArcMargin = ArcMarginSlider.value;
            Splay.Apply(Group.MountedCards);
        }

        public void AdjustXOffset()
        {
            Reticle.enabled = true;
            XOffsetText.text = $"X Offset: {XOffsetSlider.value:0.0}";
            Splay.ArcCenterOffset += Vector2.right * (XOffsetSlider.value - Splay.ArcCenterOffset.x);
            Reticle.transform.position = Splay.transform.position + (Vector3)Splay.ArcCenterOffset;
            Splay.Apply(Group.MountedCards);
        }

        public void AdjustYOffset()
        {
            Reticle.enabled = true;
            YOffsetText.text = $"Y Offset: {YOffsetSlider.value:0.0}";
            Splay.ArcCenterOffset += Vector2.up * (YOffsetSlider.value - Splay.ArcCenterOffset.y);
            Reticle.transform.position = Splay.transform.position + (Vector3)Splay.ArcCenterOffset;
            Splay.Apply(Group.MountedCards);
        }
    }
}
