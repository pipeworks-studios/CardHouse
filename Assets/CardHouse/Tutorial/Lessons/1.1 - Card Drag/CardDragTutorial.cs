using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse.Tutorial
{
    public class CardDragTutorial : MonoBehaviour
    {
        public Slider DragSwellSlider;
        public TMP_Text DragSwellText;
        public Slider SeekerGainSlider;
        public TMP_Text SeekerGainText;
        public Toggle GrabOffsetToggle;
        public Slider XOffsetSlider;
        public TMP_Text XOffsetText;
        public Slider YOffsetSlider;
        public TMP_Text YOffsetText;

        public Card Card;

        bool HasInteractedWithSwellSlider;

        private void Start()
        {
            ((ExponentialVector3SeekerScriptable)Dragging.Instance.DragHomingStrategy).XYGain = 12;
            GameObject.Find("SwellOutline").transform.localScale = Vector3.one * DragSwellSlider.value;
        }

        public void AdjustDragSwellSlider()
        {
            HasInteractedWithSwellSlider = true;
            Card.GetComponent<DragOperator>().DragSwell = DragSwellSlider.value;
            DragSwellText.text = $"Drag Swell: x{DragSwellSlider.value:0.0}";
            GameObject.Find("SwellOutline").transform.localScale = Vector3.one * DragSwellSlider.value;
            GameObject.Find("SwellOutline").GetComponent<SpriteRenderer>().enabled = true;
            UpdateOffsetReticle();
        }

        public void AdjustSeekerGainSlider()
        {
            ((ExponentialVector3SeekerScriptable)Dragging.Instance.DragHomingStrategy).XYGain = SeekerGainSlider.value;
            Dragging.Instance.UpdateStrategy();
            SeekerGainText.text = $"Seeker Gain: {SeekerGainSlider.value:0.0}";
        }

        public void OnGrabOffsetToggled()
        {
            Dragging.Instance.SetNewOffsetOnGrab = GrabOffsetToggle.isOn;
            XOffsetSlider.interactable = !GrabOffsetToggle.isOn;
            YOffsetSlider.interactable = !GrabOffsetToggle.isOn;
            GameObject.Find("Reticle").GetComponent<SpriteRenderer>().enabled = !GrabOffsetToggle.isOn;
            GameObject.Find("SwellReticle").GetComponent<SpriteRenderer>().enabled = !GrabOffsetToggle.isOn;
            if (!GrabOffsetToggle.isOn)
            {
                Dragging.Instance.GrabOffset.x = 0;
                Dragging.Instance.GrabOffset.y = 0;
            }
        }

        public void AdjustOffsetX()
        {
            Dragging.Instance.GrabOffset.x = XOffsetSlider.value;
            XOffsetText.text = $"X Offset: {XOffsetSlider.value:0.0}";
            UpdateOffsetReticle();
        }

        public void AdjustOffsetY()
        {
            Dragging.Instance.GrabOffset.y = YOffsetSlider.value;
            XOffsetText.text = $"Y Offset: {YOffsetSlider.value:0.0}";
            UpdateOffsetReticle();
        }

        void UpdateOffsetReticle()
        {
            GameObject.Find("Reticle").transform.localPosition = new Vector3(-XOffsetSlider.value, -YOffsetSlider.value, 0);
            GameObject.Find("SwellReticle").transform.localPosition = new Vector3(-1f / DragSwellSlider.value * XOffsetSlider.value, -1f / DragSwellSlider.value * YOffsetSlider.value, 0);
        }

        public void ShowSwellOutline()
        {
            StartCoroutine(ShowSwellOutlineAfter(1f));
        }

        IEnumerator ShowSwellOutlineAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (HasInteractedWithSwellSlider && Dragging.Instance.GetTarget() == null)
            {
                GameObject.Find("SwellOutline").GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
