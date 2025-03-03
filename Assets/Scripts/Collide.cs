using Leap;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public class Collide : MonoBehaviour
{

    static public bool IsTouched = false;
    bool running = false;

    Vector3 lockPosition;
    Quaternion lockRotation;
    public void Start()
    {
        lockPosition = transform.position;
        lockRotation = transform.rotation;
    }

   

    public void Update()
    {
        transform.position = lockPosition;
        transform.rotation = lockRotation;

        if (IsTouched)
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;
            GetComponent<Renderer>().enabled = true;
            
        }

        else
        {
            GetComponent<MeshRenderer>().material.color= Color.white;
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<MeshRenderer>() == null)
        {
            IsTouched = true;
            GetComponent<MeshRenderer>().material.color = Color.yellow;
            GetComponent<Renderer>().enabled = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        IsTouched = false;
        GetComponent<MeshRenderer>().material.color= Color.white;

    }
    */
}


