using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    int index = 0;
    public float explosionSize;
    public float eplosionStrength;
    public int eplosionDamage;

    void FixedUpdate()
    {
        if (this.transform.localScale.x > explosionSize)
        {

            if (index == 60) { Destroy(gameObject); }
            index++;
        }
        else 
        {
            this.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "unit")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(eplosionStrength, this.transform.position, explosionSize, 1f, ForceMode.Impulse);
            collision.gameObject.GetComponent<unit>().health -= eplosionDamage;
        }
        
    }
}
