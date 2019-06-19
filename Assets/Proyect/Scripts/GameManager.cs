using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;
    public Player playerPrefab;
    Maze mazeInstance;
    Player playerInstance;
    private void Start()
    {
        StartCoroutine(BeginGame());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }  
    }

    private void RestartGame()
    {
        Debug.Log("Restart");
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
            Destroy(playerInstance);
        BeginGame();
    }

    private IEnumerator BeginGame()
    {
        mazeInstance = Instantiate(mazePrefab);
        yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab);
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
    }
}
