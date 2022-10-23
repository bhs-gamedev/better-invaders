using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
	[SerializeField]
	int maxHealth = 3;

	// Start is called before the first frame update
	void Awake()
	{
		// Debug.Log(GetComponent<CloneState>().isMaster);
	}
	void Start()
	{
		if (GetComponent<CloneState>().isMaster)
		{
			ClonesManager.singleton.setState(gameObject, "playerHealth", maxHealth);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (GetComponent<CloneState>().isMaster && ClonesManager.singleton.getState<int>(gameObject, "playerHealth") <= 0) // bad
		{
			GameManager.singleton.GameOver();
			ClonesManager.singleton.destroyClones(gameObject);
			Destroy(gameObject);
		}
	}

	public void Damage(int amount)
	{
		int newHealth = ClonesManager.singleton.getState<int>(GetComponent<CloneState>().master, "playerHealth") - amount;
		ClonesManager.singleton.setState(GetComponent<CloneState>().master, "playerHealth", newHealth);
	}
}
