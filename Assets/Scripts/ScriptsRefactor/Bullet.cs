using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPool
{
    [SerializeField] float lifeTime;
    Rigidbody rb;
    Vector3 Initial;

    public void Instantiate()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Initial = transform.position;
    }
    public void Spawn(Vector3 position)
    {
        transform.position = position;
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        Invoke("Destroy", lifeTime);
    }
    public void Destroy()
    {
        transform.position = Initial;
        rb.isKinematic = true;
    }
}
