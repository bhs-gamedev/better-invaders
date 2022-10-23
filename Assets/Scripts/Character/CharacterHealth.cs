using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
	[SerializeField]
	public int maxHealth = 3;
	[SerializeField]
	ParticleSystem deathParticles;
	[SerializeField] ParticleSystem damageParticles;

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
			Instantiate(deathParticles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	public void Damage(int amount)
	{
		int newHealth = ClonesManager.singleton.getState<int>(GetComponent<CloneState>().master, "playerHealth") - amount;
		ClonesManager.singleton.setState(GetComponent<CloneState>().master, "playerHealth", newHealth);

		Instantiate(damageParticles, transform.position, Quaternion.identity);
		GameManager.singleton.UpdateHealth(newHealth);
	}
}
