using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class turnManager : MonoBehaviour
{

    //Player-and-unit-setup-------------//
    [Range(1, 4)] public int playerCount;

    public List<player> playerArr = new List<player>();

    public Color[] colorArr;

    private player currentPlayer;

    private bool isFirstFrame = true;

    public Transform[] spawnPosArr;
    
    public int unitCount;
    
    public GameObject playerPrefab;

    public GameObject playerController;

    private GameObject tempGameObject;
    
    private int currentPlayerIndex=0;

    public GameObject gun;
    //----------------------------------//

    
    //Turn-timer-----------------------//
    public GameObject timerPrefab;
    private timer timer;
    public GameObject timerUI;
    public float defaultTime;
    public GameObject winScreenUI;
    //----------------------------------//


    void Start()
    {
        tempGameObject = Instantiate(timerPrefab, this.transform);
        timer = tempGameObject.GetComponent<timer>();
        timer.deflaultTime = defaultTime;
        
        for (int i = 0; i < playerCount; i++)
        {

            tempGameObject = Instantiate(playerPrefab, spawnPosArr[i].position, Quaternion.identity);
            tempGameObject.name = "player " + (i+1).ToString();
            playerArr.Add(tempGameObject.GetComponent<player>());
            playerArr[i].color = colorArr[i];
            playerArr[i].unitCount = unitCount;

        }
        currentPlayer = playerArr[0];
        playerController.GetComponent<CharacterController>().enabled = false;
        playerController.transform.position = spawnPosArr[0].position;
        playerController.GetComponent<CharacterController>().enabled = true;
    }

    void Update()
    {
        timerUI.GetComponent<Text>().text = ((int)timer.leftTime).ToString();

        if (isFirstFrame) 
        { 
            currentPlayer.currentUnit.GetComponent<MeshRenderer>().enabled = false;
            playerController.transform.position = currentPlayer.currentUnit.transform.position;
            currentPlayer.currentUnit.GetComponent<Rigidbody>().isKinematic = true;
            currentPlayer.currentUnit.GetComponent<MeshCollider>().enabled = false;
            isFirstFrame = false; 
        }
        if(currentPlayer.currentUnit != null)
        {
            currentPlayer.currentUnit.transform.position = playerController.transform.position;
            currentPlayer.currentUnit.transform.rotation = playerController.transform.rotation;
        }

        if (currentPlayer.currentUnit.health <= 0) { EndTurn();}
        if (currentPlayer == null) { EndTurn();}

        playerCount = 0;
        foreach (player player in playerArr)
        {
            if(player != null)
            {
                playerCount++;
            }
        }

        if (playerCount == 1)
        {
            Win();
        }

        if (timer.leftTime < 0) { EndTurn(); }       
    }


    void EndTurn()
    {
        gun.GetComponent<gun>().projectilePrefab = null;
        if(playerCount == 1)
        {
            Win();
        }
        else
        {
            if (currentPlayer != null) { currentPlayer.switchUnit(); }
            
            for (int i = 0; i < playerArr.Count*2; i++)
            {
                currentPlayerIndex++;
                currentPlayerIndex %= playerArr.Count;
                currentPlayer = playerArr[currentPlayerIndex];
                if(currentPlayer != null)
                {
                    break;
                }
                if(i == playerArr.Count-1)
                {
                    Win();
                }
            }
            
            currentPlayer.currentUnit.GetComponent<MeshRenderer>().enabled = false;
            currentPlayer.currentUnit.GetComponent<Rigidbody>().isKinematic = true;
            currentPlayer.currentUnit.GetComponent<MeshCollider>().enabled = false;
            playerController.GetComponent<CharacterController>().enabled = false;
            playerController.transform.position = currentPlayer.currentUnit.transform.position;
            playerController.transform.rotation = currentPlayer.currentUnit.transform.rotation;
            playerController.GetComponent<CharacterController>().enabled = true;
            timer.leftTime = defaultTime;
        }
    }

    void Win()
    {

        Time.timeScale = 0;
        for (int i = 0; i < playerArr.Count; i++)
        {
            currentPlayerIndex++;
            currentPlayerIndex %= playerArr.Count;
            currentPlayer = playerArr[currentPlayerIndex];
            if (currentPlayer != null)
            {
                break;
            }
        }
        winScreenUI.gameObject.SetActive(true);
        winScreenUI.GetComponent<Text>().text = currentPlayer.name + "   WON!"; 

    }
}

