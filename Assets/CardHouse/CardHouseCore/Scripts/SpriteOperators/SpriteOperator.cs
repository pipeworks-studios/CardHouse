using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class SpriteOperator : MonoBehaviour
    {
        public string FavoredState;
        protected SpriteRenderer SpriteTarget;
        Dictionary<Object, string> Votes = new Dictionary<Object, string>();

        void Awake()
        {
            SpriteTarget = GetComponent<SpriteRenderer>();
        }

        public void Activate(string name)
        {
            Activate(name, this);
        }

        public void Activate(string name, Object voter)
        {
            Votes[voter] = name;

            if (SpriteTarget == null)
                return;

            UpdateState();
        }

        public void Remove(Object voter)
        {
            Votes.Remove(voter);

            UpdateState();
        }

        void UpdateState()
        {
            var allVotes = Votes.Values.ToList();
            if (allVotes.Contains(FavoredState) || allVotes.Count == 0)
            {
                ChangeSprite(FavoredState);
            }
            else if (AllSame(allVotes))
            {
                ChangeSprite(allVotes[0]);
            }
        }

        bool AllSame(List<string> stringList)
        {
            var counts = new Dictionary<string, int>();
            foreach (var item in stringList)
            {
                if (!counts.ContainsKey(item))
                {
                    counts[item] = 1;
                }
                else
                {
                    counts[item] += 1;
                }
            }

            return counts.Count == 1;
        }

        protected abstract void ChangeSprite(string name);
    }
}
