using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardHouse.SampleGames.MemoryMatch
{
    public class MemoryGame : MonoBehaviour
    {
        public GameObject CardPrefab;
        public List<CardGroup> Slots;
        public List<Sprite> Sprites;

        public MemoryUI MyUI;
        public GameObject MatchEffect;

        int Matches = 0;
        float Timer;
        bool IsTimerRunning = true;

        MemoryCard FlippedCard;

        public static MemoryGame Instance;

        void Start()
        {
            Restart();
        }

        public void Restart()
        {
            foreach (var slot in Slots)
            {
                Timer = 0;
                Matches = 0;
                foreach (var card in slot.MountedCards.ToList())
                {
                    slot.UnMount(card);
                    Destroy(card.gameObject);
                }
            }

            var spritePool = Sprites.ToList();
            var cards = new List<Card>();
            for (var i = 0; i < Slots.Count / 2; i++)
            {
                if (spritePool.Count == 0)
                    break;

                var sprite = spritePool[Random.Range(0, spritePool.Count)];
                spritePool.Remove(sprite);
                for (var j = 0; j < 2; j++)
                {
                    var newCard = Instantiate(CardPrefab);
                    cards.Add(newCard.GetComponent<Card>());
                    var artHandler = newCard.GetComponent<MemoryCard>();
                    if (artHandler != null)
                    {
                        artHandler.Apply(sprite);
                    }
                }
            }

            var slotPool = Slots.ToList();

            for (var i = 0; i < cards.Count; i++)
            {
                cards[i].SetFacing(CardFacing.FaceDown, immediate: true);
                var slotIndex = Random.Range(0, slotPool.Count);
                slotPool[slotIndex].Mount(cards[i], seekerSets: new SeekerSetList { new SeekerSet { Card = cards[i], Homing = new InstantVector3Seeker(), Turning = new InstantFloatSeeker() } });
                slotPool.RemoveAt(slotIndex);
            }

            MyUI.UpdateMatches(Matches);
        }

        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (IsTimerRunning)
            {
                Timer += Time.deltaTime;
                MyUI.UpdateTimer(Timer);
            }
        }

        public void Flip(MemoryCard card)
        {
            if (card == FlippedCard)
                return;

            if (FlippedCard == null)
            {
                FlippedCard = card;
                return;
            }

            if (card.MySprite == FlippedCard.MySprite)
            {
                Matches++;
                if (Matches >= Slots.Count / 2)
                {
                    IsTimerRunning = false;
                }
                Instantiate(MatchEffect, card.transform.position + Vector3.back, card.transform.rotation);
                Instantiate(MatchEffect, FlippedCard.transform.position + Vector3.back, FlippedCard.transform.rotation);
                MyUI.UpdateMatches(Matches);
            }
            else
            {
                StartCoroutine(FlipDownAfter(1f, FlippedCard, card));
            }
            FlippedCard = null;
        }

        IEnumerator FlipDownAfter(float delay, MemoryCard card1, MemoryCard card2)
        {
            yield return new WaitForSeconds(delay);
            if (card1 != FlippedCard)
            {
                card1.GetComponent<Card>().SetFacing(CardFacing.FaceDown);
            }
            if (card2 != FlippedCard)
            {
                card2.GetComponent<Card>().SetFacing(CardFacing.FaceDown);
            }
        }
    }
}
