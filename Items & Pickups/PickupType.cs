using UnityEngine;
using System.Collections;

public class PickupType : MonoBehaviour {
    [SerializeField]
    private Pickup thisPickupType;
    public Pickup ThisPickupType { get { return thisPickupType; } }
}
