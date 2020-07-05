using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletGun : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public float damage;
    public float range;
    public float impactForce;
    public float fireRate;
    private float nextFire;
    public AudioClip piewSound;


    AudioSource audioSource;
    // Update is called once per frame
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        //Make sound 3D
        audioSource.spatialBlend = 1f;
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

            audioSource.clip = piewSound;
            audioSource.Play();

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Enemy target = hit.transform.GetComponent<Enemy>();

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