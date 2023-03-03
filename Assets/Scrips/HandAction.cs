using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���콺 ���� ��ư�� ������ hand���� hand�� �չ�������
// raycast�� �̿��ؼ� ���� ���ʹ�.
// �ε��� ��ġ�� �Ѿ��ڱ��� �����ʹ�.
[RequireComponent(typeof(LineRenderer))]
public class HandAction : MonoBehaviour
{
    enum Hall
    {
        NO,
        YES
    }
    public Transform hand;
    public GameObject[] bImpactFactoryList;

    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // hand���� hand�� �չ�������
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        RaycastHit hitInfo;
        // raycast�� �̿��ؼ� ���� ���ʹ�.
        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
            // ���콺 ���� ��ư�� ������
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject prefab = null;
                // �ε��� ��ġ�� �Ѿ��ڱ��� �����ʹ�.
                if (hitInfo.transform.CompareTag("Enemy"))
                {
                    prefab = bImpactFactoryList[(int)Hall.NO];
                }
                else
                {
                    prefab = bImpactFactoryList[(int)Hall.YES];
                }
                GameObject bi = Instantiate(prefab);

                bi.transform.position = hitInfo.point;
                bi.transform.forward = hitInfo.normal;
            }
        }
        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 1000);
        }
    }
}
