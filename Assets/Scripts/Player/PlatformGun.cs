﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject instBullet;
    [SerializeField] public float speed;
  
    //[SerializeField] public int damage;

    

    // Start is called before the first frame update
    void Start()
    {
        
      
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject instBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidbody.AddForce(transform.forward * speed);
            Debug.Log("fire");
            
        }

        
      
      
    }
    void OnCollisionEnter(Collision collision)
    {

    }
}
