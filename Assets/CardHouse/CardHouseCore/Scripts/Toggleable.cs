using UnityEngine;

namespace CardHouse
{
    public class Toggleable : MonoBehaviour
    {
        public bool IsActive = true;

        public void SetIsActive(bool newValue)
        {
            IsActive = newValue;
        }
    }
}
