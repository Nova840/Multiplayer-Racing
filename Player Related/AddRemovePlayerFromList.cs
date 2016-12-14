using UnityEngine;
using System.Collections;

public class AddRemovePlayerFromList : MonoBehaviour {

    //Also added in the PlayersList script in case a players is enabled before the Scripts GameObject.
    private void OnEnable() {
        GameObject g = Scripts.ScriptsGameObject;
        if (g != null)//in case player is enabled before scripts.
            g.GetComponent<Players>().addPlayerToList(gameObject);
    }

    private void OnDisable() {
        GameObject scripts = Scripts.ScriptsGameObject;
        if (scripts != null)//can be null when you start from the game scene.
            scripts.GetComponent<Players>().removePlayerFromList(gameObject);
        else
            Debug.LogWarning("Scripts GameObject Null ( AddRemovePlayerFromList OnDisable() )");
    }
}
