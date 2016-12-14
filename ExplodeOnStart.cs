using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ExplodeOnStart : MonoBehaviour {

    [SerializeField]
    private float blastRadius = 5, force = 10;

    [SerializeField]
    private int spins = 1;

    [SerializeField]
    private ForceMode explosionForceMode;

    private int playerExplosion = 0;
    public int PlayerExplosion { set { playerExplosion = value; } }

    private void Start() {
        foreach (Collider c in Physics.OverlapSphere(transform.position, blastRadius)) {
            if (c.CompareTag("Player") && c.GetComponent<NetworkIdentity>().isLocalPlayer &&
                Scripts.ScriptsGameObject.GetComponent<Players>().MyPlayer.GetComponent<PlayerInfo>().PlayerNumber != playerExplosion) {
                c.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, blastRadius, 0, explosionForceMode);
                c.GetComponent<PlayerSpin>().Spin(spins);
            }
        }
    }

}
