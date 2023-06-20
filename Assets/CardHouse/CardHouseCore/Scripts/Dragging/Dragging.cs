using System;
using UnityEngine;

namespace CardHouse
{
    public class Dragging : MonoBehaviour
    {
        public float DefaultCardZ = 0f;
        public float CardPopupDistance = 1f;

        public bool UseGrabOffset;
        public Vector3 GrabOffset;
        public bool SetNewOffsetOnGrab;

        public SeekerScriptable<Vector3> DragHomingStrategy;
        Seeker<Vector3> MyStrategy;

        bool IsDragging;
        DragDetector TargetDraggable;
        Homing TargetHoming;
        Turning TargetTurning;
        float StartingZ;

        public static Dragging Instance;
        public Action<DragDetector> OnDrag;
        public Action<DragDetector> OnDrop;
        public Action<DragDetector> PostDrop;

        private void Awake()
        {
            Instance = this;
            MyStrategy = DragHomingStrategy.GetStrategy();
        }

        public void UpdateStrategy()
        {
            MyStrategy = DragHomingStrategy.GetStrategy();
        }

        public Homing GetTarget()
        {
            return IsDragging ? TargetHoming : null;
        }

        public void BeginDragging(DragDetector draggable, Homing homing, Turning turning, bool pointUpWhenDragged = true, float? startingZ = null)
        {
            TargetDraggable = draggable;
            TargetHoming = homing;
            TargetTurning = turning;
            StartingZ = startingZ ?? DefaultCardZ;
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (SetNewOffsetOnGrab)
            {
                GrabOffset = homing.transform.position - mouseWorldPosition;
            }
            if (pointUpWhenDragged)
            {
                TargetTurning.StartSeeking(Camera.main.transform.rotation.eulerAngles.z);
            }

            IsDragging = true;

            OnDrag?.Invoke(draggable);
        }

        void Update()
        {
            if (!IsDragging)
                return;

            GoToMouse(StartingZ - CardPopupDistance);
        }

        public void StopDragging()
        {
            if (!IsDragging)
                return;

            IsDragging = false;
            GoToMouse(StartingZ);
            OnDrop?.Invoke(TargetDraggable);
            PostDrop?.Invoke(TargetDraggable);
        }

        void GoToMouse(float newZ)
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (UseGrabOffset)
            {
                mouseWorldPosition += GrabOffset;
            }
            var zCorrection = Vector3.back * (mouseWorldPosition.z - newZ); // apply corrective vector to ignore "z" position of mouse
            TargetHoming.StartSeeking(mouseWorldPosition + zCorrection, MyStrategy);
        }
    }
}
