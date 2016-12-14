using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetTextToNumberOfPlayers : MonoBehaviour {

    [SerializeField]
    private NetworkLobbyManager networkLobbyManager;
    [SerializeField]
    private float interval = 0.25f;

    private void OnEnable() {
        StartCoroutine(UpdatePlayers(interval));
    }

    private void OnDisable() {
        StopCoroutine(UpdatePlayers(interval));
    }

    private IEnumerator UpdatePlayers(float seconds) {
        while (true) {
            GetComponent<UnityEngine.UI.Text>().text = "Number of players: " + (networkLobbyManager.numPlayers);
            yield return new WaitForSeconds(seconds);
        }
    }

}
