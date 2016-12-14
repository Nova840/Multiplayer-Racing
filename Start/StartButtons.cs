using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StartButtons : MonoBehaviour {

    [SerializeField]
    private StartMenuTransition sMT;

    [SerializeField]
    private GameObject loadingScreenPrefab;

    private NetworkLobbyManager networkLobbyManager;

    [SerializeField]
    private UnityEngine.UI.InputField iPInput;

    void Start() {
        networkLobbyManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkLobbyManager>();
        //reassigned because the gameobject gets recreated when you disconnect from a server.

        //ActivateCanvas("Main Menu Canvas");
        sMT.EnableMenu(0);
    }

    public void OnHostGameButtonClick() {
        networkLobbyManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkLobbyManager>();
        //reassigned because the gameobject gets recreated when you disconnect from a server.

        networkLobbyManager.StartHost();

        //ActivateCanvas("Host Game Canvas");
        sMT.EnableMenu(1);
    }

    public void OnJoinGameButtonClick() {

        if (iPInput.text == "")
            return;

        PlayerPrefs.SetString(StoredKeys.lastJoinedIP, iPInput.text);
        PlayerPrefs.Save();

        networkLobbyManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkLobbyManager>();
        //reassigned because the gameobject gets recreated when you disconnect from a server.

        networkLobbyManager.networkAddress = iPInput.text;
        networkLobbyManager.StartClient();

        //ActivateCanvas("Join Game Canvas");
        sMT.EnableMenu(2);
    }

    public void OnBackButtonClick(bool disconnect) {
        //Scripts will be destroyed and come back automatically if connected to server, so Start() can happen again.
        //The ActivateCanvas("Main Menu Canvas") can happen twice. (Doesn't really matter, just efficiency)
        //if (networkLobbyManager.IsClientConnected())

        //ActivateCanvas("Main Menu Canvas");

        //Added the if statement to fix the not-problem above.
        //Unity 5.3 broke that fix. ActivateCanvas would sometimes not happen.

        sMT.EnableMenu(0);

        if (disconnect)
            networkLobbyManager.StopHost();
        //Works for both the host and client.
    }

    public void OnStartButtonClick() {
        foreach (NetworkLobbyPlayer p in networkLobbyManager.lobbySlots) {
            //Number of lobby slots is the maximum number of players. Players not joined are null.
            if (p != null && p.isLocalPlayer) {
                p.GetComponent<LobbyPlayerCmdRpc>().RpcInstantiate();//Needs to be before SendReadyToBeginMessage
                p.SendReadyToBeginMessage();
                return;
            }
        }
    }


    public void OnQuitButtonClick() {
        Application.Quit();
    }

    public void OnOptionsButtonClick() {
        sMT.EnableMenu(3);
    }

}
