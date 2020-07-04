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
    public bool mustBeDestroyed = false;

    void Start()
    {
        platformRB = this.gameObject.GetComponent<Rigidbody>();
        gun = GameObject.Find("PlatformGun").GetComponent<PlatformGun>();
        lifeTimer = lifeTime;
        
    }

    private void Update()
    {
        //if the platform is not moving
        if (frozen == true)
        {
            //set the layer to default
            platformRB.gameObject.layer = LayerMask.NameToLayer("Default");
           
            //lifetimer -1 every second
            lifeTimer -= Time.deltaTime;

            //checking if the timer is 0 or below
            if (lifeTimer <= 0f)
            {
               //destroying platform
                Destroy(gameObject);
                
            } 
        }
        else
        {
            //if platform is moving the layer will still be bullet
            platformRB.gameObject.layer = LayerMask.NameToLayer("Bullet");
        }
    }

    void OnCollisionEnter(Collision collision)
    {


        //stop platform from moving
        platformRB.velocity = Vector3.zero;
        platformRB.angularVelocity = Vector3.zero;
        platformRB.constraints = RigidbodyConstraints.FreezePosition;
        //setting is shot to false, player cannot stop a platform that already has stopped
        gun.isShot = false;
        //setting frozen to true/ platform has stopped
        frozen = true;
       

    }

}
