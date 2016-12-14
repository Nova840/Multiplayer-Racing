using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ExplodeOnTriggerEnter : NetworkBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (!hasAuthority)
            //So there is only one client that sends the command.
            return;

        if (other.isTrigger)
            //So it doesn't hit not-solid stuff like the lap counter.
            return;

        if (other.CompareTag("Player") &&
            Scripts.ScriptsGameObject.GetComponent<Players>().MyPlayer.GetComponent<PlayerInfo>().UID == other.GetComponent<PlayerInfo>().UID)
            //So it can't hit the player that threw it as soom as it's thrown
            return;

        CmdDestroyThis(Scripts.ScriptsGameObject.GetComponent<Players>().MyPlayer.GetComponent<PlayerInfo>().PlayerNumber);
    }

    [Command]
    public void CmdDestroyThis(int playerExplosion) {
        Scripts.ScriptsGameObject.GetComponent<Players>().MyPlayer.GetComponent<PlayerRpcs>().
            RpcBombExplosion(transform.position, transform.rotation, playerExplosion);
        Destroy(gameObject);
    }

}
