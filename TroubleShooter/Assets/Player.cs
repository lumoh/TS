using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Gun")]
    public Transform GunT;
    public Transform BulletSpawnT;
    public float FireCooldown;
    public Bullet BulletPrefab;

    [Header("Movement")]
    public float Speed = 15f;
    public float Drag = 30f;

    Rigidbody rb;
    float vInput = 0f;
    float hInput = 0f;
    bool firing = false;
    bool firingOnCd = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = Drag;
    }

    private void Update()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");

        pointGun();
        fireUpdate();
    }

    void pointGun()
    {
        Camera cam = Camera.main;
        Vector3 mousePos = Input.mousePosition;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 9.25f));
        float z = Mathf.Atan2((point.y - transform.position.y), (point.x - transform.position.x)) * Mathf.Rad2Deg;
        GunT.rotation = Quaternion.Euler(new Vector3(0, 0, z));
    }

    void fireUpdate()
    {
        firing = Input.GetAxis("Fire1") == 1f;
        if (firing && !firingOnCd)
        {
            fire();
        }
    }

    void fire()
    {
        StartCoroutine(fireCd());
        Bullet bullet = Instantiate(BulletPrefab, BulletSpawnT.position, BulletSpawnT.rotation);
        Vector3 dir = (BulletSpawnT.position - GunT.position).normalized;
        bullet.Init(dir);
    }

    IEnumerator fireCd()
    {
        firingOnCd = true;
        yield return new WaitForSeconds(FireCooldown);
        firingOnCd = false;
    }

    private void FixedUpdate()
    {
        Vector3 movemoventForce = new Vector3(hInput, vInput, 0) * Speed;
        rb.AddForce(movemoventForce, ForceMode.VelocityChange);
    }
}
