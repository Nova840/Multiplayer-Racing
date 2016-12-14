using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartImagesColor : MonoBehaviour {

    [SerializeField]
    private Image[] images;

    [SerializeField]
    private float lerpRate = .1f;

    private Color[] menuColors = { new Color(234f/255f, 64f/255f, 64f/255f),
        new Color(100f/255f, 223f/255f, 100f/255f),
        new Color(94f/255f, 102f/255f, 255f/255f),
        new Color(180f/255f, 180f/255f, 180f/255f) };//corresponds to the index of the menu in StartMenuTransition

    private void Awake() {
        foreach (Image img in images) {
            //snaps color instead of lerping like in FixedUpdate
            Color targetColor = menuColors[transform.parent.GetComponent<StartMenuTransition>().CurrentMenu];
            targetColor.a = img.color.a;
            img.color = targetColor;
        }
    }

    private void FixedUpdate() {
        foreach (Image img in images) {
            Color targetColor = menuColors[transform.parent.GetComponent<StartMenuTransition>().CurrentMenu];
            targetColor.a = img.color.a;
            img.color = Color.Lerp(img.color, targetColor, lerpRate);
        }
    }

}
