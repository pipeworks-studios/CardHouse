using UnityEngine;
using UnityEngine.Events;

public class DragDetector : Toggleable
{
    public GateCollection<NoParams> DragGates;
    public UnityEvent OnDragStart;

    public GateCollection<DropParams> DropGates;
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
