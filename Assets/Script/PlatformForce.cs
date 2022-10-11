using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformForce : MonoBehaviour
{

    [SerializeField]private float Degree;
    [SerializeField] private float Force;


    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Degree,90,0)*Force,ForceMode.Force);
    }
}
