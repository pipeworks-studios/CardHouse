using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceRegistry : MonoBehaviour
{
    public Text CurrentPlayerLabel;
    public Transform ResourceDisplayParent;
    public GameObject ResourceDisplayPrefab;

    public List<ResourceWallet> PlayerResources;

    public static ResourceRegistry Instance;

    PhaseManager MyPhaseManager;

    Dictionary<ResourceContainer, ResourceUI> ResourceUILookup = new Dictionary<ResourceContainer, ResourceUI>();

    public Action<int, ResourceWallet> OnResourcesChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MyPhaseManager = PhaseManager.Instance;
        if (MyPhaseManager == null)
        {
            Debug.LogError("Resource Registry requires a Phase Manager to work");
            return;
        }
        MyPhaseManager.OnPhaseChanged += HandlePhaseChange;

        if (PlayerResources.Count > 0)
        {
            ShowResourcesForPlayer(0);
        }
    }

    void OnDestroy()
    {
        if (MyPhaseManager != null)
        {
            MyPhaseManager.OnPhaseChanged -= HandlePhaseChange;
        }
    }

    void HandlePhaseChange(Phase newPhase)
    {
        ShowResourcesForPlayer(PhaseManager.Instance.PlayerIndex);
    }

    void ShowResourcesForPlayer(int playerIndex)
    {
        CurrentPlayerLabel.text = string.Format("Player {0}", PhaseManager.Instance.PlayerIndex + 1);

        ResourceUILookup.Clear();
        for (var i = 0; i < ResourceDisplayParent.childCount; i++)
        {
            Destroy(ResourceDisplayParent.GetChild(i).gameObject);
        }
        foreach (var resource in PlayerResources[playerIndex].Resources)
        {
            var newRow = Instantiate(ResourceDisplayPrefab, ResourceDisplayParent);
            var resourceUI = newRow.GetComponent<ResourceUI>();
            ResourceUILookup[resource] = resourceUI;
            resourceUI.Apply(resource);
        }
    }

    public int? GetResource(string name, int playerIndex)
    {
        return FindResource(name, playerIndex)?.Amount;
    }

    public int? GetResource(ResourceScriptable resourceDef, int playerIndex)
    {
        return FindResource(resourceDef.name, playerIndex)?.Amount;
    }

    ResourceContainer FindResource(string name, int playerIndex)
    {
        if (playerIndex < 0 || playerIndex >= PlayerResources.Count)
            return null;

        return PlayerResources[playerIndex].FindResource(name);
    }

    public void AdjustResource(string name, int playerIndex, int amount)
    {
        var resource = FindResource(name, playerIndex);
        if (resource != null)
        {
            resource.Adjust(amount);
            ResourceUILookup[resource].Apply(resource);
            OnResourcesChanged?.Invoke(playerIndex, PlayerResources[playerIndex]);
        }
    }

    public void Refill(string name, int playerIndex)
    {
        var resource = FindResource(name, playerIndex);
        if (resource != null)
        {
            resource.Amount = resource.HasMax ? resource.Max : resource.RefillValue;
            if (ResourceUILookup.ContainsKey(resource))
            {
                ResourceUILookup[resource].Apply(resource);
            }
            OnResourcesChanged?.Invoke(playerIndex, PlayerResources[playerIndex]);
        }
    }
}
