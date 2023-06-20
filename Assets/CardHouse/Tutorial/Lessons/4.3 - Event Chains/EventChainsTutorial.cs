using TMPro;
using UnityEngine;

namespace CardHouse.Tutorial
{
    public class EventChainsTutorial : MonoBehaviour
    {
        public EventChain NoChaining;
        public EventChain Chaining;
        public EventChain SafeChaining;

        public TMP_Dropdown Dropdown;

        public void StartTransition()
        {
            switch (Dropdown.value)
            {
                case 0:
                    NoChaining.Activate();
                    break;
                case 1:
                    Chaining.Activate();
                    break;
                case 2:
                    SafeChaining.Activate();
                    break;
            }
        }
    }
}
