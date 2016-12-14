using UnityEngine;
using System.Collections;

public class AddItemToList : MonoBehaviour {

    private void OnEnable() {
        Scripts.ScriptsGameObject.GetComponent<Items>().AddItemToList(gameObject);
    }

    private void OnDisable() {
        Scripts.ScriptsGameObject.GetComponent<Items>().RemoveItemFromList(gameObject);
    }

}
