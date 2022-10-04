using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unit : MonoBehaviour
{
    public int health;
    public Vector3 position;
    public Color32 color;
    public GameObject healthBar;

    public Shader shader;
    private Renderer rend;

    void Start()
    {
        rend = this.GetComponent<Renderer>();
        rend.material = new Material(shader);
        rend.sharedMaterial.color = color;
        healthBar.GetComponent<Text>().text = health.ToString();
    }

    private void Update()
    {

        healthBar.GetComponent<Text>().text = health.ToString();
        if(this.transform.position.y <= 0)
        {
            health = 0;
        }
        
        if(health == 0)
        { 
            Destroy(this.gameObject);
        }
    }
}
