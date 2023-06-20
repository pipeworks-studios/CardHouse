using System.Collections.Generic;
using UnityEngine;

namespace CardHouse.SampleGames.Solitaire
{
    [RequireComponent(typeof(Card))]
    public class SolitaireCardDragHandler : MonoBehaviour
    {
        Card MyCard;

        List<Transform> MyChildren = new List<Transform>();

        void Awake()
        {
            MyCard = GetComponent<Card>();
        }

        public void AttachChildren()
        {
            MyChildren.Clear();
            for (var i = MyCard.Group.MountedCards.IndexOf(MyCard) + 1; i < MyCard.Group.MountedCards.Count; i++)
            {
                var childTransform = MyCard.Group.MountedCards[i].transform;
                childTransform.parent = MyCard.transform;
                MyChildren.Add(childTransform);
            }
        }

        public void DetatchChildren()
        {
            foreach (var child in MyChildren)
            {
                child.parent = null;
                var childCard = child.GetComponent<Card>();
                if (childCard.Group != MyCard.Group)
                {
                    MyCard.Group.Mount(childCard);
                }
            }
            MyChildren.Clear();
        }
    }
}
