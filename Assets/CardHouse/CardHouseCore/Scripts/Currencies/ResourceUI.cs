using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    public Image Image;
    public Text Text;

    public void Apply(ResourceContainer resource)
    {
        Image.sprite = resource.ResourceType.Sprite;
        var text = resource.Amount.ToString();
        if (resource.HasMax)
        {
            text += "/" + resource.Max.ToString();
        }
        Text.text = text;
    }
}
