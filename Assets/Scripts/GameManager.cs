using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    float lastSpawn = 0f;
    bool isPlaying = true;
    public GameObject enemyPrefab;
    public GameObject gameOverMenu;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawn += Time.deltaTime;
        if (isPlaying && lastSpawn > 3) {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float height = Camera.main.orthographicSize;
        float width = Camera.main.orthographicSize * Camera.main.aspect;
        Vector3[] positions = {
            new Vector3(Random.Range(-width, width), height, 0),
            new Vector3(Random.Range(-width, width), -height, 0),
            new Vector3(width, Random.Range(-height, height), 0),
            new Vector3(-width, Random.Range(-height, height), 0)
        };
        Vector3 position = positions[Random.Range(0, 4)];
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.GetComponent<Enemy>().direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 4;
        lastSpawn = 0;
    }

    public void GameOver()
    {
        isPlaying = false;
        gameOverMenu.SetActive(true);
        player.GetComponent<CharacterMovement>().enabled = false;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
