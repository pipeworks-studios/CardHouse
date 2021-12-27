using UnityEngine;

public abstract class Gate<T> : MonoBehaviour
{
    public abstract bool IsUnlocked(T argObject);
}
