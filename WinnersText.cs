using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinnersText : MonoBehaviour {
    [SerializeField]
    private FinishedPlayers finishedPlayers;

    [SerializeField]
    private float updateInterval, lerpRate = .1f;

    private Vector2 targetAnchoredPosition;

    private void Start() {
        //gameObject.SetActive(false);
        //Makes it not enable from PlayerInfo if you disable it in the editor as well.

        RectTransform rT = GetComponent<RectTransform>();

        targetAnchoredPosition = rT.anchoredPosition;

        CanvasScaler cS = transform.parent.GetComponent<CanvasScaler>();

        if (cS.screenMatchMode != CanvasScaler.ScreenMatchMode.Expand)//Assumes that the screen match mode is set to "Expand".
            Debug.LogWarning("Warning: this script assumes the canvas' screen match mode has been set to \"Expand\", untested on any other mode");

        //copied the code below from the StartMenuTransition script and modified a little.
        float yPos = rT.anchoredPosition.y;
        float avgYAnchor = (rT.anchorMin.y + rT.anchorMax.y) / 2;

        yPos =
            transform.parent.GetComponent<RectTransform>().rect.height
            * -avgYAnchor
            - (rT.rect.height / 2);

        rT.anchoredPosition = new Vector2(rT.anchoredPosition.x, yPos);

    }

    private void FixedUpdate() {
        RectTransform rT = GetComponent<RectTransform>();
        rT.anchoredPosition = Vector2.Lerp(rT.anchoredPosition, targetAnchoredPosition, lerpRate);
    }

    private void OnEnable() {
        StartCoroutine(UpdateFinishedPlayers(updateInterval));
    }

    private void OnDisable() {
        StopCoroutine(UpdateFinishedPlayers(updateInterval));
    }

    private IEnumerator UpdateFinishedPlayers(float seconds) {
        while (true) {
            string text = "";
            for (int i = 0; i < finishedPlayers.FinishedPlayersList.Count; i++) {
                text += (i + 1) + ": " + finishedPlayers.FinishedPlayersList[i] + " : " + finishedPlayers.FinishedPlayerTimesList[i].ToString() + "s\n";
            }
            GetComponent<UnityEngine.UI.Text>().text = text;

            yield return new WaitForSeconds(seconds);
        }
    }
}
