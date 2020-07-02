using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGun : MonoBehaviour
{
    private bool isShot = false;
    GameObject instBullet;
    public GameObject BulletPrefab;
    Rigidbody instBulletRigidbody;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0)/* && isShot*/)
        {
            Debug.Log("shoot");
            Shoot();
            isShot = !isShot;
        }

       if (Input.GetMouseButtonDown(1) /*&& !isShot*/)
        {
            Stop();
            isShot = !isShot;
        }
    }

    void Shoot()
    {
        GameObject instBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
        instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.AddForce(transform.forward * speed);
        Debug.Log("fire");
        Debug.Log(speed);
    }

    void Stop()
    {

        instBulletRigidbody.velocity = Vector3.zero;
        instBulletRigidbody.angularVelocity = Vector3.zero;
    }
}
