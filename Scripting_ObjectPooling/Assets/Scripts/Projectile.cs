using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false)
        //Destroy(gameObject, 2.0f); //TODO: Instead of destroying, reuse        
    }

    private void OnEnable()
    {
        Invoke(nameof(DeActivateProjectile), 1.0f);
    }

    public void DeActivateProjectile()
    {
        gameObject.SetActive(false); //After projectile is used, deactivate it instead of destroying it so that it may be recycled/reused

        //Debug.Log("Projectile deactivated");
    }    
}
