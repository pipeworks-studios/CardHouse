using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(Card))]
    public abstract class CardTargetCardOperator : Activatable
    {
        public SeekerScriptableSet DiscardSeekers;
        protected Card MyCard;
        protected Card Target;

        void Awake()
        {
            MyCard = GetComponent<Card>();
            CardGroup.OnCardUsedOnTarget += SetTarget;
        }

        void OnDestroy()
        {
            CardGroup.OnCardUsedOnTarget -= SetTarget;
        }

        void SetTarget(Card source, Card target)
        {
            if (source == MyCard)
            {
                Target = target;
            }
        }

        protected override void OnActivate()
        {
            ActOnTarget();
        }

        protected abstract void ActOnTarget();
    }
}
