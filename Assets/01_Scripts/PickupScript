using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private GameObject pickup;
    public Transform pickupTransform; //Used for raycasting
    public LayerMask pickupLayer;
    public float pickupDistance = 2.0f;
    public Vector3 pickupOffset;
    private void Awake()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !HasPickup())
        {
            RaycastHit hit;
            if(Physics.Raycast(pickupTransform.position, pickupTransform.forward, out hit, pickupDistance, pickupLayer))
            {
                Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    //Pickup the object
                    pickup = rb.gameObject;
                    rb.isKinematic = true;
                    hit.collider.enabled = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasPickup())
        {
            DropPickup();
        }

        if (HasPickup())
        {
            pickup.transform.position = pickupTransform.position + pickupTransform.TransformDirection(pickupOffset);
            Vector3 XZForward = pickupTransform.forward;
            XZForward.y = 0;
            XZForward.Normalize();
            pickup.transform.rotation = Quaternion.LookRotation(XZForward);
        }
    }

    public void DropPickup()
    {
        Rigidbody rb = pickup.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.GetComponent<Collider>().enabled = true;
        pickup = null;
    }

    public bool HasPickup()
    {
        return pickup != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pickupTransform.position, pickupTransform.position + pickupTransform.forward * pickupDistance);
    }
}
