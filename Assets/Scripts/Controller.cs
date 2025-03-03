using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Transform end_point;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //end_point.position = Vector3.Lerp(end_point.position, target.position, Time.deltaTime * 5.0f);
        end_point.position = target.position;
        end_point.rotation = Quaternion.Slerp(end_point.rotation, target.rotation, Time.deltaTime * 5.0f);

        /*Vector3 targetEuler = target.rotation.eulerAngles;
        Vector3 endEuler = end_point.rotation.eulerAngles;
        
        endEuler.x = -targetEuler.z + 90;
        endEuler.y = -targetEuler.x + 90;
        endEuler.z = targetEuler.y + 450.0f;

        end_point.rotation = Quaternion.Euler(endEuler);*/
        Debug.Log(end_point.eulerAngles);
    }
}
