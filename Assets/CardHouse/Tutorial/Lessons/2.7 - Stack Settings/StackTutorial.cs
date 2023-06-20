using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse.Tutorial
{
    public class StackTutorial : MonoBehaviour
    {
        public Slider XOffsetSlider;
        public TMP_Text XOffsetText;
        public Slider YOffsetSlider;
        public TMP_Text YOffsetText;

        public StackLayout Stack;

        public void AdjustXOffset()
        {
            SetXOffset(XOffsetSlider.value);
        }

        public void AdjustYOffset()
        {
            SetYOffset(YOffsetSlider.value);
        }

        void SetXOffset(float value)
        {
            XOffsetText.text = $"X Offset: {value:0.000}";
            Stack.MarginalCardOffset += Vector3.right * (value - Stack.MarginalCardOffset.x);
            XOffsetSlider.value = value;
            Stack.Apply(Stack.GetComponent<CardGroup>().MountedCards);
        }
        void SetYOffset(float value)
        {
            YOffsetText.text = $"Y Offset: {value:0.000}";
            Stack.MarginalCardOffset += Vector3.up * (value - Stack.MarginalCardOffset.y);
            YOffsetSlider.value = value;
            Stack.Apply(Stack.GetComponent<CardGroup>().MountedCards);
        }

        public void UseColumnPreset()
        {
            SetXOffset(0);
            SetYOffset(-0.2f);
        }

        public void UseDeckPreset()
        {
            SetXOffset(0.03f);
            SetYOffset(0.03f);
        }

        public void UseCompactDeckPreset()
        {
            SetXOffset(0.003f);
            SetYOffset(0.003f);
        }

        public void UseRowPreset()
        {
            SetXOffset(1f);
            SetYOffset(0f);
        }
    }
}
