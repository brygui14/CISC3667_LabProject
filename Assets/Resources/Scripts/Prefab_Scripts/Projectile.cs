using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    int force = 5;
    
    void Start() {
        Destroy(this.gameObject, 5f);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * force);

    }

    
}
