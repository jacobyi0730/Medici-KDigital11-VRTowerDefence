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
        // 손에 쥐고있을때는 터지고 싶지 않다.
        if (false == canExplosion)
        {
            return;
        }
        // 폭발VFX를 표현하고
        Vector3 hitPoint = collision.contacts[0].point;
        GameObject exp = Instantiate(explosionFactory);
        exp.transform.position = hitPoint;
        Destroy(exp, 2);
        // 범위 폭발하고싶다.
        // -> 부딪힌 위치의
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] cols = Physics.OverlapSphere(hitPoint, 5, layerMask);
        // 반경 5M안의 적에게 2점 데미지를 입히고 죽었다면 Boom상태로 전이하게 하고싶다.
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
