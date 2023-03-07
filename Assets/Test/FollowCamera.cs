using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform lookTarget;
    public Transform origin;

    void Update()
    {
        Vector3 dir = origin.position - lookTarget.position;
        Ray ray = new Ray(lookTarget.position, dir);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, dir.magnitude))
        {
            transform.position = hitInfo.point;
        }
        else
        {
            transform.position = origin.position;
        }
        transform.LookAt(lookTarget);
    }

}
