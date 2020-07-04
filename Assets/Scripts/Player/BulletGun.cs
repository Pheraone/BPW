using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGun : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public float damage;
    public float range;
    public float impactForce;
    public float fireRate;
    private float nextFire;


    // Update is called once per frame
    private void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();

        }

        void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                targetHealth target = hit.transform.GetComponent<targetHealth>();

                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
        }

    }
}