using UnityEngine;
using System.Collections;

using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {
    [SerializeField]
    private float moveForce = 1000, turnForce = 1000;
    public float MoveForce { get { return moveForce; } }
    [SerializeField]
    private bool touchControlled = true;
    private TouchControls touchControls_Script;

    private bool movingForward = true;//Used in PlayerSpin and PlayerInfo in RpcFinish.
    public bool MovingForward { get { return movingForward; } set { movingForward = value; } }

    private bool moveable = false;
    public bool Moveable {
        get {
            return moveable;
        }
        set {
            if (isLocalPlayer)//so other players wheels will still stop spinning
                GetComponent<Rigidbody>().isKinematic = !value;
            moveable = value;
        }
    }

    private void Start() {
        if (!isLocalPlayer)//so it doesn't move other player's cars
            this.enabled = false;
        else {//because other players should not be kinematic
            GetComponent<Rigidbody>().isKinematic = true;

            //the local player has its prefab's rotation for some reason. probably something to do with it being kinematic.
            //Cars face the same way as the lap counter to fix the problem above.
            transform.rotation = GameObject.FindWithTag("LapCounter").transform.rotation;
        }

        touchControls_Script = GameObject.FindGameObjectWithTag("TouchControls").GetComponent<TouchControls>();
    }

    private void FixedUpdate() {
        if (!movingForward)
            return;
        if (touchControls_Script == null)
            return;

        if (touchControlled) {
            GetComponent<Rigidbody>().AddForce(transform.forward * moveForce);
            GetComponent<Rigidbody>().AddTorque(Vector3.up * touchControls_Script.Axis * turnForce);
        } else
          //For testing only
          {
            GetComponent<Rigidbody>().AddForce(transform.forward * Input.GetAxisRaw("Vertical") * moveForce);
            GetComponent<Rigidbody>().AddTorque(Vector3.up * Input.GetAxisRaw("Horizontal") * turnForce);
        }
    }

    private void OnDestroy() {
        DestroyTouchControls(0);
        //Destroys too quickly and gives an error in editor when you manually end the game, so wait for the next frame to destroy
    }

    private IEnumerator DestroyTouchControls(float seconds) {
        yield return new WaitForSeconds(seconds);

        if (touchControls_Script.gameObject != null)
            Destroy(touchControls_Script.gameObject);
    }
}
