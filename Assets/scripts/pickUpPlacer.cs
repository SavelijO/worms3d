using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpPlacer : MonoBehaviour
{
    public GameObject pickUpPrefab;
    public GameObject pickUp;
    private void Start()
    {
        InvokeRepeating("Place", 0, 5f);
    }

    private void Place()
    {
        if(pickUp == null)
        {
            pickUp = Instantiate(pickUpPrefab, this.transform);
        }
    }
}
