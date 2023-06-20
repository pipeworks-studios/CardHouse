using UnityEngine;

namespace CardHouse
{
    public class TriggerEnterRelay : Toggleable
    {
        public CardGroup Relay;

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!IsActive)
                return;

            Relay.HandleTriggerEnter2D(col);
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (!IsActive)
                return;

            Relay.HandleTriggerExit2D(col);
        }
    }
}
