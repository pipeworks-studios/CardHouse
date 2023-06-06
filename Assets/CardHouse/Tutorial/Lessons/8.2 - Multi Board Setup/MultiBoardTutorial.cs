using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiBoardTutorial : MonoBehaviour
{
    public MultiplayerBoardSetup SetupScript;
    public TMP_Text PlayerCountLabel;
    public Slider PlayerCountSlider;
    public TMP_Text SpacingLabel;
    public Slider SpacingSlider;
    public TMP_Text CameraSizeLabel;
    public Slider CameraSizeSlider;
    public TMP_Text CameraOffsetLabel;
    public Slider CameraOffsetSlider;

    public void SetupBoard()
    {
        SetupScript.PlayerCount = Mathf.RoundToInt(PlayerCountSlider.value);
        SetupScript.SpacingMultiplier = SpacingSlider.value;

        SetupScript.Setup();
    }

    public void UpdatePlayerCount()
    {
        PlayerCountLabel.text = $"Player Count: {PlayerCountSlider.value: 0}";
    }

    public void UpdateSpacingMultiplier()
    {
        SpacingLabel.text = $"Spacing Multiplier: {SpacingSlider.value: 0.00}";
    }

    public void UpdateCameraSize()
    {
        CameraSizeLabel.text = $"Camera Size: {CameraSizeSlider.value: 0.00}";
        Camera.main.orthographicSize = CameraSizeSlider.value;
    }

    public void UpdateCameraYOffset()
    {
        foreach (var offset in PhaseManager.Instance.Phases.Select(x => x.CameraPosition))
        {
            offset.localPosition = offset.localPosition + Vector3.up * (CameraOffsetSlider.value - offset.localPosition.y);
        }
        CameraOffsetLabel.text = $"Camera Y: {CameraOffsetSlider.value: 0.00}";
        Camera.main.GetComponent<Homing>().StartSeeking(PhaseManager.Instance.CurrentPhase.CameraPosition.position + Vector3.back * 10);
        Camera.main.GetComponent<Turning>().StartSeeking(PhaseManager.Instance.CurrentPhase.CameraPosition.rotation.eulerAngles.z);
    }
}
