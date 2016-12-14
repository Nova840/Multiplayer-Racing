using UnityEngine;
using System.Collections;

public class LevelData : MonoBehaviour
{
    [SerializeField]
    private int laps = 0;
    public int Laps { get { return laps; } }
}
