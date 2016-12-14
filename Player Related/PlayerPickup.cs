using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerPickup : NetworkBehaviour {

    [SyncVar(hook = "OnCurrentPickupChange")]
    private Pickup currentPickup = Pickup.None;

    [SerializeField]
    private GameObject bombPickup;
    //Can't serialize a dictionary. Set up in Start()
    private Dictionary<Pickup, GameObject> displayPickups = new Dictionary<Pickup, GameObject>();
    private Dictionary<Pickup, UnityEngine.UI.Button> buttons = new Dictionary<Pickup, UnityEngine.UI.Button>();

    private void Start() {
        displayPickups.Add(Pickup.Bomb, bombPickup);

        Transform interactableOverlayCanvas = GameObject.FindWithTag("InteractableOverlayCanvas").transform;
        buttons.Add(Pickup.Bomb, interactableOverlayCanvas.Find("Pickup Buttons/Bomb Button").GetComponent<UnityEngine.UI.Button>());

        foreach (KeyValuePair<Pickup, GameObject> g in displayPickups) {
            if (g.Value == null)
                Debug.LogWarning(g.Key + " Null");
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Pickup") || !isLocalPlayer || currentPickup != Pickup.None)
            return;

        CmdSetCurrentPickup(other.GetComponent<PickupType>().ThisPickupType);

        CmdDestroyPickup(other.gameObject);
    }

    [Command]
    public void CmdSetCurrentPickup(Pickup newPickup) {
        currentPickup = newPickup;
    }

    private void OnCurrentPickupChange(Pickup newPickup) {
        currentPickup = newPickup;
        SetDisplayPickup(newPickup);
        if (isLocalPlayer)
            ShowButton(newPickup);
    }

    private void SetDisplayPickup(Pickup pickup) {
        foreach (KeyValuePair<Pickup, GameObject> g in displayPickups) {
            g.Value.SetActive(pickup == g.Key);
        }
    }

    private void ShowButton(Pickup pickup) {
        foreach (KeyValuePair<Pickup, UnityEngine.UI.Button> b in buttons) {
            b.Value.gameObject.SetActive(pickup == b.Key);
        }
    }

    [Command]
    private void CmdDestroyPickup(GameObject pickup) {
        Destroy(pickup);
    }

}

public enum Pickup {
    None, Bomb
}