using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse.Tutorial
{
    public class MultiBoardTutorial : MonoBehaviour
    {
        [Serializable]
        public class InstructionImagePair
        {
            public Sprite Image;
            [TextArea]
            public string Text;
        }

        public MultiplayerBoardSetup SetupScript;
        public TMP_Text PlayerCountLabel;
        public Slider PlayerCountSlider;
        public TMP_Text SpacingLabel;
        public Slider SpacingSlider;
        public Button SetupButton;

        public GameObject InstructionsRoot;
        public Image InstructionsImage;
        public TMP_Text InstructionsText;
        public List<InstructionImagePair> Instructions;
        public TMP_Text PageNumberText;
        public Button ForwardButton;
        public Button BackButton;
        public int InstructionIndex;

        public GameObject CommonArea;


        private void Start()
        {
            SandboxManager.MultiBoardTutorial = this;
            UpdateInstructions();
        }

        private void OnDestroy()
        {
            SandboxManager.MultiBoardTutorial = null;
        }

        void UpdateInstructions()
        {
            InstructionsImage.sprite = Instructions[InstructionIndex].Image;
            InstructionsText.text = Instructions[InstructionIndex].Text;
            PageNumberText.text = $"{InstructionIndex + 1} / {Instructions.Count}";
            BackButton.gameObject.SetActive(InstructionIndex > 0);
            ForwardButton.gameObject.SetActive(InstructionIndex < Instructions.Count - 1);

            CommonArea.SetActive(InstructionIndex > 4);

            switch (InstructionIndex)
            {
                case 0:
                    foreach (var board in SetupScript.GetAllBoards())
                    {
                        board.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    PlayerCountLabel.color = Color.yellow;
                    SetSelectableColor(PlayerCountSlider, Color.yellow);
                    SetSelectableColor(SetupButton, Color.yellow);
                    break;
                case 1:
                    SpacingLabel.color = Color.yellow;
                    SetSelectableColor(SpacingSlider, Color.yellow);
                    SetSelectableColor(SetupButton, Color.yellow);
                    break;
            }
        }

        void SetSelectableColor(Selectable button, Color color)
        {
            var buttonColors = button.colors;
            buttonColors.normalColor = color;
            buttonColors.selectedColor = color;
            button.colors = buttonColors;
        }

        void TearDownInstructions()
        {
            switch (InstructionIndex)
            {
                case 0:
                    foreach (var board in SetupScript.GetAllBoards())
                    {
                        board.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    PlayerCountLabel.color = Color.white;
                    SetSelectableColor(PlayerCountSlider, Color.white);
                    SetSelectableColor(SetupButton, Color.white);

                    break;
                case 1:
                    SpacingLabel.color = Color.white;
                    SetSelectableColor(SpacingSlider, Color.white);
                    SetSelectableColor(SetupButton, Color.white);
                    break;
            }
        }

        public void InstructionsForward()
        {
            TearDownInstructions();
            InstructionIndex++;
            if (InstructionIndex >= Instructions.Count)
            {
                InstructionsRoot.SetActive(false);
            }
            else
            {
                UpdateInstructions();
            }
        }

        public void InstructionsBackward()
        {
            InstructionIndex = Mathf.Max(0, InstructionIndex - 1);

            UpdateInstructions();
        }

        public void SetupBoard()
        {
            SetupScript.PlayerCount = Mathf.RoundToInt(PlayerCountSlider.value);
            SetupScript.SpacingMultiplier = SpacingSlider.value;

            SetupScript.Setup(InstructionIndex > 1);
        }

        public void UpdatePlayerCount()
        {
            PlayerCountLabel.text = $"Player Count: {PlayerCountSlider.value: 0}";
        }

        public void UpdateSpacingMultiplier()
        {
            SpacingLabel.text = $"Spacing Multiplier: {SpacingSlider.value: 0.00}";
        }
    }
}
