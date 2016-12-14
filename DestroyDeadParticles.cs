using UnityEngine;
using System.Collections;

public class DestroyDeadParticles : MonoBehaviour {

    private void Update() {
        if (!GetComponent<ParticleSystem>().IsAlive())
            Destroy(gameObject);
    }

}
