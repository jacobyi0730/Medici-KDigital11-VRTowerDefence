using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ʈ���� ��ư�� ������ �ڷ���Ʈ ���� Ȱ��ȭ �ǰ�
// Ʈ���� ��ư�� ���� �ڷ���Ʈ ���� ��Ȱ��ȭ �ȴ�.
// ��ư�� ���� �� �ε��� ���� Ÿ����� �װ����� �̵��ϰ�ʹ�.
public class Teleport : MonoBehaviour
{
    // Ʈ���� ��ư�� �ϴ� ���콺 ������ ��ư�� �ǹ��Ѵ�.
    // ��.
    LineRenderer lr;
    // ��.
    public Transform hand;
    public Player player;
    Transform target = null;
    
    public Transform teleportIcon;
    // �־����� �Ȱ��� ũ��� ����ʹ�.
    public float kAdjust = 1;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }

        // �¾ �� ���� �������� ��Ȱ�� �ϰ�ʹ�.
        lr.enabled = false;
        teleportIcon.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
            // ���� ���� �� Ÿ���� �ٶ󺸰� �־��ٸ� �װ����� �̵�.
            if (target != null)
            {
                player.transform.position = target.position;
                target = null;
            }
            // �׷��� �ʴٸ� �ƹ��͵� ����.
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
