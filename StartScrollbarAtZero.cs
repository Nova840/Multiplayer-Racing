using UnityEngine;
using System.Collections;

public class StartScrollbarAtZero : MonoBehaviour {

    void Start() {
        GetComponent<UnityEngine.UI.Scrollbar>().value = 0;
    }

}
