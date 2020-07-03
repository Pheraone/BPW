using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformGun : MonoBehaviour
{
    public bool isShot = false;
    GameObject instBullet;
    public GameObject BulletPrefab;
    Rigidbody instBulletRigidbody;
    [SerializeField] float speed;
    public Platform freezePlatform;
    



    // Start is called before the first frame update
    void Start()
    {
       
    }

    

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(isShot);
       if (Input.GetMouseButtonDown(0) && isShot == false)
        {
            Debug.Log("shoot");
            Shoot();
            isShot = !isShot;

        }

       if (Input.GetMouseButtonDown(1) && isShot == true)
        {
            Stop();
            isShot = !isShot;
            
        }

        
    }

    void Shoot()
    {
        instBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
        instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.AddForce(transform.forward * speed);
        // Debug.Log("fire");
        // Debug.Log(speed);
        //FROZEN NAAR FALSE
        freezePlatform = instBullet.GetComponent<Platform>();
        freezePlatform.frozen = false;
    }

    void Stop()
    {

        instBulletRigidbody.velocity = Vector3.zero;
        instBulletRigidbody.angularVelocity = Vector3.zero;
        instBulletRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        //FROZEN NAAR TRUE
        freezePlatform = instBullet.GetComponent<Platform>();
        freezePlatform.frozen = true;
    }

   
}
