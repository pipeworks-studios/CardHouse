using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CardHouse.SampleGames.DeckBuilder
{
    public class Health : MonoBehaviour
    {
        public TextMeshPro HealthText;
        public int HealthLevel;
        public UnityEvent OnDeath;

        void Start()
        {
            UpdateHealthText();
        }

        void UpdateHealthText()
        {
            HealthText.text = HealthLevel.ToString();
        }

        public void Change(int diff)
        {
            HealthLevel += diff;
            UpdateHealthText();

            if (HealthLevel <= 0)
            {
                OnDeath.Invoke();
            }
        }
    }
}
