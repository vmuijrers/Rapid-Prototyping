using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public float currentAngle;
    private float targetAngle;
    public float sensitivity = 1;
    public float lerpSpeed = 1;
    private Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hor = (Input.mousePosition - lastPos).x;
        targetAngle += hor * sensitivity * Time.deltaTime;
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, lerpSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        lastPos = Input.mousePosition;
    }
}
