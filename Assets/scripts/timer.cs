using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float deflaultTime;
    public float leftTime;
    
    void Start()
    {
        leftTime = deflaultTime;
    }

   
    void Update()
    {
        leftTime -= Time.deltaTime;
    }

}
