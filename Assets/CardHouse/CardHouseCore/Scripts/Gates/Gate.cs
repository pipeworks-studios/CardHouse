namespace CardHouse
{
    public abstract class Gate<T> : Toggleable
    {
        public bool IsUnlocked(T argObject)
        {
            if (!IsActive)
                return true;

            return IsUnlockedInternal(argObject);
        }

        protected abstract bool IsUnlockedInternal(T argObject);
    }
}
