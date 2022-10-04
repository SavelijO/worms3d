using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gun : MonoBehaviour
{
    public Camera cam;
    private Vector3 destination;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject gunTypeUI;
    public Texture defaultGunType;

    void Start()
    {
        gunTypeUI.GetComponent<RawImage>().texture = defaultGunType;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && projectilePrefab!= null)
        {
            Shoot();
        }
        if(projectilePrefab != null)
        {
            gunTypeUI.GetComponent<RawImage>().texture = projectilePrefab.GetComponent<projectile>().gunTypeIcon;
        }
        else
        {
            gunTypeUI.GetComponent<RawImage>().texture = defaultGunType;
        }
    }

    void Shoot()
    {
        Ray ray =  cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        destination = ray.GetPoint(1000);

        InstanciateProjectile();
    }

    void InstanciateProjectile()
    {
        GameObject projectlile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectlile.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectlile.GetComponent<projectile>().speed;
        projectilePrefab = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickUp")
        {
            if (!projectilePrefab)
            {
                projectilePrefab = other.gameObject.GetComponent<pickUp>().projectilePrefab;
                Destroy(other.gameObject);
            }
        }

    }
}
