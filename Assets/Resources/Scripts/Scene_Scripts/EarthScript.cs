using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthScript : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0,0,10 * Time.deltaTime));       
    }
}
