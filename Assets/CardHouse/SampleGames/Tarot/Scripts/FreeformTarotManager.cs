using CardHouse;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FreeformTarotManager : MonoBehaviour
{
    public ShuffleOperator Shuffler;
    public Text ReverseChanceLabel;
    public Slider ReverseChanceSlider;

    void Start()
    {
        ReverseChanceSlider.value = 10f;
        UpdateReverseChance(0.1f);
    }

    public void UpdateReverseChance(Single newVal)
    {
        ReverseChanceLabel.text = $"Reverse Chance:\n{ReverseChanceSlider.value:F1}%";
        Shuffler.UpsideDownChance = ReverseChanceSlider.value / 100f;
    }
}
