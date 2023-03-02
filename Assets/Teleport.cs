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
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }
        //lr.widthCurve = Player.instance.animCurve;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            lr.enabled = true;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            lr.enabled = false;
            // ���� ���� �� Ÿ���� �ٶ󺸰� �־��ٸ� �װ����� �̵�.
            if (target != null)
            {
                player.transform.position = target.position;
                target = null;
            }
            // �׷��� �ʴٸ� �ƹ��͵� ����.
        }

        if (lr.enabled && Input.GetButton("Fire2"))
        {
            Ray ray = new Ray(hand.position, hand.forward);
            lr.SetPosition(0, hand.position);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                lr.SetPosition(1, hitInfo.point);
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
                lr.SetPosition(1, hand.position + hand.forward * 1000);
            }
        }

       
      
    }
}
