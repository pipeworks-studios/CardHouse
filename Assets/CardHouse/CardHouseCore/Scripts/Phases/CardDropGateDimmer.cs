using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(Card), typeof(DragDetector))]
    public class CardDropGateDimmer : Toggleable
    {
        public MultiSpriteOperator Handler;
        public string ActiveMessage;
        public string InactiveMessage;

        DragDetector ThingBeingDragged;
        bool IsGroupTargetable;
        bool AmITargetable;

        Dragging MyDragging;
        Card MyCard;
        DragDetector MyDragDetector;

        void Start()
        {
            MyCard = GetComponent<Card>();
            MyDragDetector = GetComponent<DragDetector>();
            MyDragging = Dragging.Instance;
            if (MyDragging == null)
            {
                Debug.LogError("Drag Target Drop Dimmers require a Dragging to operate!");
                return;
            }

            MyDragging.OnDrag += HandleDrag;
            MyDragging.OnDrop += HandleDrop;
        }

        void OnDestroy()
        {
            if (MyDragging != null)
            {
                MyDragging.OnDrag -= HandleDrag;
                MyDragging.OnDrop -= HandleDrop;
            }
        }

        void HandleDrag(DragDetector draggable)
        {
            if (!IsActive || Handler == null)
                return;

            var draggedCard = draggable.GetComponent<Card>();
            var dragHandler = draggable.GetComponent<DragOperator>();
            if (draggedCard == null || dragHandler == null || dragHandler.DragAction != DragAction.UseOnTargetAndDiscard)
                return;

            ThingBeingDragged = draggable;

            var dropParams = new DropParams
            {
                Source = draggedCard?.Group,
                Target = MyCard.Group,
                Card = draggedCard,
                DragType = dragHandler == null ? DragAction.None : dragHandler.DragAction
            };
            IsGroupTargetable = draggable.GroupDropGates.AllUnlocked(dropParams)
                                && MyCard.Group.DropGates.AllUnlocked(dropParams);

            AmITargetable = true;
            if (dropParams.DragType == DragAction.UseOnTargetAndDiscard)
            {
                var targetCardParams = new TargetCardParams
                {
                    Source = draggedCard,
                    Target = MyCard
                };
                AmITargetable = targetCardParams.Source.GetComponent<DragDetector>().TargetCardGates.AllUnlocked(targetCardParams)
                                && targetCardParams.Target.GetComponent<DragDetector>().TargetCardGates.AllUnlocked(targetCardParams);
            }

            Handler.Activate(IsGroupTargetable && AmITargetable ? ActiveMessage : InactiveMessage, this);
        }

        void Update()
        {
            if (ThingBeingDragged == null)
                return;

            if (MyCard.Group == CardGroup.HilightedGroup)
            {
                Handler.Activate((AmITargetable && CardGroup.GetActiveCard(ThingBeingDragged) == MyCard) ? ActiveMessage : InactiveMessage, this);
            }
            else if (ThingBeingDragged != MyDragDetector)
            {
                Handler.Activate(IsGroupTargetable && AmITargetable ? ActiveMessage : InactiveMessage, this);
            }
        }

        void HandleDrop(DragDetector draggable)
        {
            if (!IsActive || Handler == null)
                return;

            ThingBeingDragged = null;
            Handler.Activate(ActiveMessage, this);
        }
    }
}
