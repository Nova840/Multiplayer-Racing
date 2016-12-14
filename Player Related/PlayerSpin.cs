using UnityEngine;
using System.Collections;

public class PlayerSpin : MonoBehaviour {

    [SerializeField, Range(0, 30)]//Above 30 is too fast.
    private float speed;

    [SerializeField]
    private bool clockwise = true;

    private float degreesLeft = 0;

    public void Spin(int revolutions) {
        degreesLeft += (revolutions * 360);
        GetComponent<PlayerMove>().MovingForward = false;
    }

    private void FixedUpdate() {
        if (degreesLeft <= 0)
            return;

        if (clockwise)
            transform.Rotate(Vector3.up * speed);
        else
            transform.Rotate(Vector3.up * -speed);

        degreesLeft -= speed;

        if (degreesLeft <= 0) {
            degreesLeft = 0;
            GetComponent<PlayerMove>().MovingForward = true;
        }
    }

}
