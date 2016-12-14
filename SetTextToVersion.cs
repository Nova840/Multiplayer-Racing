using UnityEngine;
using System.Collections;

public class SetTextToVersion : MonoBehaviour {

    void Start() {
        GetComponent<UnityEngine.UI.Text>().text = "v" + Application.version;
    }

}
