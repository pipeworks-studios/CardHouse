using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(CardGroup))]
    public class PhaseGateCardDrop : Gate<DropParams>
    {
        CardGroup MyGroup;

        void Awake()
        {
            MyGroup = GetComponent<CardGroup>();
        }

        protected override bool IsUnlockedInternal(DropParams gateParams)
        {
            return PhaseManager.Instance?.IsValidDrag(gateParams.Source, MyGroup, gateParams.DragType) ?? true;
        }
    }
}
