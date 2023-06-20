using UnityEngine;

namespace CardHouse
{
    public class SeekerSet
    {
        public Card Card;
        public Seeker<Vector3> Homing;
        public Seeker<float> Turning;
        public Seeker<float> Scaling;
        public float FlipSpeed = 1f;
    }
}
