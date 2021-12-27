using UnityEngine;

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

    public override bool IsUnlocked(NoParams gateParams)
    {
        return PhaseManager.Instance.IsValidDragStart(MyCard.Group, MyDraggable.DragAction);
    }
}
