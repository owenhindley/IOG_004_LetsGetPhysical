using UnityEngine;

public class SetGlobalSettings : MonoBehaviour {
    public int fps = 60;
    public int antiAliasing = 2;

    void Awake() {
        QualitySettings.antiAliasing = antiAliasing;
        Application.targetFrameRate = fps;
    }
}
