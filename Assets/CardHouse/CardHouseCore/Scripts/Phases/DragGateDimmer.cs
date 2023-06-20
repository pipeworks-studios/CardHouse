using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(DragDetector))]
    public class DragGateDimmer : Toggleable
    {
        public MultiSpriteOperator Handler;
        public string ActiveMessage;
        public string InactiveMessage;

        DragDetector MyDraggable;

        void Start()
        {
            MyDraggable = GetComponent<DragDetector>();
        }

        public void UpdateHandler()
        {
            if (!IsActive || Handler == null)
                return;

            Handler.Activate(
                MyDraggable.DragGates.AllUnlocked(null)
                    ? ActiveMessage
                    : InactiveMessage,
                this);
        }
    }
}
