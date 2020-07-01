using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitch : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchGun();
        }   
    }
    void SwitchGun()
    {
        foreach(Transform gun in transform)
        {
            gun.gameObject.SetActive(!gun.gameObject.activeSelf);
        }
    }
}
