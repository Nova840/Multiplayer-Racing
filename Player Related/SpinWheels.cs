using UnityEngine;
using System.Collections;

public class SpinWheels : MonoBehaviour {

    [SerializeField]
    private bool reverseDirection = false;

    private float speed;

    private PlayerMove playerMove;

    private void Start() {
        playerMove = transform.root.GetComponent<PlayerMove>();

        speed = playerMove.MoveForce * 2.5f;
        //2.5 is experimentally determined.
        if (reverseDirection)
            speed = -speed;
    }

    void FixedUpdate() {
        if (playerMove.Moveable && playerMove.MovingForward)
            transform.Rotate(Vector3.right * speed);
    }
}
