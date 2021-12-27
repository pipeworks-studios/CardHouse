using UnityEngine;
using UnityEngine.Events;

public class DragDetector : MonoBehaviour
{
    public GateCollection<NoParams> DragGates;
    public UnityEvent OnDragStart;

    public GateCollection<DropParams> DropGates;
    public UnityEvent OnDragEnd;

    void OnMouseDown()
    {
        if (!DragGates.AllUnlocked(null))
            return;

        OnDragStart.Invoke();
    }

    void OnMouseUp()
    {
        if (!DragGates.AllUnlocked(null))
            return;

        OnDragEnd.Invoke();
    }
}
