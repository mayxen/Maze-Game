using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;
    public Player playerPrefab;
    public Camera camaraPlayer;
    public Camera camaraGeneral;
    public CameraMove camara;
    Maze mazeInstance;
    Player playerInstance;
    
    private void Start()
    {
        camaraGeneral.enabled = true;
        camaraPlayer.enabled = false;
        StartCoroutine(BeginGame());
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && mazeInstance != null)
        {
            RestartGame();
        }  
    }

    private void RestartGame()
    {
        
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        camaraPlayer.transform.parent = playerInstance.transform.parent;
        
        if (playerInstance != null)
            Destroy(playerInstance.gameObject);
        StartCoroutine(BeginGame());
    }

    private IEnumerator BeginGame()
    {
        camaraGeneral.enabled = true;
        camaraPlayer.enabled = false;
        mazeInstance = Instantiate(mazePrefab);
        yield return StartCoroutine(mazeInstance.Generate());
        camaraGeneral.enabled = false;
        camaraPlayer.enabled = true;

        playerInstance = Instantiate(playerPrefab);
        camara.SetPlayer(playerInstance);
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        camaraPlayer.transform.position = new Vector3(playerInstance.transform.GetChild(0).transform.position.x,0.5f, playerInstance.transform.GetChild(0).transform.position.z);



    }
}
