using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool canExplosion {
        get;
        set;
    }
    public GameObject explosionFactory;

    private void OnCollisionEnter(Collision collision)
    {
        // �տ� ����������� ������ ���� �ʴ�.
        if (false == canExplosion)
        {
            return;
        }
        // ����VFX�� ǥ���ϰ�
        Vector3 hitPoint = collision.contacts[0].point;
        GameObject exp = Instantiate(explosionFactory);
        exp.transform.position = hitPoint;
        Destroy(exp, 2);
        // ���� �����ϰ�ʹ�.
        // -> �ε��� ��ġ��
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] cols = Physics.OverlapSphere(hitPoint, 5, layerMask);
        // �ݰ� 5M���� ������ 2�� �������� ������ �׾��ٸ� Boom���·� �����ϰ� �ϰ�ʹ�.
        for (int i=0; i<cols.Length; i++)
        {
            Enemy enemy = cols[i].GetComponent<Enemy>();
            if (null != enemy)
            {
                enemy.OnMyDamageProcess(2, true);
            }

        }
        Destroy(gameObject);

    }
}
