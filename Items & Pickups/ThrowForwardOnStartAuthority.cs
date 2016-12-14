using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ThrowForwardOnStartAuthority : NetworkBehaviour {

    [SerializeField]
    private float force = 1000;

    public override void OnStartAuthority() {
        base.OnStartAuthority();
        //hasAuthority always false in Start(): happens too early (I think).
        GetComponent<Rigidbody>().AddForce(
            Scripts.ScriptsGameObject.GetComponent<Players>().MyPlayer.transform.Find("Pickup/BombBall/Throw Direction").forward * force);
    }

}