using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(CardGroup))]
    public class GroupDropGateDimmer : MonoBehaviour
    {
        public SpriteOperator Handler;
        public string ActiveMessage;
        public string InactiveMessage;

        CardGroup MyGroup;
        Dragging MyDragging;

        void Start()
        {
            MyGroup = GetComponent<CardGroup>();
            MyDragging = Dragging.Instance;
            if (MyDragging == null)
            {
                Debug.LogError("Droppability Dimmers need the Dragging script to exist!");
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

        void HandleDrag(DragDetector dragDetector)
        {
            if (Handler == null)
                return;

            var draggable = dragDetector.GetComponent<DragOperator>();
            var card = dragDetector.GetComponent<Card>();
            if (draggable == null || card == null)
            {
                Debug.LogWarningFormat("{0}: Dropped object {1} needs DragHandler and Card components to use GroupDropGateDimmer", gameObject, dragDetector.gameObject);
                return;
            }

            var dropParams = new DropParams
            {
                Source = card?.Group,
                Target = MyGroup,
                Card = card,
                DragType = draggable == null ? DragAction.None : draggable.DragAction
            };

            var gatesUnlocked = MyGroup.DropGates.AllUnlocked(dropParams)
                                && dragDetector.GroupDropGates.AllUnlocked(dropParams);

            Handler.Activate(
                gatesUnlocked && MyGroup.HasRoom()
                    ? ActiveMessage
                    : InactiveMessage,
                this);
        }

        void HandleDrop(DragDetector draggable)
        {
            if (Handler == null)
                return;

            Handler.Activate(InactiveMessage, this);
        }
    }
}
