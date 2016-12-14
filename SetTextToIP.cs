using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTextToIP : MonoBehaviour {

    private void OnEnable() {
        string ip = Network.player.ipAddress;
        if (ip != "0.0.0.0")
            GetComponent<Text>().text = "IP: " + ip;
        else
            GetComponent<Text>().text = "No IP address. Make sure you are connected to a network.";
    }

}
