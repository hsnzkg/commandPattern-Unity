using UnityEngine;

public class FPSCapper : MonoBehaviour {
    public int FPS_CAP;
    private void Awake() {
        Application.targetFrameRate = FPS_CAP;
    }
}