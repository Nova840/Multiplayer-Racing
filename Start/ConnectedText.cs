using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ConnectedText : MonoBehaviour
{
    //[SerializeField]
    private NetworkLobbyManager networkLobbyManager;
    [SerializeField]
    private float interval = 0;

    private void OnEnable()
    {
        networkLobbyManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkLobbyManager>();
        //reassigned because the gameobject gets recreated when you disconnect from a server.
        StartCoroutine(CheckIfConnected(interval));
    }

    private void OnDisable()
    {
        StopCoroutine(CheckIfConnected(interval));
    }

    private IEnumerator CheckIfConnected(float seconds)
    {
        while (true)
        {
            GetComponent<UnityEngine.UI.Text>().text = networkLobbyManager.IsClientConnected() ? "Connected to Host" : "Looking for Host";
            yield return new WaitForSeconds(seconds);
        }


    }

}
