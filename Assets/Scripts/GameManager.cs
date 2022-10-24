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
	private float timeLasted = 0;
	void Awake()
	{
		singleton = this;
	}

	float lastSpawn = 0f;
	bool isPlaying = true;

	float spawnCooldown = 3.0f;

	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private GameObject gameOverMenu;
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private GameObject nameInput;
	[SerializeField] private GameObject hud;
	[SerializeField] private GameObject healthHUD;
	[SerializeField] private GameObject healthIcon;
	[SerializeField] private TMP_Text hudScoreText;

	// Start is called before the first frame update
	void Start()
	{
		GameObject player = ClonesManager.singleton.Spawn(playerPrefab, Vector3.zero, Quaternion.identity);
		StartCoroutine(gameClock());
		Debug.Log(player.GetComponent<CharacterHealth>().maxHealth);
		for (int i = 0; i < player.GetComponent<CharacterHealth>().maxHealth - 1; i++)
		{
			Instantiate(healthIcon, healthHUD.transform);
		}
	}

	// Update is called once per frame
	void Update()
	{
		lastSpawn += Time.deltaTime;
		if (isPlaying)
		{
			if (lastSpawn > spawnCooldown)
			{
				SpawnEnemy();
			}
			hudScoreText.GetComponent<TMP_Text>().text = "Score: " + CalculateScore();
			timeLasted += Time.deltaTime;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OpenMenu();
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
		Vector2 velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 4;
		float rotationEuler = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
		GameObject enemy = ClonesManager.singleton.Spawn(enemyPrefab, position, Quaternion.Euler(0, 0, rotationEuler));
		enemy.GetComponent<Enemy>().setVelocity(velocity);
		lastSpawn = 0;
	}

	public void UpdateHealth(int health)
	{
		foreach (Transform child in healthHUD.transform)
		{
			child.gameObject.SetActive(false);
		}
		for (int i = 0; i < health - 1; i++)
		{
			healthHUD.transform.GetChild(i).gameObject.SetActive(true);
		}
	}

	public void GameOver()
	{
		isPlaying = false;
		scoreText.text = "Score: " + CalculateScore();
		hud.SetActive(false);
		gameOverMenu.SetActive(true);
		// Debug.Log(score);
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	int CalculateScore()
	{
		return (int)(timeLasted * Mathf.Pow(1 + killCount, 1.5f));
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
			if (spawnCooldown > 0.5f) spawnCooldown -= 0.05f;
		}
	}

	public void OpenMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void SaveScore()
	{
		int index;
		if (!PlayerPrefs.HasKey("scoreCount"))
		{
			index = 0;
		}
		else
		{
			index = PlayerPrefs.GetInt("scoreCount");
		}
		string name = nameInput.GetComponent<TMP_Text>().text;
		if (name.Contains("your name here"))
		{
			name = "no name";
		}
		PlayerPrefs.SetInt("score" + index, CalculateScore());
		PlayerPrefs.SetString("score" + index + "name", name);
		PlayerPrefs.SetInt("scoreCount", index + 1);
	}
}
