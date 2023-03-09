using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 트리거 버튼을 누르면 텔레포트 선이 활성화 되고
// 트리거 버튼을 떼면 텔레포트 선이 비활성화 된다.
// 버튼을 뗏을 때 부딪힌 곳이 타워라면 그곳으로 이동하고싶다.
public class Teleport : MonoBehaviour
{
    // 트리거 버튼은 일단 마우스 오른쪽 버튼을 의미한다.
    // 선.
    LineRenderer lr;
    // 손.
    public Transform hand;
    public Player player;
    Transform target = null;
    
    public Transform teleportIcon;
    // 멀어져도 똑같은 크기로 보고싶다.
    public float kAdjust = 1;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }

        // 태어날 때 선과 아이콘을 비활성 하고싶다.
        lr.enabled = false;
        teleportIcon.gameObject.SetActive(false);
    }

    void Update()
    {
        //UpdateForPC();
        UpdateForOculus();
    }

    public OVRInput.Button teleportButton;
    public OVRInput.Controller teleportController;
    void UpdateForOculus()
    {
        // 왼쪽 컨트롤러의 One
        if (OVRInput.GetDown(teleportButton, teleportController))
        {
            lr.enabled = true;
            teleportIcon.gameObject.SetActive(true);
        }
        if (OVRInput.GetUp(teleportButton, teleportController))
        {
            lr.enabled = false;
            teleportIcon.gameObject.SetActive(false);
            // 손을 뗏을 때 타워를 바라보고 있었다면 그곳으로 이동.
            if (target != null)
            {
                player.transform.position = target.position;
                target = null;
            }
            // 그렇지 않다면 아무것도 안함.
        }

        if (lr.enabled /*&& Input.GetButton("Fire2")*/)
        {
            Ray ray = new Ray(hand.position, hand.forward);
            lr.SetPosition(0, hand.position);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                lr.SetPosition(1, hitInfo.point);

                teleportIcon.position = hitInfo.point;
                teleportIcon.up = hitInfo.normal;
                teleportIcon.localScale = Vector3.one * hitInfo.distance * kAdjust;

                if (hitInfo.transform.name.Contains("Tower"))
                {
                    target = hitInfo.transform;
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                target = null;
                Vector3 endPos = hand.position + hand.forward * 1000;
                lr.SetPosition(1, endPos);

                teleportIcon.position = endPos;
                teleportIcon.up = -hand.forward;
                teleportIcon.localScale = Vector3.one * 1000 * kAdjust;
            }
        }
    }

    // Update is called once per frame
    void UpdateForPC()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            lr.enabled = true;
            teleportIcon.gameObject.SetActive(true);
        }

        if (Input.GetButtonUp("Fire2"))
        {
            lr.enabled = false;
            teleportIcon.gameObject.SetActive(false);
            // 손을 뗏을 때 타워를 바라보고 있었다면 그곳으로 이동.
            if (target != null)
            {
                player.transform.position = target.position;
                target = null;
            }
            // 그렇지 않다면 아무것도 안함.
        }

        if (lr.enabled /*&& Input.GetButton("Fire2")*/)
        {
            Ray ray = new Ray(hand.position, hand.forward);
            lr.SetPosition(0, hand.position);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                lr.SetPosition(1, hitInfo.point);

                teleportIcon.position = hitInfo.point;
                teleportIcon.up = hitInfo.normal;
                teleportIcon.localScale = Vector3.one * hitInfo.distance * kAdjust;

                if (hitInfo.transform.name.Contains("Tower"))
                {
                    target = hitInfo.transform;
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                target = null;
                Vector3 endPos = hand.position + hand.forward * 1000;
                lr.SetPosition(1, endPos);

                teleportIcon.position = endPos;
                teleportIcon.up = -hand.forward;
                teleportIcon.localScale = Vector3.one * 1000 * kAdjust;
            }
        }

       
      
    }
}
