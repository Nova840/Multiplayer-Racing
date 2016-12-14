using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTime : MonoBehaviour {

    private float raceTime, currentLapTime;
    private List<float> lapTimeList = new List<float>();
    public float RaceTime { get { return raceTime; } }

    private void Update() {
        if (GetComponent<PlayerMove>().Moveable && !GetComponent<PlayerInfo>().Finished) {
            raceTime += Time.deltaTime;
            currentLapTime += Time.deltaTime;
        }
    }

    public void FinishLap() {
        lapTimeList.Add(currentLapTime);
        currentLapTime = 0;
    }

}
