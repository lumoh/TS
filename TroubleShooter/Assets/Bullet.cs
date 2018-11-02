using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 100f;
    public float Lifetime = 3f;

    public void Init(Vector3 dir)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(dir * Speed, ForceMode.VelocityChange);
        Destroy(gameObject, Lifetime);
    }
}
