using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnManager : MonoBehaviour
{
    [Range(1,4)] public int playerCount;

    public List<player> playerArr = new List<player>();
    public Color[] colorArr;
    private player currentPlayer;
    public Vector3[] spawnPosArr;
    public int unitCount;
    public GameObject playerPrefab;
    private GameObject tempGameObject;
    
    void Start()
    {
        for (int i = 0; i < playerCount; i++)
        {

            tempGameObject = Instantiate(playerPrefab, spawnPosArr[i], Quaternion.identity);
            playerArr.Add(tempGameObject.GetComponent<player>());
            playerArr[i].color = colorArr[i];


        } 
        currentPlayer = playerArr[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
