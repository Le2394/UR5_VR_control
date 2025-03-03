using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Sphere : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
}
