using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	[SerializeField] private float speed = 5;
	[SerializeField] private float lifetime = 5.0f;
	private Rigidbody2D RB;

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
				GameManager.singleton.IncrementKills();
				col.gameObject.GetComponent<Enemy>().Die();
				ClonesManager.singleton.destroyClones(gameObject);
				Destroy(gameObject);
			}
			else if (col.tag == "Player")
			{
				col.gameObject.GetComponent<CharacterHealth>().Damage(1);
				ClonesManager.singleton.destroyClones(gameObject);
				Destroy(gameObject);
			}
			else if (col.tag == "EnemyBullet")
			{
				ClonesManager.singleton.destroyClones(gameObject);
				Destroy(gameObject);
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

