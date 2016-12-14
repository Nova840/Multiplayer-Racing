using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour {

    private GameObject cameraGameObject;

    [SerializeField]
    private float lerpRate = .1f, minDistance = 10, buffer = 10;

    private Vector3 targetPosition;
    private float targetDistance;

    private void Awake() {
        cameraGameObject = transform.Find("Camera").gameObject;
    }

    private void LateUpdate() {
        List<GameObject> playersList = Scripts.ScriptsGameObject.GetComponent<Players>().PlayersList;

        for (int i = 0; i < playersList.Count; i++) {
            if (playersList[i].GetComponent<PlayerInfo>().Finished) {//camera shouldn't track finished players
                playersList.RemoveAt(i);
                i--;
            }
        }

        if (playersList.Count <= 0)
            return;

        float avgX, avgZ;
        AverageXZ(playersList, out avgX, out avgZ);

        targetPosition = new Vector3(avgX, transform.position.y, avgZ);

        targetDistance = FindTargetDistance(playersList);
    }


    private float FindTargetDistance(List<GameObject> playersList) {
        Vector3 p1CamPos = transform.InverseTransformPoint(playersList[0].transform.position);//player's position relative to camera rig.
        float highestX = p1CamPos.x,
            lowestX = p1CamPos.x,
            highestY = p1CamPos.y,
            lowestY = p1CamPos.y;

        for (int i = 1; i < playersList.Count; i++) {//Starts at 1 because index 0 is set above.
            Vector3 v = transform.InverseTransformPoint(playersList[i].transform.position);

            lowestX = Mathf.Min(v.x, lowestX);
            highestX = Mathf.Max(v.x, highestX);

            lowestY = Mathf.Min(v.y, lowestY);
            highestY = Mathf.Max(v.y, highestY);
        }

        float fitHeight = Mathf.Abs(highestY - lowestY) + buffer;
        float fitWidth = Mathf.Abs(highestX - lowestX) + buffer;

        float targetHeight = Mathf.Max(fitHeight, fitWidth / cameraGameObject.GetComponent<Camera>().aspect);

        targetDistance = targetHeight * 0.5f / Mathf.Tan(cameraGameObject.GetComponent<Camera>().fieldOfView * 0.5f * Mathf.Deg2Rad);
        targetDistance = Mathf.Max(targetDistance, minDistance);

        return targetDistance;
    }

    private void AverageXZ(List<GameObject> playersList, out float avgX, out float avgZ) {
        Vector3 p1pos = playersList[0].transform.position;
        float highestX = p1pos.x,
            lowestX = p1pos.x,
            highestZ = p1pos.z,
            lowestZ = p1pos.z;

        for (int i = 1; i < playersList.Count; i++) {//Starts at 1 because index 0 is set above.
            Transform t = playersList[i].transform;

            lowestX = Mathf.Min(t.position.x, lowestX);
            highestX = Mathf.Max(t.position.x, highestX);

            lowestZ = Mathf.Min(t.position.z, lowestZ);
            highestZ = Mathf.Max(t.position.z, highestZ);
        }

        avgX = (lowestX + highestX) / 2;
        avgZ = (lowestZ + highestZ) / 2;
    }

    private float startTime;
    private void Start() {
        startTime = Time.time;
    }

    [SerializeField]
    private float moveAfterSeconds = 0.5f;//Starts moving the camera after a time delay.
    private void FixedUpdate() {
        if (Scripts.ScriptsGameObject.GetComponent<Players>().PlayersList.Count <= 0 ||
            Time.time - startTime <= moveAfterSeconds)
            return;

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpRate);
        cameraGameObject.transform.localPosition =
            new Vector3(
                cameraGameObject.transform.localPosition.x,
                cameraGameObject.transform.localPosition.y,
                Mathf.Lerp(cameraGameObject.transform.localPosition.z, -targetDistance, lerpRate));//Lerp Z to zoom.
    }

}
