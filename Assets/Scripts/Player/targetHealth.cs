using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetHealth : MonoBehaviour
{
  public void TakeDamage(float amount)
    {
        Debug.Log("Damage taken" + amount);
    }
}
