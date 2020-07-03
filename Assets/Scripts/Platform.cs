using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformGun gun;
    private Rigidbody platformRB;
    public bool frozen = false;
    [SerializeField] GameObject Player;
    public float lifeTime = 5f;
    private float lifeTimer;

    void Start()
    {
        platformRB = this.gameObject.GetComponent<Rigidbody>();
        gun = GameObject.Find("PlatformGun").GetComponent<PlatformGun>();
        lifeTimer = lifeTime;
        
    }

    private void Update()
    {
        if(frozen == true)
        {
            platformRB.gameObject.layer = LayerMask.NameToLayer("Default");
            Debug.Log("frozen is " + frozen);
        }
        else
        {
            platformRB.gameObject.layer = LayerMask.NameToLayer("Bullet");
            Debug.Log("frozen is "+ frozen);
        }

        if (frozen == true)
        {
            lifeTimer -= Time.deltaTime;

            if (lifeTimer <= 0f)
            {
                Debug.Log("I am doing something " + lifeTime);
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {


        Debug.Log("hit");
        platformRB.velocity = Vector3.zero;
        platformRB.angularVelocity = Vector3.zero;
        platformRB.constraints = RigidbodyConstraints.FreezePosition;
        gun.isShot = false;
        frozen = true;
        Debug.Log(frozen);
        Debug.Log(collision.gameObject);
        /* if (Collision collision.gameObject.tag == "Player")
         {
             Debug.Log("oof");
             Physics.IgnoreCollision(collision.gameObject.GetComponent<CapsuleCollider>(), platformRB.GetComponent<SphereCollider>());
         }*/

    }

}
