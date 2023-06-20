using UnityEngine;

namespace CardHouse
{
    public class Activatable : MonoBehaviour
    {
        public void Activate()
        {
            OnActivate();
        }

        protected virtual void OnActivate() { }
    }
}
