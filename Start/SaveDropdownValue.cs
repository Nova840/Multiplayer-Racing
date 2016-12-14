using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
This script has a higher default time in the script execution order
because other scripts (like quality dropdown) should happen before this one.
*/
public class SaveDropdownValue : MonoBehaviour {

    [SerializeField]
    private string key;//NOT type safe

    private void OnEnable() {
        int storedValue = PlayerPrefs.GetInt(key, -1);
        if (storedValue > -1) {
            GetComponent<Dropdown>().value = storedValue;
        } else {
            PlayerPrefs.SetInt(key, GetComponent<Dropdown>().value);
            PlayerPrefs.Save();
        }
    }

    public void OnDropdownValueChanged(int option) {
        PlayerPrefs.SetInt(key, option);
        PlayerPrefs.Save();
    }

}
