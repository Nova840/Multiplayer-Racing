using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerRpcs : NetworkBehaviour {

    [SerializeField]
    private string explosionName;

    [ClientRpc]
    public void RpcBombExplosion(Vector3 position, Quaternion rotation, int playerExplosion) {
        GameObject explosion = Instantiate(Resources.Load(explosionName), position, rotation) as GameObject;
        explosion.GetComponent<ExplodeOnStart>().PlayerExplosion = playerExplosion;
    }

}
