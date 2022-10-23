using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	Rigidbody2D rb;
	[SerializeField] ParticleSystem deathParticles;
	[SerializeField] private GameObject bullet;


	// Start is called before the first frame update
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		if (GetComponent<CloneState>().isMaster) StartCoroutine(ShootRoutine());
	}

	// Update is called once per frame
	IEnumerator ShootRoutine()
	{
		bool playerAlive = true;
		while (playerAlive)
		{
			yield return new WaitForSeconds(4);
			GameObject player = GameObject.Find("Player(Clone)");
			if (player != null)
			{
				Vector2 diff = player.transform.position - transform.position;
				GameObject bulletObj = ClonesManager.singleton.Spawn(bullet, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg));
			}
			else
			{
				playerAlive = false;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (other.gameObject.GetComponent<CloneState>().isMaster && gameObject.GetComponent<CloneState>().isMaster)
			{
				other.gameObject.GetComponent<CharacterHealth>().Damage(1);
				ClonesManager.singleton.destroyClones(gameObject);
				Destroy(gameObject);
			}
		}
	}

	public void setVelocity(Vector2 velocity)
	{
		rb.velocity = velocity;
		// ClonesManager.singleton.enableClones(gameObject);
	}

	public void Die()
	{
		if (!GetComponent<CloneState>().isMaster) return;

		Instantiate(deathParticles, transform.position, Quaternion.identity);

		ClonesManager.singleton.destroyClones(gameObject);
		Destroy(gameObject);
	}
}
