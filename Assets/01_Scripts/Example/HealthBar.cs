using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform healthBar;
    private Quaternion startRotation;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
    }
    public void UpdateHealthBar(float percentage)
    {
        var scale = healthBar.localScale;
        scale.x = percentage;
        healthBar.localScale = scale;
    }
    public void ResetHealthBar()
    {
        UpdateHealthBar(1f);
    }

    public void LateUpdate()
    {
        transform.rotation = startRotation;
        //Billboard
        //transform.LookAt(Camera.main.transform.position);
        //transform.Rotate(0, 180, 0);
    }
}
