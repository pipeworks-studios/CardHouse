using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse.SampleGames.Tarot
{
    public class SpreadManager : MonoBehaviour
    {
        public Text SpreadLabel;
        public CardGroup Deck;
        public GameObject SpreadOrderLabelPrefab;
        public TMP_Text Key;

        public List<TarotSpread> Spreads;
        List<GameObject> CurrentSpreadLabels = new List<GameObject>();

        int CurrentSpreadIndex = 0;

        void Start()
        {
            foreach (var spread in Spreads)
            {
                foreach (var slot in spread.Slots)
                {
                    slot.gameObject.SetActive(false);
                }
            }
            AdjustSpread(0);
        }

        public void NextSpread()
        {
            AdjustSpread(1);
        }

        public void PreviousSpread()
        {
            AdjustSpread(-1);
        }

        void AdjustSpread(int diff)
        {
            ShuffleCardsBackIn();

            foreach (var label in CurrentSpreadLabels)
            {
                Destroy(label);
            }

            foreach (var slot in Spreads[CurrentSpreadIndex].Slots)
            {
                slot.gameObject.SetActive(false);
            }

            CurrentSpreadIndex = (CurrentSpreadIndex + diff) % Spreads.Count;
            while (CurrentSpreadIndex < 0)
            {
                CurrentSpreadIndex += Spreads.Count;
            }
            SpreadLabel.text = Spreads[CurrentSpreadIndex].Name;
            Key.text = Spreads[CurrentSpreadIndex].Instructions;

            CurrentSpreadLabels.Clear();
            for (var i = 0; i < Spreads[CurrentSpreadIndex].Slots.Count; i++)
            {
                var slot = Spreads[CurrentSpreadIndex].Slots[i];
                slot.gameObject.SetActive(true);
                var label = Instantiate(SpreadOrderLabelPrefab, slot.transform);
                label.GetComponent<TMP_Text>().text = (i + 1).ToString();
                CurrentSpreadLabels.Add(label);
            }
        }

        public void ShuffleCardsBackIn()
        {
            var areCardsInPlay = false;
            foreach (var slot in Spreads[CurrentSpreadIndex].Slots)
            {
                foreach (var card in slot.MountedCards.ToList())
                {
                    Deck.Mount(card);
                    areCardsInPlay = true;
                }
            }

            if (areCardsInPlay)
            {
                Deck.Shuffle();
            }
        }

        public void DealNextCard()
        {
            if (Deck.MountedCards.Count == 0)
                return;

            Spreads[CurrentSpreadIndex].FillNext(Deck.MountedCards[Deck.MountedCards.Count - 1]);
        }
    }
}
