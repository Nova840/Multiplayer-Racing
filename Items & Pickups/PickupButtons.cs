using UnityEngine;
using System.Collections;

public class PickupButtons : MonoBehaviour {

    public void OnBombButtonClick() {
        GameObject myPlayer = Scripts.ScriptsGameObject.GetComponent<Players>().MyPlayer;

        myPlayer.GetComponent<PlayerPickup>().CmdSetCurrentPickup(Pickup.None);

        Transform bombTransform = myPlayer.transform.Find("Pickup/BombBall");

        Scripts.ScriptsGameObject.GetComponent<Players>().MyPlayer.GetComponent<PlayerCommands>().
            CmdSpawnBomb(bombTransform.position, bombTransform.rotation, myPlayer);
    }

}
