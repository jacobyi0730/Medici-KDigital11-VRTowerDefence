using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInteraction : MonoBehaviour
{
    public Transform hand;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(hand.position, hand.forward);

        RaycastHit hitInfo;
        // raycast를 이용해서 버튼을 누르고싶다.
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                UnityEngine.UI.Button button = hitInfo.transform.GetComponent<UnityEngine.UI.Button>();
                if (null != button)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }
}
