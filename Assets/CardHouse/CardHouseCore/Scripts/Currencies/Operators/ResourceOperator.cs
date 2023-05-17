using UnityEngine;

public abstract class ResourceOperator : MonoBehaviour
{
    protected ResourceRegistry MyRegistry;

    void Start()
    {
        MyRegistry = ResourceRegistry.Instance;
        if (MyRegistry == null)
        {
            Debug.LogWarningFormat("{0}: Missing SpriteRenderer for Sprite Response to operate on", name);
        }
    }

    public void Activate()
    {
        if (MyRegistry == null)
            return;

        AdjustResources();
    }

    protected abstract void AdjustResources();
}
