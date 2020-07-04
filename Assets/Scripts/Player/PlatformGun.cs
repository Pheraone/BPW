using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class PlatformGun : MonoBehaviour
{
    //
    public bool isShot = false;
    private bool notEmpty = false;

    GameObject instBullet;
    public GameObject BulletPrefab;

    Rigidbody instBulletRigidbody;
    
    [SerializeField] float speed;
   
    public Platform freezePlatform;
   
    

    
    public List<GameObject> magazine = new List<GameObject>();
   
    private void Start()
    {
 
    }
    void Update()
    {
        
       //check if mousebutton is clicked and there is not yet been shot
       if (Input.GetMouseButtonDown(0) && isShot == false)
        {
            //shooting copy of platform 
            Debug.Log("shoot");
            Shoot();
            //switching shot to true
            isShot = !isShot;
            //for the magazine
            notEmpty = true;
        }

       //check if mousebutton2 is clicked and there is been shot 
       if (Input.GetMouseButtonDown(1) && isShot == true)
        {
            //stops platform
            Stop();
            //switching shot to false
            isShot = !isShot;
        }

       //check if more than 2 in magazine + if there was no gameObject (+ if the list is not empty)
       if(magazine.Count > 3 && notEmpty == true || instBullet == null && notEmpty == true)
        {
            //first platform in the list becomes a gameObject
            GameObject firstPlatform = magazine[0];
            
            Debug.Log(magazine.Count + "is outta here");
            //Destroy the first gameObject in the list
            Destroy(firstPlatform);
            //first gameObject is taken off list
            magazine.RemoveAt(0);

        } 

       //check if magazine is never 0
       if(magazine.Count <= 0)
        {
            notEmpty = false;
        }
        
    }

    void Shoot()
    {
        //making clone and instantiating it
        instBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
        //adding rigidbody + add force to move gameObject
        instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.AddForce(transform.forward * speed);

        //changing the frozing boolean to false
        freezePlatform = instBullet.GetComponent<Platform>();
        freezePlatform.frozen = false;

        //Add the clone to the magazinelist
        magazine.Add(instBullet);

    }

    void Stop()
    {
        //stopping/freezing bullet from moving
        instBulletRigidbody.velocity = Vector3.zero;
        instBulletRigidbody.angularVelocity = Vector3.zero;
        instBulletRigidbody.constraints = RigidbodyConstraints.FreezePosition;

        //changing frozen to true
        freezePlatform = instBullet.GetComponent<Platform>();
        freezePlatform.frozen = true;
    }

   
}
