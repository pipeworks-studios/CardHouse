using System;
using System.Collections.Generic;
using UnityEngine;

public class GroupSetup : MonoBehaviour
{
    [Serializable]
    public struct GroupPopulationData
    {
        public CardGroup Group;
        public GameObject CardPrefab;
        public int CardCount;
    }

    public List<GroupPopulationData> GroupPopulationList;

    public List<CardGroup> GroupsToShuffle;

    public List<TimedEvent> OnSetupCompleteEventChain;

    void Start()
    {
        var homing = new InstantVector3Seeker();
        var turning = new InstantFloatSeeker();
        foreach (var group in GroupPopulationList)
        {
            for (var i = 0; i < group.CardCount; i++)
            {
                var newThing = Instantiate(group.CardPrefab);
                group.Group.Mount(newThing.GetComponent<Card>(), instaFlip: true, seekerSets: new SeekerSetList { new SeekerSet { Homing = homing, Turning = turning } });
            }
        }

        foreach (var group in GroupsToShuffle)
        {
            group.Shuffle(true);
        }

        StartCoroutine(TimedEvent.ExecuteChain(OnSetupCompleteEventChain));
    }
}
