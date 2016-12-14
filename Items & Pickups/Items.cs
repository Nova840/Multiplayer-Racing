using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Items : MonoBehaviour {

    private List<GameObject> itemsList = new List<GameObject>();
    public List<GameObject> ItemsList { get { return itemsList; } }

    public void AddItemToList(GameObject item) {
        if (!item.CompareTag("Item")) {
            Debug.LogWarning("Tried to add a non-item to the items list");
            return;
        }

        itemsList.Add(item);
    }

    public void RemoveItemFromList(GameObject item) {
        itemsList.Remove(item);
    }

}
