using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	[SerializeField] private float speed = 5;
	[SerializeField] GameObject particles;
	private Rigidbody2D RB;
	private float lifetime = 3.0f;

	void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		RB.velocity = speed * transform.right;

		if (GetComponent<CloneState>().isMaster)
		{
			StartCoroutine(LifetimeClock());
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (GetComponent<CloneState>().isMaster && col.gameObject.GetComponent<CloneState>().isMaster)
		{
			if (col.tag == "Enemy")
			{
				// col.gameObject.GetComponent<Enemy>().Die();
				// ClonesManager.singleton.destroyClones(gameObject);
				// Destroy(gameObject);
			}
			else if (col.tag == "Player")
			{
				col.gameObject.GetComponent<CharacterHealth>().Damage(1);
				ClonesManager.singleton.destroyClones(gameObject);
				Destroy(gameObject);
			}
			else if (col.tag == "PlayerBullet")
			{
				ClonesManager.singleton.destroyClones(gameObject);
				Destroy(gameObject);
				Instantiate(particles, transform.position, Quaternion.identity);

			}
		}
	}

	IEnumerator LifetimeClock()
	{
		yield return new WaitForSeconds(lifetime);
		ClonesManager.singleton.destroyClones(gameObject);
		Destroy(gameObject);
	}
}
