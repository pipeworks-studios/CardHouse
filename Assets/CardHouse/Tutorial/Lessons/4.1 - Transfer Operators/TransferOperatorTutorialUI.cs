using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse.Tutorial
{
    public class TransferOperatorTutorialUI : MonoBehaviour
    {
        public TMP_Dropdown GrabFromDropdown;
        public TMP_Dropdown SendToDropdown;
        public TMP_Text NumberToTransferText;
        public Slider NumberToTransferSlider;
        public TMP_Text FlipSpeedText;
        public Slider FlipSpeedSlider;
        public CardTransferOperator Operator;

        public void AdjustNumberToTransfer()
        {
            NumberToTransferText.text = $"# to Transfer: {NumberToTransferSlider.value:0}";
            Operator.NumberToTransfer = Mathf.RoundToInt(NumberToTransferSlider.value);
        }

        public void AdjustFlipSpeed()
        {
            FlipSpeedText.text = $"Flip Speed: {FlipSpeedSlider.value:0.00}";
            Operator.FlipSpeed = FlipSpeedSlider.value;
        }

        public void AdjustGrabFrom()
        {
            var i = GrabFromDropdown.value;
            switch (i)
            {
                case 0:
                    Operator.GrabFrom = GroupTargetType.Last;
                    break;
                case 1:
                    Operator.GrabFrom = GroupTargetType.First;
                    break;
                case 2:
                    Operator.GrabFrom = GroupTargetType.Random;
                    break;
            }
        }

        public void AdjustSendTo()
        {
            var i = SendToDropdown.value;
            switch (i)
            {
                case 0:
                    Operator.SendTo = GroupTargetType.Last;
                    break;
                case 1:
                    Operator.SendTo = GroupTargetType.First;
                    break;
                case 2:
                    Operator.SendTo = GroupTargetType.Random;
                    break;
            }
        }
    }
}
