using UnityEngine;
using System.Collections;

public class SetInputFieldTextToStoredIP : MonoBehaviour {

    private void Awake() {
        GetComponent<UnityEngine.UI.InputField>().text = PlayerPrefs.GetString(StoredKeys.lastJoinedIP);
    }

}
