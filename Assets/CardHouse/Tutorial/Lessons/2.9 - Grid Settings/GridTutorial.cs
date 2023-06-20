using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse.Tutorial
{
    public class GridTutorial : MonoBehaviour
    {
        public Slider CardsPerRowSlider;
        public TMP_Text CardsPerRowText;
        public Slider CardLimitSlider;
        public TMP_Text CardLimitText;
        public Slider XScaleSlider;
        public TMP_Text XScaleText;
        public Slider YScaleSlider;
        public TMP_Text YScaleText;

        public CardGroup Deck;
        public CardGridLayout Grid;
        CardGroup Group;

        void Start()
        {
            Group = Grid.GetComponent<CardGroup>();
        }

        public void AdjustCardsPerRow()
        {
            CardsPerRowText.text = $"Cards Per Row: {CardsPerRowSlider.value:0}";
            Grid.CardsPerRow = Mathf.RoundToInt(CardsPerRowSlider.value);
            Grid.Apply(Group.MountedCards);
        }

        public void AdjustCardLimit()
        {
            CardLimitText.text = $"Card Limit: {CardLimitSlider.value:0}";
            Grid.CardLimit = Mathf.RoundToInt(CardLimitSlider.value);
            while (Group.MountedCards.Count > Grid.CardLimit)
            {
                Deck.Mount(Group.Get());
            }
            Grid.Apply(Group.MountedCards);
        }

        public void AdjustXScale()
        {
            XScaleText.text = $"X Scale: {XScaleSlider.value:0.0}";
            Grid.transform.localScale += Vector3.right * (XScaleSlider.value - Grid.transform.localScale.x);
            Grid.Apply(Group.MountedCards);
        }

        public void AdjustYScale()
        {
            YScaleText.text = $"Y Scale: {YScaleSlider.value:0.0}";
            Grid.transform.localScale += Vector3.up * (YScaleSlider.value - Grid.transform.localScale.y);
            Grid.Apply(Group.MountedCards);
        }

    }
}
