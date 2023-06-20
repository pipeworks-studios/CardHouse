using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    public class GroupRegistry : MonoBehaviour
    {
        [Serializable]
        public class NamedGroup
        {
            public int PlayerIndex;
            public GroupName Name;
            public CardGroup Group;
        }

        public List<NamedGroup> Groups = new List<NamedGroup>();

        public static GroupRegistry Instance;

        void Awake()
        {
            Instance = this;
        }

        public CardGroup Get(GroupName name, int? playerIndex)
        {
            foreach (var group in Groups)
            {
                if ((playerIndex == null || group.PlayerIndex == playerIndex) && group.Name == name)
                {
                    return group.Group;
                }
            }
            return null;
        }

        public GroupName GetGroupName(CardGroup group)
        {
            foreach (var namedGroup in Groups)
            {
                if (namedGroup.Group == group)
                {
                    return namedGroup.Name;
                }
            }
            return GroupName.None;
        }

        public Loyalty GetLoyalty(CardGroup group, int playerIndex)
        {
            var ownerIndex = GetOwnerIndex(group);
            if (ownerIndex == null)
                return Loyalty.None;

            return ownerIndex == playerIndex ? Loyalty.Self : Loyalty.Other;
        }

        public int? GetOwnerIndex(CardGroup group)
        {
            foreach (var namedGroup in Groups)
            {
                if (namedGroup.Group == group)
                {
                    return namedGroup.PlayerIndex;
                }
            }

            return null;
        }
    }
}
