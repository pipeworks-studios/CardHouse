using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    public class GroupSetup : MonoBehaviour
    {
        [Serializable]
        public struct GroupPopulationData
        {
            public CardGroup Group;
            public GameObject CardPrefab;
            public int CardCount;
        }

        public bool RunOnStart = true;

        public List<GroupPopulationData> GroupPopulationList;

        public List<CardGroup> GroupsToShuffle;

        public List<TimedEvent> OnSetupCompleteEventChain;

        void Start()
        {
            if (RunOnStart)
            {
                DoSetup();
            }
        }

        public void DoSetup()
        {
            StartCoroutine(SetupCoroutine());
        }

        IEnumerator SetupCoroutine()
        {
            var homing = new InstantVector3Seeker();
            var turning = new InstantFloatSeeker();
            var newThingDict = new Dictionary<GroupPopulationData, List<GameObject>>();
            foreach (var group in GroupPopulationList)
            {
                newThingDict[group] = new List<GameObject>();
                for (var i = 0; i < group.CardCount; i++)
                {
                    var newThing = Instantiate(group.CardPrefab, Vector3.down * 10, Quaternion.identity);
                    newThingDict[group].Add(newThing);
                }
            }

            yield return new WaitForEndOfFrame();

            foreach (var kvp in newThingDict)
            {
                foreach (var newThing in kvp.Value)
                {
                    kvp.Key.Group.Mount(newThing.GetComponent<Card>(), instaFlip: true, seekerSets: new SeekerSetList { new SeekerSet { Homing = homing, Turning = turning } });
                }
            }

            foreach (var group in GroupsToShuffle)
            {
                group.Shuffle(true);
            }

            StartCoroutine(TimedEvent.ExecuteChain(OnSetupCompleteEventChain));
        }
    }
}
