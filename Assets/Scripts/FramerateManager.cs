using UnityEngine;

public class FramerateManager : MonoBehaviour/*, ISettingsPersistence*/
{
    [SerializeField] TMPro.TextMeshProUGUI descriptionText;
    int[] framerateModes = { -1, -2, 10, 24, 30, 45, 60, 75, 90, 120, 144, 240, 360};
    int currentlySelectedMode = 1; // == -2

    /*public void LoadSettings(SettingsData data)
    {
        currentlySelectedMode = data.selectedFramerate;
        UpdateFramerateLimit(currentlySelectedMode);
    }
    public void SaveSettings(SettingsData data)
    {
        data.selectedFramerate = currentlySelectedMode;
    }*/
    void UpdateFramerateLimit(int currentlySelectedMode)
    {
        int selectedFramerate = framerateModes[currentlySelectedMode];
        QualitySettings.vSyncCount = 0;
        switch (selectedFramerate)
        {
            case -2:
                {
                    descriptionText.text = "Screen's Hz";
                    selectedFramerate = (int)Screen.currentResolution.refreshRateRatio.value;
                    break;
                }
            default:
                {
                    if (selectedFramerate == -1)
                    {
#if UNITY_ANDROID
                        descriptionText.text = "30 FPS (Android default)";
#else
                        descriptionText.text = "Max FPS";
#endif
                    }
                    else
                    {
                        descriptionText.text = framerateModes[currentlySelectedMode].ToString() + " FPS";
                    }
                    break;
                }
        }
        Application.targetFrameRate = selectedFramerate;
        Debug.Log("Application.targetFrameRate: " + Application.targetFrameRate);
    }
    public void SwitchFramerateLeft() // used by UI button
    {
        if (--currentlySelectedMode < 0)
        {
            currentlySelectedMode = framerateModes.Length - 1;
        }
        UpdateFramerateLimit(currentlySelectedMode);
    }
    public void SwitchFramerateRight() // used by UI button
    {
        if (++currentlySelectedMode >= framerateModes.Length)
        {
            currentlySelectedMode = 0;
        }
        UpdateFramerateLimit(currentlySelectedMode);
    }
    public void SwitchFramerate(float state)
    {
        UpdateFramerateLimit((int)state);
    }
}
