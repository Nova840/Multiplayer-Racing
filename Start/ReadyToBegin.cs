using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/*
In Unity 5.3 SendReadyToBeginMessage() starts the game no matter who calls it.
Makes this script start the game whenever any remote clilent enters the lobby.

UPDATE: In unity 5.4 it's back to the way it used to be,
so this script is now on the network lobby player prefab.
*/
public class ReadyToBegin : NetworkBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(SendReadyIfNotHost());
    }

    private IEnumerator SendReadyIfNotHost()
    {
        yield return new WaitForEndOfFrame();

        if (GetComponent<NetworkLobbyPlayer>().isLocalPlayer && !NetworkServer.localClientActive)
            GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();        
    }
}
