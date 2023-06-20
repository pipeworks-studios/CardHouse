using UnityEngine;

namespace CardHouse
{
    public abstract class CardSetup : MonoBehaviour
    {
        public abstract void Apply(CardDefinition data);
    }
}