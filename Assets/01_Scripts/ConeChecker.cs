using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeChecker : MonoBehaviour
{
    public Light light;
    private Renderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.SetFloat("_ConeHeight", light.range);
        meshRenderer.material.SetFloat("_ConeAngle", light.spotAngle);
        meshRenderer.material.SetVector("_ConeOrigin", light.transform.position);
        meshRenderer.material.SetVector("_ConeDirection", light.transform.forward);
        meshRenderer.material.SetColor("_LightColor", light.color);
        //bool isInCone = IsInCone(light, transform.position);
        //GetComponent<MeshRenderer>().material.color = isInCone ? Color.green : Color.red;
    }

    public bool IsInCone(Light light, Vector3 point)
    {
        float height = light.range;
        float radius = Mathf.Tan(Mathf.Deg2Rad * light.spotAngle * 0.5f) * height;
        Vector3 dir = light.transform.forward;
        Vector3 origin = light.transform.position;

        float coneDistance = Vector3.Dot(point - origin, dir);
        if (coneDistance < 0 || coneDistance > height) return false;
        float coneRadius = (coneDistance / height) * radius;
        float orthDistance = Vector3.Magnitude((point - origin) - coneDistance * dir);
        return orthDistance < coneRadius;
    }
}
