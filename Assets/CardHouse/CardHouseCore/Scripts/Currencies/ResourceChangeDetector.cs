using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceChangeDetector : MonoBehaviour
{
    public UnityEvent OnResourceChange;
    ResourceRegistry MyResourceRegistry;
    
    void Start()
    {
        MyResourceRegistry = ResourceRegistry.Instance;
        if (MyResourceRegistry != null)
        {
            MyResourceRegistry.OnResourcesChanged += HandleResourcesChanged;
        }
    }

    void OnDestroy()
    {
        if (MyResourceRegistry != null)
        {
            MyResourceRegistry.OnResourcesChanged -= HandleResourcesChanged;
        }
    }

    void HandleResourcesChanged(int playerIndex, ResourceWallet newResources)
    {
        OnResourceChange.Invoke();
    }
}
