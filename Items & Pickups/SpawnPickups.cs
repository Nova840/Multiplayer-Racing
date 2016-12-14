using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnPickups : NetworkBehaviour {

    private PickupSpawnpoints spawnpoints;

    [SerializeField]
    private float spawnInerval = 5;

    [SerializeField]
    private GameObject[] pickups;

    private void Start() {
        GameObject spawnpointsGameObject = GameObject.FindWithTag("PickupSpawnpoints");
        if (spawnpointsGameObject == null)
            return;
        spawnpoints = spawnpointsGameObject.GetComponent<PickupSpawnpoints>();

        if (NetworkServer.localClientActive)//Only the host can spawn pickups.
            StartCoroutine(Spawner(spawnInerval));
    }

    private IEnumerator Spawner(float seconds) {
        while (true) {
            yield return new WaitForSeconds(seconds);

            Transform spawnpoint = null;
            for (int i = 0; i < spawnpoints.Spawnpoints.Count; i++) {
                //tries to spawn as many times as there are spawnpoints.

                spawnpoint = spawnpoints.Spawnpoints[Random.Range(0, spawnpoints.Spawnpoints.Count)];

                bool alreadySpawned = false;
                foreach (Collider c in Physics.OverlapSphere(spawnpoint.position, 0.75f)) {
                    //.75 is about the size of a pickup
                    if (c.CompareTag("Pickup")) {
                        alreadySpawned = true;
                        break;
                    }
                }
                if (alreadySpawned)
                    continue;//try to spawn again.

                CmdSpawnPickup(pickups[Random.Range(0, pickups.Length)], spawnpoint.position, spawnpoint.rotation);
                break;//Spawned, so exit the loop.
            }
        }
    }

    //Can't be static: gives a wierd error.
    //Has to be called from the host because of the prefab. Only works because it's only called from the host.
    [Command]
    public void CmdSpawnPickup(GameObject pickup, Vector3 position, Quaternion rotation) {
        NetworkServer.Spawn((GameObject)Instantiate(pickup, position, rotation));
    }

}
