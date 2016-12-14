using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PauseButtons : MonoBehaviour {

    public void OnPauseButtonClick() {
        Players players = Scripts.ScriptsGameObject.GetComponent<Players>();
        players.MyPlayer.GetComponent<Pause>().OnPauseButtonClick(players);
    }

    public void OnExitGameButtonClick() {
        Players players = Scripts.ScriptsGameObject.GetComponent<Players>();
        Pause pause = players.MyPlayer.GetComponent<Pause>();

        if (pause.Paused && !pause.isOtherPlayerPaused(players))
            pause.CmdPause(false, players.MyPlayer.GetComponent<PlayerInfo>().PlayerName);

        StartCoroutine(StopHost(.1f));//Allows for enough time for the game to be unpaused.
    }

    private IEnumerator StopHost(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>().StopHost();
    }

}
