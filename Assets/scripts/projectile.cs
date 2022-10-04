using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    public float explosionSize;
    public float eplosionStrength;
    public int eplosionDamage;
    public GameObject explosion;
    public Texture gunTypeIcon;


    private void Start()
    {
        this.transform.rotation = Quaternion.LookRotation(transform.InverseTransformDirection(this.GetComponent<Rigidbody>().velocity));
    }

    private void FixedUpdate()
    {
        this.transform.rotation = Quaternion.LookRotation(this.GetComponent<Rigidbody>().velocity, Vector3.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "gun")
        {
            GameObject tempObj = Instantiate(explosion);
            tempObj.transform.position = collision.contacts[0].point;
            tempObj.GetComponent<explosion>().explosionSize = explosionSize;
            tempObj.GetComponent<explosion>().eplosionStrength = eplosionStrength;
            tempObj.GetComponent<explosion>().eplosionDamage = eplosionDamage;
            Destroy(gameObject);
        }

    }
}
