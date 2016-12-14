using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreenFade : MonoBehaviour {

    [SerializeField]
    private float fadeTime = .5f;

    private void Awake() {
        foreach (Graphic g in GetComponentsInChildren<Graphic>()) {
            g.CrossFadeAlpha(0, 0, false);
            g.CrossFadeAlpha(1, fadeTime, false);
        }
    }

}
