using UnityEngine;
using UnityEngine.UI;

namespace CardHouse
{
    public class CurrencyUI : MonoBehaviour
    {
        public Image Image;
        public Text Text;

        public void Apply(CurrencyContainer resource)
        {
            Image.sprite = resource.CurrencyType.Sprite;
            var text = resource.Amount.ToString();
            if (resource.HasMax)
            {
                text += "/" + resource.Max.ToString();
            }
            Text.text = text;
        }
    }
}
