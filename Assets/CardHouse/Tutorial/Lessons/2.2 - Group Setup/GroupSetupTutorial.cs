using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse.Tutorial
{
    public class GroupSetupTutorial : MonoBehaviour
    {
        public GroupSetup SetupComponent;
        public CardTransferOperator PullBackOperator;
        public CardGroup Deck;

        public TMP_Text ASpadesText;
        public Slider ASpadesSlider;
        public TMP_Text QDiamondsText;
        public Slider QDiamondsSlider;
        public TMP_Text Hearts10Text;
        public Slider Hearts10Slider;
        public Toggle ShuffleToggle;

        public void Setup()
        {
            PullBackOperator.Activate();
            foreach (var card in Deck.MountedCards.ToList())
            {
                Deck.UnMount(card);
                Destroy(card.gameObject);
            }

            SetupComponent.DoSetup();
        }

        public void AdjustShuffle()
        {
            SetupComponent.GroupsToShuffle = new List<CardGroup>();
            if (ShuffleToggle.isOn)
            {
                SetupComponent.GroupsToShuffle.Add(Deck);
            }
        }

        public void AdjustASpadesSlider()
        {
            ASpadesText.text = $"Ace of Spades: {ASpadesSlider.value:0}";
            var entry = SetupComponent.GroupPopulationList[0];
            entry.CardCount = Mathf.RoundToInt(ASpadesSlider.value);
            SetupComponent.GroupPopulationList[0] = entry;
        }

        public void AdjustQDiamondsSlider()
        {
            QDiamondsText.text = $"Queen of Diamonds: {QDiamondsSlider.value:0}";
            var entry = SetupComponent.GroupPopulationList[1];
            entry.CardCount = Mathf.RoundToInt(QDiamondsSlider.value);
            SetupComponent.GroupPopulationList[1] = entry;
        }

        public void AdjustHearts10Slider()
        {
            Hearts10Text.text = $"10 of Hearts: {Hearts10Slider.value:0}";
            var entry = SetupComponent.GroupPopulationList[2];
            entry.CardCount = Mathf.RoundToInt(Hearts10Slider.value);
            SetupComponent.GroupPopulationList[2] = entry;
        }

    }
}
