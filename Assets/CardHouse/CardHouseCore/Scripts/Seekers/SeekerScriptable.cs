using UnityEngine;

namespace CardHouse
{
    public abstract class SeekerScriptable<T> : ScriptableObject
    {
        public abstract Seeker<T> GetStrategy(params object[] args);
    }
}
