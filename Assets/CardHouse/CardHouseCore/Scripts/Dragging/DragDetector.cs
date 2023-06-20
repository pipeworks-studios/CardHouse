using UnityEngine.Events;
using UnityEngine.Serialization;

namespace CardHouse
{
    public class DragDetector : Toggleable
    {
        public GateCollection<NoParams> DragGates;
        public UnityEvent OnDragStart;

        [FormerlySerializedAs("DropGates")]
        public GateCollection<DropParams> GroupDropGates;
        public GateCollection<TargetCardParams> TargetCardGates;
        public UnityEvent OnDragEnd;

        void OnMouseDown()
        {
            if (!IsActive || !DragGates.AllUnlocked(null))
                return;

            OnDragStart.Invoke();
        }

        void OnMouseUp()
        {
            if (!IsActive || !DragGates.AllUnlocked(null))
                return;

            OnDragEnd.Invoke();
        }
    }
}
