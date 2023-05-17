using UnityEngine;

[RequireComponent(typeof(Homing)), RequireComponent(typeof(Turning)), RequireComponent(typeof(DragDetector))]
public class DragOperator : MonoBehaviour
{
    public DragAction DragAction;
    public float DragSwell = 1.2f;

    public SeekerScriptableSet PresentationSeekers;

    Homing MyHoming;
    Turning MyTurning;
    Scaling MyScaling;
    DragDetector MyDragDetector;

    private void Awake()
    {
        MyHoming = GetComponent<Homing>();
        MyTurning = GetComponent<Turning>();
        MyScaling = GetComponent<Scaling>();
        MyDragDetector = GetComponent<DragDetector>();
    }

    public void SetDragState(bool newState)
    {
        if (newState)
        {
            MyScaling.StartSeeking(DragSwell);
            Dragging.Instance.BeginDragging(MyDragDetector, MyHoming, MyTurning);
        }
        else
        {
            MyScaling.StartSeeking(1f);
            Dragging.Instance.StopDragging();
        }
    }
}
