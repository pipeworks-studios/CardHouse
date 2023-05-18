using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plant : MonoBehaviour
{
    public TMP_Text NameText;
    public TMP_Text DescriptionText;
    public SpriteRenderer Sprite;

    public List<PlantGrowthScriptable> PossiblePlants;
    List<PlantMaturityInfo> Stages;

    int WaterLevel = -1;

    private void Start()
    {
        Stages = PossiblePlants[UnityEngine.Random.Range(0, PossiblePlants.Count)].Stages;
        Water();
    }

    public void Water()
    {
        if (CanBeWatered())
        {
            WaterLevel++;
            NameText.text = Stages[WaterLevel].Name;
            DescriptionText.text = Stages[WaterLevel].Description;
            Sprite.sprite = Stages[WaterLevel].Sprite;
        }
    }

    public bool CanBeWatered()
    {
        return WaterLevel < Stages.Count - 1;
    }
}