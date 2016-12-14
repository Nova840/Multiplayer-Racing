using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TurnRedAndExplode : MonoBehaviour {

    [SerializeField]
    private float lerpRate;

    private void FixedUpdate() {
        GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, Color.red, lerpRate);

        Transform root = transform.root;
        float red = GetComponent<MeshRenderer>().material.color.r;
        if (root.GetComponent<NetworkIdentity>().hasAuthority && red > .5f)//.5 experimentally determined.
            root.GetComponent<ExplodeOnTriggerEnter>().CmdDestroyThis(Scripts.ScriptsGameObject.GetComponent<Players>().MyPlayer.GetComponent<PlayerInfo>().PlayerNumber);
    }

}
