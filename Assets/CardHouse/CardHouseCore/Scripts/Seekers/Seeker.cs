namespace CardHouse
{
    public abstract class Seeker<T>
    {
        protected T Start;
        public T End;

        public abstract Seeker<T> MakeCopy();

        public void StartSeeking(T from, T to)
        {
            Start = from;
            End = to;
        }

        public abstract T Pump(T currentValue, float TimeSinceLastFrame);

        public abstract bool IsDone(T currentValue);
    }
}
