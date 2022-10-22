using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	Rigidbody2D rb;

	// Start is called before the first frame update
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (other.gameObject.GetComponent<CloneState>().isMaster && gameObject.GetComponent<CloneState>().isMaster)
			{
				other.gameObject.GetComponent<CharacterHealth>().Damage(1);
			}
		}
	}

	public void setVelocity(Vector2 velocity)
	{
		rb.velocity = velocity;
		// ClonesManager.singleton.enableClones(gameObject);
	}

	void OnDestroy()
	{
		if (GetComponent<CloneState>().isMaster)
		{
			ClonesManager.singleton.destroyClones(gameObject);
		}
	}
}
