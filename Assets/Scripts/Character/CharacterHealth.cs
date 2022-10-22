using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
	[SerializeField]
	int maxHealth = 3;

	// Start is called before the first frame update
	void Start()
	{
		if (GetComponent<CloneState>().getMaster())
		{
			ClonesManager.singleton.setState("playerHealth", maxHealth);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (ClonesManager.singleton.getState<int>("playerHealth") <= 0 && GetComponent<CloneState>().getMaster()) // bad
		{
			GameManager.singleton.GameOver();
			ClonesManager.singleton.destroyClones(gameObject);
			Destroy(gameObject);
		}
	}

	public void Damage(int amount)
	{
		int newHealth = ClonesManager.singleton.getState<int>("playerHealth") - amount;
		ClonesManager.singleton.setState("playerHealth", newHealth);


	}
}
