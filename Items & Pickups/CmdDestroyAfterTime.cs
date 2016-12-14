using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CmdDestroyAfterTime : MonoBehaviour {

    [SerializeField]
    private float time = 10;

    private void Awake() {
        if (NetworkServer.localClientActive)
            StartCoroutine(DestroyAfterTime(time));
    }

    private IEnumerator DestroyAfterTime(float seconds) {
        yield return new WaitForSeconds(seconds);
        Scripts.ScriptsGameObject.GetComponent<Players>().MyPlayer.GetComponent<PlayerCommands>().CmdDestroy(gameObject);
    }

}
