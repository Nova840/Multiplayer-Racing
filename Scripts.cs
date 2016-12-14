using UnityEngine;
using System.Collections;

public class Scripts : MonoBehaviour {

    /*
    This class should always be attached to the Scripts GameObject so you can call Scripts.ScriptsGameObject to get it.
    Much better than using GameObject.Find("Scripts").
    */

    private static GameObject scriptsGameObject;
    public static GameObject ScriptsGameObject { get { return scriptsGameObject; } }

    private void Awake() {
        scriptsGameObject = this.gameObject;
    }

}
