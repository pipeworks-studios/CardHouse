using UnityEngine;

namespace CardHouse.Tutorial
{
    public class SpriteOperatorTutorial : MonoBehaviour
    {
        public MultiSpriteOperator ColorOperator;
        public SpriteImageOperator ImageOperator;

        public static SpriteOperatorTutorial Instance;


        private void Awake()
        {
            Instance = this;
        }

        public void RegisterColorVote(Object voter, string vote)
        {
            ColorOperator.Activate(vote, voter);
        }

        public void RemoveColorVote(Object voter)
        {
            ColorOperator.Remove(voter);
        }

        public void RegisterImageVote(Object voter, string vote)
        {
            ImageOperator.Activate(vote, voter);
        }

        public void RemoveImageVote(Object voter)
        {
            ImageOperator.Remove(voter);
        }
    }
}
