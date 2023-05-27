using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerSpawn;
    public Transform enemySpawn;
    private GameController gameController;
    public int price = 100;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (gameController.Gold >= price)
            {
                gameController.Gold -= price;
                Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity, playerSpawn);
            }
        }
    }

    IEnumerator SpawnEnemies() {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1,11));
            Instantiate(enemyPrefab, enemySpawn.position, Quaternion.Euler(0,180,0), enemySpawn);
        }
    }
}
