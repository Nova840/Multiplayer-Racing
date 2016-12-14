using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameInput : MonoBehaviour {

    private void Start() {
        GetComponent<InputField>().text = PlayerPrefs.GetString(StoredKeys.playerName);
    }

    public void OnEndEdit(string playerName) {
        PlayerPrefs.SetString(StoredKeys.playerName, playerName);
        PlayerPrefs.Save();
    }

}
