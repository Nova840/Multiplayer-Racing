using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Countdown : MonoBehaviour {

    [SerializeField]
    private float timeLeft = 10, timeMultiplier = 1;

    private void Update() {
        if (!Pause.isPaused(Scripts.ScriptsGameObject.GetComponent<Players>()))
            timeLeft -= Time.deltaTime * timeMultiplier;

        if (NetworkServer.localClientActive && timeLeft <= 0) {
            Scripts.ScriptsGameObject.GetComponent<OnPlayersInList>().CmdDestroyCountdown();
            Destroy(this);
            //Doesn't do anything because this is on a child of what we just destroyed, but just in case.
        }

        //set a minimum of 1 because if the host leaves it can go negative.
        GetComponent<UnityEngine.UI.Text>().text = Mathf.Ceil(Mathf.Max(timeLeft, 1)).ToString();
    }

}
