using UnityEngine;
using System.Collections;

public class NeverSleep : MonoBehaviour {

    private void Awake() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

}
