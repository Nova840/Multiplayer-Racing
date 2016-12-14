using UnityEngine;
using System.Collections;

public class LapsText : MonoBehaviour
{

    [SerializeField]
    private LevelData levelData;

    public void Start()
    {
        GetComponent<UnityEngine.UI.Text>().text = "Lap 1/" + levelData.Laps;
    }

}
