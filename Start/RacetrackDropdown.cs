using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RacetrackDropdown : MonoBehaviour {

    [SerializeField]
    private string[] scenes;//index must correspond to the int value in enum Racetracks.

    private NetworkLobbyManager Nlm {
        get {
            return GameObject.FindWithTag("NetworkManager").GetComponent<NetworkLobbyManager>();
        }
    }

    public void OnRacetrackChange(int racetrack) {
        switch (racetrack) {
            case (int)Racetracks.Testing:
                Nlm.playScene = scenes[(int)Racetracks.Testing];
                //assumes the NetworkManager is a NetworkLobbyManager.
                //can't just assign the NetworkManager as a class variable in the editor because the gameobject gets deleted on disconnect.
                break;

            case (int)Racetracks.Forest:
                Nlm.playScene = scenes[(int)Racetracks.Forest];
                break;
        }
    }

    //this gameobject is never disabled, but a parent is.
    //happens when I would think OnDisable() would happen as well, but doesn't really matter.
    private void OnEnable() {
        OnRacetrackChange(GetComponent<UnityEngine.UI.Dropdown>().value);
        //so the play scene is correct after returning to the hosting screen.
    }
}

public enum Racetracks {
    Testing = 0, Forest = 1
}