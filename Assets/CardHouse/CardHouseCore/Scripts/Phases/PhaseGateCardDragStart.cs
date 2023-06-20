using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(Card)), RequireComponent(typeof(DragOperator))]
    public class PhaseGateCardDragStart : Gate<NoParams>
    {
        Card MyCard;
        DragOperator MyDraggable;

        void Awake()
        {
            MyCard = GetComponent<Card>();
            MyDraggable = GetComponent<DragOperator>();
        }

        protected override bool IsUnlockedInternal(NoParams gateParams)
        {
            return PhaseManager.Instance.IsValidDragStart(MyCard.Group, MyDraggable.DragAction);
        }
    }
}
