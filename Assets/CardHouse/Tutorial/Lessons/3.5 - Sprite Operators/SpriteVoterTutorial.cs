using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CardHouse.Tutorial
{
    public class SpriteVoterTutorial : MonoBehaviour
    {
        public UnityEvent OnStart;

        private void Start()
        {
            OnStart?.Invoke();
        }

        public void OnColorDropdownUpdated()
        {
            var value = GetComponent<TMP_Dropdown>().value;
            switch (value)
            {
                case 0:
                    RemoveColorVote();
                    break;
                case 1:
                    RegisterColorVote("Active");
                    break;
                case 2:
                    RegisterColorVote("Dim");
                    break;
            }
        }

        public void OnImageDropdownUpdated()
        {
            var value = GetComponent<TMP_Dropdown>().value;
            switch (value)
            {
                case 0:
                    RemoveImageVote();
                    break;
                case 1:
                    RegisterImageVote("Bat");
                    break;
                case 2:
                    RegisterImageVote("Spider");
                    break;
            }
        }

        void RegisterColorVote(string vote)
        {
            SpriteOperatorTutorial.Instance.RegisterColorVote(this, vote);
        }

        void RemoveColorVote()
        {
            SpriteOperatorTutorial.Instance.RemoveColorVote(this);
        }

        void RegisterImageVote(string vote)
        {
            SpriteOperatorTutorial.Instance.RegisterImageVote(this, vote);
        }

        void RemoveImageVote()
        {
            SpriteOperatorTutorial.Instance.RemoveImageVote(this);
        }
    }
}
