using UnityEngine;
using System.Collections;

public class CarColors : MonoBehaviour {

    [SerializeField]
    private Texture blueTexture = null, redTexture = null, yellowTexture = null, greenTexture = null;

    public Texture BlueTexture { get { return blueTexture; } }
    public Texture RedTexture { get { return redTexture; } }
    public Texture YellowTexture { get { return yellowTexture; } }
    public Texture GreenTexture { get { return greenTexture; } }

}
