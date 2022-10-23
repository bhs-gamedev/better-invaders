using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;


class ScoreComparer : IComparer<int[]>
{
    public int Compare(int[] x, int[] y)
    {
        if (x[1] < y[1])
        {
            return 1;
        }
        else if (x[1] == y[1])
        {
            return 0;
        }
        return -1;

    }
}

public class MenuManager : MonoBehaviour
{
    public GameObject leaderboard;
    public GameObject mainMenu;

    public GameObject scores;
    public GameObject leaderboardScore;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject scoreText = Instantiate(leaderboardScore, scores.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowLeaderboard()
    {
        mainMenu.SetActive(false);
        leaderboard.SetActive(true);

        if (PlayerPrefs.HasKey("scoreCount"))
        {
            int scoreCount = PlayerPrefs.GetInt("scoreCount");
            List<int[]> highscores = new List<int[]>();
            for (int i = 0; i < scoreCount; i++)
            {
                highscores.Add(new int[] {i, PlayerPrefs.GetInt("score" + i)});
            }
            highscores.Sort(new ScoreComparer());

            for (int i = 0; i < Math.Min(10, scoreCount); i++)
            {
                scores.transform.GetChild(i).GetComponent<TMP_Text>().text = PlayerPrefs.GetString("score" + highscores[i][0] + "name") + ": " + highscores[i][1].ToString();
            }
        }
    }

    public void ShowMenu()
    {
        leaderboard.SetActive(false);
        mainMenu.SetActive(true);
    }
}
