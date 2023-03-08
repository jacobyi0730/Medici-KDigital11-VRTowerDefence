using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody rb;
    public float force = 20;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (false == rb.isKinematic && rb.velocity != Vector3.zero)
        {
            transform.forward = rb.velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        StartCoroutine("IEAutoDestroy", 2);
    }

    IEnumerator IEAutoDestroy(float time)
    {
        Material mat = GetComponent<MeshRenderer>().material;
        Color c = mat.color;
        // 1초동안 투명해지고싶다.
        for (float t = 0; t <= time; t += Time.deltaTime)
        {
            c.a = 1 - (t / time);
            mat.color = c;
            yield return 0;
        }
        // 1초가 지나면 파괴되고싶다.
        Destroy(gameObject);
    }
}
