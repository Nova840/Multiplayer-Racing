using UnityEngine;
using System.Collections;

public class TimeScaleOnAwake : MonoBehaviour {

    [SerializeField]
    private float timeScale = 1;

    private void Awake() {
        Time.timeScale = timeScale;
    }

}
