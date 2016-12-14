using UnityEngine;
using System.Collections;

public class RotateDirection : MonoBehaviour {

    [SerializeField]
    private float speed = 100;

    [SerializeField]
    private Vector3 direction = Vector3.up;

    private void FixedUpdate() {
        transform.Rotate(direction * speed);
    }

}
