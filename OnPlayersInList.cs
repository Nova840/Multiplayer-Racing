using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class OnPlayersInList : NetworkBehaviour {

    [SerializeField]
    private GameObject countdownCanvas;

    [SerializeField]
    private float waitBeforeAllPlayersConnected = 0;

    private void Awake() {
        ClientScene.RegisterPrefab(countdownCanvas);
        //Needs time to register prefab, so does not work in Start().
    }

    private void Start() {
        if (NetworkServer.localClientActive)//If this is the "host".
            StartCoroutine(WhenToStart());
    }

    private IEnumerator WhenToStart() {
        Players players = GetComponent<Players>();
        NetworkManager nM = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
        while (true) {
            if (!(players.PlayersList.Count < nM.numPlayers))
            //Starts when all players are in list.
            {
                Invoke("CmdStartCountdown", waitBeforeAllPlayersConnected);
                break;
            }
            yield return new WaitForSeconds(0);
        }
    }

    [Command]
    private void CmdStartCountdown() {
        NetworkServer.Spawn(Instantiate(countdownCanvas));
    }

    [Command]
    public void CmdDestroyCountdown() {
        Destroy(GameObject.FindWithTag("CountdownCanvas"));
        RpcStartPlayers();
    }

    [ClientRpc]
    private void RpcStartPlayers() {
        foreach (GameObject g in GetComponent<Players>().PlayersList) {
            g.GetComponent<PlayerMove>().Moveable = true;
        }
    }
}
