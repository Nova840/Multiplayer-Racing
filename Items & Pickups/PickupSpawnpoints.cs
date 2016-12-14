using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupSpawnpoints : MonoBehaviour {

    private List<Transform> spawnpoints = new List<Transform>();
    public List<Transform> Spawnpoints { get { return spawnpoints; } }

    private void Awake() {
        spawnpoints.AddRange(GetComponentsInChildren<Transform>());

        for (int i = 0; i < spawnpoints.Count; i++) {//becasue it gets the container gameobject's transform.
            if (spawnpoints[i].gameObject == this.gameObject)
                spawnpoints.RemoveAt(i);
        }
    }
}
