using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerInfo : NetworkBehaviour {

    [SyncVar(hook = "OnLapsChange")]
    private int laps = 0;
    private int maxLaps;
    //the highest laps has ever been. Used because of PlayerTime and going back laps.

    [SyncVar(hook = "OnUIDChange")]
    private string uID;
    public string UID { get { return uID; } }
    public int PlayerNumber { get { return int.Parse(uID.Substring(uID.Length - 1)); } }

    [SyncVar(hook = "OnPlayerNameChange")]
    private string playerName;
    public string PlayerName {
        get {
            if (playerName != "")
                return playerName;
            else
                return uID;
        }
    }

    [SerializeField]
    private GameObject[] cars;

    [SerializeField]
    private string explosionName;

    [SyncVar(hook = "OnPlayerCarChange")]
    private int playerCar = -1;

    private enum PlayerColor {
        Blue, Red, Yellow, Green
    }

    [SyncVar(hook = "OnPlayerColorChange")]
    private PlayerColor playerColor = PlayerColor.Blue;

    [SyncVar]
    private bool finished;
    public bool Finished { get { return finished; } }

    //private LevelData levelData;
    //private FinishedPlayers finishedPlayers;

    private void Start() {
        //levelData = GameObject.Find("Scripts").GetComponent<LevelData>();
        //finishedPlayers = GameObject.Find("Scripts").GetComponent<FinishedPlayers>();

        //OnLapsChange(laps);

        //Happens before these objects actually exist. It has to do with scripts having a NetworkIdentity.

        if (isLocalPlayer) {
            CmdSetPlayerCar(PlayerPrefs.GetInt(StoredKeys.currentCar, -1));
            //the retrieved value should always have a value > -1 because it checks this in the start menu.

            CmdSetUID("Player " + RankNetID());
            CmdSetPlayerName(PlayerPrefs.GetString(StoredKeys.playerName));
        } else {
            Invoke("ChangeCar", 1f);
            //OnPlayerColorChange is called in OnPlayerCarChange.
        }

        gameObject.name = uID;
        //In the case where other players spawn first.
        //Doesn't matter if they don't have a UID yet because it will be set in OnUIDChange().

        //UID Display Text initialized in the PlayerUIDText script.
    }

    private void ChangeCar() {
        OnPlayerCarChange(playerCar);
    }

    private int RankNetID() {
        int numPlayersLower = 0;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player")) {
            if (g.GetComponent<PlayerInfo>().isLocalPlayer)
                //ignore this player.
                continue;

            if (g.GetComponent<NetworkIdentity>().netId.Value < GetComponent<NetworkIdentity>().netId.Value)
                //if other player's netID is less than yours.
                numPlayersLower++;
        }
        //Determines number of players with a lower netID than this player.

        return numPlayersLower + 1;
    }

    [Command]
    private void CmdSetPlayerCar(int car) {
        playerCar = car;
    }

    private void OnPlayerCarChange(int car) {
        playerCar = car;

        if (GetComponentInChildren<CarColors>() != null)
            return;
        //assumes that the player doesn't already have a car

        Transform carTransform = Instantiate(cars[car]).transform;

        Vector3 carOffset = carTransform.localPosition;
        carTransform.parent = transform;
        carTransform.localPosition = carOffset;
        carTransform.localRotation = Quaternion.identity;

        OnPlayerColorChange(playerColor);
    }

    [Command]
    private void CmdSetUID(string str) {
        uID = str;
    }

    private void OnUIDChange(string uID) {
        this.uID = uID;
        gameObject.name = uID;

        if (!isLocalPlayer)
            return;

        if (isLocalPlayer) {
            Invoke("SendPlayerColorCmd", 1f);
        }
    }

    private void SendPlayerColorCmd() {
        PlayerColor color = PlayerColor.Blue;
        //Change if I ever want to let the player choose color.
        if (uID == "Player 1") {
            color = PlayerColor.Blue;
        } else if (uID == "Player 2") {
            color = PlayerColor.Red;
        } else if (uID == "Player 3") {
            color = PlayerColor.Yellow;
        } else if (uID == "Player 4") {
            color = PlayerColor.Green;
        }

        CmdSetPlayerColor(color);
    }

    [Command]
    private void CmdSetPlayerColor(PlayerColor color) {
        playerColor = color;
    }

    private void OnPlayerColorChange(PlayerColor newColor) {
        playerColor = newColor;

        foreach (MeshRenderer mR in transform.GetComponentsInChildren<MeshRenderer>()) {
            mR.enabled = true;
        }

        foreach (CarColors carColors in GetComponentsInChildren<CarColors>()) {
            if (newColor == PlayerColor.Blue) {
                carColors.GetComponent<Renderer>().material.mainTexture = carColors.BlueTexture;
            } else if (newColor == PlayerColor.Red) {
                carColors.GetComponent<Renderer>().material.mainTexture = carColors.RedTexture;
            } else if (newColor == PlayerColor.Yellow) {
                carColors.GetComponent<Renderer>().material.mainTexture = carColors.YellowTexture;
            } else if (newColor == PlayerColor.Green) {
                carColors.GetComponent<Renderer>().material.mainTexture = carColors.GreenTexture;
            }
        }
    }

    [Command]
    private void CmdSetPlayerName(string str) {
        playerName = str;
    }

    private void OnPlayerNameChange(string playerName) {
        this.playerName = playerName;

        if (playerName == "") {
            if (!isLocalPlayer)
                transform.Find("Name Display Canvas/Text").GetComponent<UnityEngine.UI.Text>().text = uID;
            else
                transform.Find("Name Display Canvas/Text").GetComponent<UnityEngine.UI.Text>().text = uID + "\n(You)";
        } else {
            transform.Find("Name Display Canvas/Text").GetComponent<UnityEngine.UI.Text>().text = playerName;
        }

    }

    [Command]
    public void CmdAddLaps(int laps) {
        if (this.laps + laps >= 0)
            //0 is before the race starts.
            this.laps += laps;

        if (this.laps > Scripts.ScriptsGameObject.GetComponent<LevelData>().Laps) {
            Scripts.ScriptsGameObject.GetComponent<FinishedPlayers>().CmdAddFinishedPlayer(gameObject);
            this.finished = true;
            RpcFinish();
        }
    }

    private void OnLapsChange(int laps) {
        this.laps = laps;

        if (isLocalPlayer)
            GameObject.FindWithTag("OverlayCanvas").transform.Find("Laps Text").GetComponent<UnityEngine.UI.Text>().text =
                "Lap " + Mathf.Max(1, laps) + "/" + Scripts.ScriptsGameObject.GetComponent<LevelData>().Laps;
        //Doesn't work with saving the gameobject as a variable because Start() happens too early.

        if (!finished && laps > 1 && laps > maxLaps)
            GetComponent<PlayerTime>().FinishLap();

        maxLaps = Mathf.Max(maxLaps, laps);
    }

    [ClientRpc]
    private void RpcFinish() {

        foreach (MeshRenderer mr in transform.GetComponentsInChildren<MeshRenderer>()) {
            mr.enabled = false;
        }

        GetComponent<PlayerMove>().MovingForward = false;
        GetComponent<Collider>().enabled = false;

        transform.Find("Name Display Canvas/Text").gameObject.SetActive(false);

        if (isLocalPlayer) {
            Scripts.ScriptsGameObject.GetComponent<Players>().MyPlayer.GetComponent<PlayerPickup>().CmdSetCurrentPickup(Pickup.None);

            GameObject overlayCanvas = GameObject.FindWithTag("OverlayCanvas");

            overlayCanvas.transform.Find("Winners Text").gameObject.SetActive(true);
            //Doesn't work with saving the gameobject as a class variable because Start() happens too early.
            //Can't just GameObject.Find("Winners Text") because it's not active.

            overlayCanvas.transform.Find("Laps Text").gameObject.SetActive(false);
            CmdSpawnExplosion(transform.position, transform.rotation);
        }
    }

    [Command]
    private void CmdSpawnExplosion(Vector3 position, Quaternion rotation) {
        NetworkServer.Spawn((GameObject)Instantiate(Resources.Load(explosionName), position, rotation));
    }

}