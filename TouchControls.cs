using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour
{
    private bool leftButton, rightButton;

    public float Axis
    {
        get
        {
            float axis = 0;

            if (leftButton)
                axis--;
            if (rightButton)
                axis++;

            return axis;
        }
    }

    public void LeftButtonDown()
    {
        leftButton = true;
    }

    public void LeftButtonUp()
    {
        leftButton = false;
    }

    public void RightButtonDown()
    {
        rightButton = true;
    }

    public void RightDuttonUp()
    {
        rightButton = false;
    }
}
