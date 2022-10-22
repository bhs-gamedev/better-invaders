using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
	[SerializeField]
	int health = 3;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Damage(int amount)
	{
		health -= amount;
		if (health <= 0)
		{
			GameManager.singleton.GameOver();
			Destroy(gameObject);
		}
	}
}
