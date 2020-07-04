using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetHealth : MonoBehaviour
{

    private Renderer colorchange;
    private Color colorStart;
    Color colorAttack;
    public bool iGotShot = false;
    public float timeToChange = 0.3f;
    private float timeSinceChange;
    float health = 100;
    
    void start()
    {
        colorStart = this.GetComponent<Renderer>().material.GetColor("_Color");

    }


    public void TakeDamage(float amount)
    {
        Debug.Log("Damage taken" + amount);
        iGotShot = true;
        health = health -amount;
        Debug.Log(health);
    }

    void Update()
    {
 
        if (iGotShot == false)
        {
            colorchange = this.gameObject.GetComponent<Renderer>();
            colorchange.material.SetColor("_Color", colorStart);
        }
        else if (iGotShot == true)
        {
            colorchange = this.gameObject.GetComponent<Renderer>();
            colorchange.material.SetColor("_Color", Color.red);

            timeSinceChange += Time.deltaTime; 
            if (timeSinceChange >= timeToChange)
            {
                iGotShot = false;
                timeSinceChange = 0f;
            }
        }

        if(health <= 0 && iGotShot == false)
        {
            Destroy(gameObject);
        }
    }

}
