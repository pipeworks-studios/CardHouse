namespace CardHouse
{
    public class InstantFloatSeeker : Seeker<float>
    {
        public override Seeker<float> MakeCopy()
        {
            return new InstantFloatSeeker();
        }

        public override float Pump(float currentValue, float TimeSinceLastFrame)
        {
            return End;
        }

        public override bool IsDone(float currentValue)
        {
            return true;
        }
    }
}
