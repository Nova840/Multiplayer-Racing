using UnityEngine;
using System.Collections;

public class CarsDropdown : MonoBehaviour {

    public void OnCarChanged(int car) {
        PlayerPrefs.SetInt(StoredKeys.currentCar, car);
        PlayerPrefs.Save();
    }

}
