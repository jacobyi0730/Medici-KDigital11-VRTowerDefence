using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    private void Awake()
    {
        Player.instance = this;
    }

    public AnimationCurve animCurve;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            MyHaptic.instance.PlayVibration(false, 1000, 0.3f);
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            MyHaptic.instance.PlayVibration(true, 1000, 0.3f);
        }
    }

    
}
