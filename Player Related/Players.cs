using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Players : NetworkBehaviour {
    [SerializeField]
    private List<GameObject> playersList = new List<GameObject>();

    private GameObject myPlayer;
    public GameObject MyPlayer {
        get {
            if (myPlayer == null) {
                foreach (GameObject g in playersList) {
                    if (g.GetComponent<PlayerInfo>().isLocalPlayer) {
                        myPlayer = g;
                        break;
                    }
                }
            }
            return myPlayer;
        }
    }

    public List<GameObject> PlayersList { get { return playersList; } }


    private void Start() {
        AddAllPlayers();//in case the car is enabled first
        //Also added in the AddRemovePlayerFromList script in case the Scripts GameObject is enabled before a player.
    }

    private void AddAllPlayers() {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player")) {
            addPlayerToList(g);
        }
    }

    public void addPlayerToList(GameObject player) {
        if (!player.CompareTag("Player")) {
            Debug.LogError("Tried to add a non-player to the players list");
            return;
        }

        if (!playersList.Contains(player))
            playersList.Add(player);

    }

    public void removePlayerFromList(GameObject player) {
        if (!player.CompareTag("Player"))
            Debug.LogError("Tried to add a non-player to the players list");

        playersList.Remove(player);
    }

}
