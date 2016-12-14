using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
/*
On player object so it can use the Command attribute
*/
public class Pause : NetworkBehaviour {

    [SyncVar]
    private bool paused = false;
    public bool Paused { get { return paused; } }

    //private Vector3 localPlayerVelocity = Vector3.zero;

    private GameObject pausedText, exitGameButton;

    private void Start() {
        pausedText = GameObject.FindWithTag("OverlayCanvas").transform.Find("Paused Text").gameObject;
        exitGameButton = GameObject.FindWithTag("InteractableOverlayCanvas").transform.Find("Exit Button").gameObject;
        //Might not be active so can't find them directly
    }

    //Only happens on the local player. Called from the PauseButton script.
    public void OnPauseButtonClick(Players players) {
        if (isOtherPlayerPaused(players))
            return;

        CmdPause(!paused, GetComponent<PlayerInfo>().PlayerName);
    }

    public bool isOtherPlayerPaused(Players players) {
        foreach (GameObject g in players.PlayersList) {
            if (g != this.gameObject && g.GetComponent<Pause>().Paused)
                return true;
        }
        return false;
    }

    public static bool isPaused(Players players) {
        foreach (GameObject g in players.PlayersList) {
            if (g.GetComponent<Pause>().Paused)
                return true;
        }
        return false;
    }

    [Command]
    public void CmdPause(bool toPause, string playerName) {
        paused = toPause;
        RpcPause(toPause);
        RpcPausedGUI(toPause, playerName);
    }

    [ClientRpc]
    private void RpcPause(bool toPause) {
        if (toPause) {
            SetListToTargetSyncPos(Scripts.ScriptsGameObject.GetComponent<Items>().ItemsList);
            SetListToTargetSyncPos(Scripts.ScriptsGameObject.GetComponent<Players>().PlayersList);
        }

        Time.timeScale = toPause ? 0 : 1;
    }

    private void SetListToTargetSyncPos(List<GameObject> list) {//Assumes everything in the list has a NetworkTransform component
        foreach (GameObject g in list) {
            if (!g.GetComponent<NetworkIdentity>().hasAuthority)
                g.transform.position = g.GetComponent<NetworkTransform>().targetSyncPosition;
        }
    }

    [ClientRpc]
    private void RpcPausedGUI(bool toPause, string playerName) {
        pausedText.SetActive(toPause);
        pausedText.GetComponent<UnityEngine.UI.Text>().text = playerName + " Pause";

        exitGameButton.SetActive(toPause);
    }

}
