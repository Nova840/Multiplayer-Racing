using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using System;

public class QualityDropDown : MonoBehaviour {

    private void Awake() {//This gameobject is reset when network is reset for some reason.
        if (GetComponent<SaveDropdownValue>() != null)//The SaveDropdownValue component will conflict with this component because this script adds things to the dropdown.
            throw new Exception("This component can't have a SaveDropdownValue component");

        Dropdown dDown = GetComponent<Dropdown>();
        dDown.AddOptions(QualitySettings.names.ToList<string>());

        int storedValue = PlayerPrefs.GetInt(StoredKeys.quality, -1);
        if (storedValue > -1) {
            GetComponent<Dropdown>().value = storedValue;
        } else {
            //Quality is at default here.
            GetComponent<Dropdown>().value = QualitySettings.GetQualityLevel();
            PlayerPrefs.SetInt(StoredKeys.quality, GetComponent<Dropdown>().value);
            PlayerPrefs.Save();
        }

        if (storedValue > -1 && storedValue != QualitySettings.GetQualityLevel())
            QualitySettings.SetQualityLevel(storedValue, true);

    }

    public void OnQualityChanged(int option) {
        QualitySettings.SetQualityLevel(option, true);
        PlayerPrefs.SetInt(StoredKeys.quality, GetComponent<Dropdown>().value);
        PlayerPrefs.Save();
    }

}
