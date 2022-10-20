using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float lastSpawn = 0f;
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lastSpawn += Time.deltaTime;
        float height = Camera.main.orthographicSize;
        float width = Camera.main.orthographicSize * Camera.main.aspect;
        Vector3[] positions = {
            new Vector3(Random.Range(-width, width), height, 0),
            new Vector3(Random.Range(-width, width), -height, 0),
            new Vector3(width, Random.Range(-height, height), 0),
            new Vector3(-width, Random.Range(-height, height), 0)
        };
        Vector3 position = positions[Random.Range(0, 4)];
        if (lastSpawn > 3) {
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.GetComponent<Enemy>().direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 4;
            lastSpawn = 0;
        }
    }
}
