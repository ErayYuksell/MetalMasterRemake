using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ShatterPart")
        {
            var shatterControl = collision.gameObject.GetComponent<ShatterControl>();
            shatterControl.transform.parent = transform;
        }
    }
    //private void (Collider other)
    //{
    //    if (other.CompareTag("ShatterPart"))
    //    {
    //        var shatterControl = other.GetComponent<ShatterControl>();
    //        shatterControl.transform.parent = transform;
    //    }
    //}
}
