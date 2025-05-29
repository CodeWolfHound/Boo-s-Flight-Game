using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    private int FPS = -1;
    private int vSync = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // Uses vSync to lock minitor refresh rate
        QualitySettings.vSyncCount = vSync;
        // Lets browsers and vSync control fps
        Application.targetFrameRate = FPS;
    }

}
