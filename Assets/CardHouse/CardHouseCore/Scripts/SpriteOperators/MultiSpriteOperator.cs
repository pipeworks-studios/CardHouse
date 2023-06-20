using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    public class MultiSpriteOperator : MonoBehaviour
    {
        public List<SpriteOperator> SpriteOperators;

        public void Activate(string name)
        {
            Activate(name, this);
        }

        public void Activate(string name, Object voter)
        {
            foreach (var handler in SpriteOperators)
            {
                handler?.Activate(name, voter);
            }
        }

        public void Remove(Object voter)
        {
            foreach (var handler in SpriteOperators)
            {
                handler?.Remove(voter);
            }
        }

        public void RemoveVote()
        {
            foreach (var handler in SpriteOperators)
            {
                handler?.Remove(this);
            }
        }
    }
}
