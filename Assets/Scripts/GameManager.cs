using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager singleton;
	[SerializeField] TMP_Text scoreText;
	private int killCount = 0;
	private int timeLasted = 0;
	void Awake()
	{
		singleton = this;
	}

	float lastSpawn = 0f;
	bool isPlaying = true;

	float score = 0f;
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private GameObject gameOverMenu;
	[SerializeField] private GameObject playerPrefab;

	// Start is called before the first frame update
	void Start()
	{
		ClonesManager.singleton.Spawn(playerPrefab, Vector3.zero, Quaternion.identity);
		StartCoroutine(gameClock());
	}

	// Update is called once per frame
	void Update()
	{
		lastSpawn += Time.deltaTime;
		if (isPlaying && lastSpawn > 3)
		{
			SpawnEnemy();
		}
		// Debug.Log(timeLasted);
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
		GameObject enemy = ClonesManager.singleton.Spawn(enemyPrefab, position, Quaternion.identity);
		enemy.GetComponent<Enemy>().setVelocity(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 4);
		lastSpawn = 0;
	}

	public void GameOver()
	{
		isPlaying = false;
		scoreText.text = "Score: " + CalculateScore();
		gameOverMenu.SetActive(true);
		// Debug.Log(score);
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	float CalculateScore()
	{
		return score + killCount;
	}

	public void IncrementKills()
	{
		killCount += 1;
	}

	IEnumerator gameClock()
	{
		while (isPlaying)
		{
			yield return new WaitForSeconds(1);
			timeLasted += 1;
		}
	}
}
