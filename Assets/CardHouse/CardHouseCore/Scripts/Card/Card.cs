using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CardHouse
{
    [RequireComponent(typeof(Homing)), RequireComponent(typeof(Turning)), RequireComponent(typeof(Scaling))]
    public class Card : MonoBehaviour
    {
        [Serializable]
        public class GroupTransitionEvent
        {
            public GroupName Group;
            public UnityEvent EntryEvent;
            public UnityEvent ExitEvent;
        }

        [HideInInspector]
        public CardGroup Group;
        public Homing Homing { get; private set; }
        public Turning Turning { get; private set; }
        public Scaling Scaling { get; private set; }

        public Animator FlipAnimator;

        public bool CanBeUpsideDown;
        [Range(0f, 1f)]
        public float UpsideDownChance = 0.5f;
        public Transform RootToRotateWhenUpsideDown;

        public Homing FaceHoming;
        public Turning FaceTurning;
        public Scaling FaceScaling;

        public List<GroupTransitionEvent> GroupTransitionEvents;

        public CardFacing Facing { get { return FlipAnimator.GetBool("FaceUp") ? CardFacing.FaceUp : CardFacing.FaceDown; } }

        public UnityEvent OnFlipUp;
        public UnityEvent OnFlipDown;
        public UnityEvent OnPlay;

        public Action<Card, CardGroup> OnMount;

        bool IsFocused;

        public static Action<Card> OnCardFocused;

        void Awake()
        {
            Homing = GetComponent<Homing>();
            Turning = GetComponent<Turning>();
            Scaling = GetComponent<Scaling>();
            OnCardFocused += HandleCardFocused;
        }

        void OnDestroy()
        {
            OnCardFocused -= HandleCardFocused;
        }

        void Update()
        {
            if (IsFocused && Input.GetMouseButtonDown(0))
            {
                SetFocus(false);
            }
        }

        public void SetFacing(bool isFaceUp)
        {
            SetFacing(isFaceUp ? CardFacing.FaceUp : CardFacing.FaceDown);
        }

        public void SetFacing(CardFacing facing, bool immediate = false, float spd = 1f)
        {
            if (facing == CardFacing.None)
                return;

            FlipAnimator.SetBool("SkipAnimation", immediate);
            FlipAnimator.SetBool("FaceUp", facing == CardFacing.FaceUp);
            FlipAnimator.speed = spd;

            if (facing == CardFacing.FaceUp)
            {
                OnFlipUp?.Invoke();
            }
            else if (facing == CardFacing.FaceDown)
            {
                OnFlipDown?.Invoke();
            }
        }

        public void SetUpsideDown(bool isUpsideDown)
        {
            if (!CanBeUpsideDown)
                return;

            var currentRotation = RootToRotateWhenUpsideDown.localRotation.eulerAngles;
            currentRotation += Vector3.forward * ((isUpsideDown ? 180f : 0f) - RootToRotateWhenUpsideDown.localRotation.eulerAngles.z);

            RootToRotateWhenUpsideDown.localRotation = Quaternion.Euler(currentRotation);
        }

        public bool IsUpsideDown => CanBeUpsideDown && (Mathf.Abs(RootToRotateWhenUpsideDown.localRotation.eulerAngles.z) - 180f) < 1f;

        public void HandlePlayed()
        {
            OnPlay.Invoke();
        }

        public CardGroup GetDiscardGroup()
        {
            var ownerIndex = GroupRegistry.Instance.GetOwnerIndex(Group);
            if (ownerIndex == null && GetComponent<CardLoyalty>() != null)
            {
                ownerIndex = GetComponent<CardLoyalty>().PlayerIndex;
            }
            var discardGroup = GroupRegistry.Instance?.Get(GroupName.Discard, ownerIndex);
            return discardGroup;
        }

        public void SetFocus(bool isFocused)
        {
            IsFocused = isFocused;
            FaceHoming.StartSeeking(isFocused ? Camera.main.transform.position + Vector3.forward * 2f : Vector3.zero, useLocalSpace: !isFocused);
            FaceTurning.StartSeeking(isFocused ? Camera.main.transform.rotation.eulerAngles.z : 0, useLocalSpace: !isFocused);
            FaceScaling.StartSeeking(isFocused ? 2f * Camera.main.orthographicSize / 4f : 1f, useLocalSpace: !isFocused);
            if (isFocused)
            {
                OnCardFocused?.Invoke(this);
            }
        }

        void HandleCardFocused(Card card)
        {
            if (IsFocused && card != this)
            {
                SetFocus(false);
            }
        }

        public void ToggleFocus()
        {
            SetFocus(!IsFocused);
        }

        public void TriggerMountEvents(CardGroup group)
        {
            OnMount?.Invoke(this, group);

            var groupName = GroupRegistry.Instance?.GetGroupName(group) ?? GroupName.None;
            foreach (var eventTransition in GroupTransitionEvents)
            {
                if (eventTransition.Group == groupName)
                {
                    eventTransition.EntryEvent?.Invoke();
                    break;
                }
            }
        }

        public void TriggerUnMountEvents(GroupName group)
        {
            foreach (var eventTransition in GroupTransitionEvents)
            {
                if (eventTransition.Group == group)
                {
                    eventTransition.ExitEvent.Invoke();
                    break;
                }
            }
        }
    }
}
