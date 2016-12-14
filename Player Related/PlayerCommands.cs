using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class PlayerCommands : NetworkBehaviour {

    [Command]
    public void CmdSpawnBomb(Vector3 position, Quaternion rotation, GameObject player) {
        GameObject bomb = Instantiate(Resources.Load("Bomb"), position, rotation) as GameObject;
        NetworkServer.SpawnWithClientAuthority(bomb, player);
    }

    [Command]
    public void CmdDestroy(GameObject g) {
        Destroy(g);
    }

}
