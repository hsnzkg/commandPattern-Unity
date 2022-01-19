using UnityEngine;

public class FPSCapper : MonoBehaviour {
    private void Awake() {
        Application.targetFrameRate = 60;
    }
}