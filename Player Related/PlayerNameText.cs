using UnityEngine;
using System.Collections;

public class PlayerNameText : MonoBehaviour {
    [SerializeField]
    private Transform toLook;
    private Transform cameraTransform;

    [SerializeField]
    private PlayerInfo playerInfo;

    private void Start() {
        cameraTransform = GameObject.FindWithTag("MainCamera").transform;

        //Happens before the local player has a UID/Name, so set in the playerInfo script.
        //This is for the non-local players that have already spawned.

        if (!transform.Find("../../").GetComponent<PlayerInfo>().isLocalPlayer) {
            string pN = playerInfo.PlayerName;
            if (pN == "")
                GetComponent<UnityEngine.UI.Text>().text = playerInfo.UID;
            else
                GetComponent<UnityEngine.UI.Text>().text = pN;

        }
    }

    private void Update() {
        toLook.eulerAngles = cameraTransform.eulerAngles;
    }

}
