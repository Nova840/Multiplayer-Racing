using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LobbyPlayerCmdRpc : NetworkBehaviour {

    [ClientRpc]
    public void RpcInstantiate() {
        Instantiate(Resources.Load("Loading Canvas"));
    }

}
