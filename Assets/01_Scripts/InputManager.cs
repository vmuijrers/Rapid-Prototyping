using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputData inputData;
    
    // Update is called once per frame
    void Update()
    {
        //Change this so it fits your project
        inputData.NorthButtonPressed = Input.GetKeyDown(KeyCode.W);
        inputData.EastButtonPressed = Input.GetKeyDown(KeyCode.D);
        inputData.SouthButtonPressed = Input.GetKeyDown(KeyCode.S);
        inputData.WestButtonPressed = Input.GetKeyDown(KeyCode.A);
        inputData.LeftMouseButtonPressed = Input.GetKeyDown(KeyCode.Mouse0);
        inputData.RightMouseButtonPressed = Input.GetKeyDown(KeyCode.Mouse1);
        inputData.MiddleMouseButtonPressed = Input.GetKeyDown(KeyCode.Mouse2);
        inputData.VerticalAxis = Input.GetAxis("Vertical");
        inputData.HorizontalAxis = Input.GetAxis("Horizontal");
    }
}

public class InputData
{
    public bool NorthButtonPressed;
    public bool EastButtonPressed;
    public bool SouthButtonPressed;
    public bool WestButtonPressed;
    public float VerticalAxis;
    public float HorizontalAxis;
    public bool LeftMouseButtonPressed;
    public bool RightMouseButtonPressed;
    public bool MiddleMouseButtonPressed;
}