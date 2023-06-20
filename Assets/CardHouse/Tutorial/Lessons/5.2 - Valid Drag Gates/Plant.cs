using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardHouse.Tutorial
{
    public class Plant : MonoBehaviour
    {
        public TMP_Text NameText;
        public TMP_Text DescriptionText;
        public SpriteRenderer Sprite;
        public GameObject CostJewel;
        public TMP_Text CostText;

        public List<PlantGrowthScriptable> PossiblePlants;
        List<PlantMaturityInfo> Stages;

        public int Value = 10;

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

                if (!CanBeWatered() && CostJewel != null)
                {
                    CostJewel.SetActive(true);
                    CostText.text = Value.ToString();
                }
            }
        }

        public void HideCost()
        {
            CostJewel.SetActive(false);
        }

        public void Payoff()
        {
            CurrencyRegistry.Instance.AdjustCurrency("Gold", PhaseManager.Instance.PlayerIndex, Value);
        }

        public bool CanBeWatered()
        {
            return WaterLevel < Stages.Count - 1;
        }
    }
}