using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Card))]
public class MountDetector : MonoBehaviour
{
    public UnityEvent OnMount;
    Card MyCard;

    void Start()
    {
        MyCard = GetComponent<Card>();
        MyCard.OnMount += HandleMount;
    }

    void OnDestroy()
    {
        MyCard.OnMount -= HandleMount;
    }

    void HandleMount(Card card, CardGroup group)
    {
        OnMount.Invoke();
    }
}
