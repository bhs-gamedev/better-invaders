using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	[SerializeField] private float speed = 5;
	private Rigidbody2D RB;

	void Awake()
	{
		RB = GetComponent<Rigidbody2D>();

	}

	void Start()
	{
		RB.velocity = speed * transform.right;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy" && GetComponent<CloneState>().isMaster && col.gameObject.GetComponent<CloneState>().isMaster)
		{
			GameManager.singleton.IncrementKills();
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}

	void OnDestroy()
	{
		if (GetComponent<CloneState>().isMaster)
		{
			ClonesManager.singleton.destroyClones(gameObject);
		}
	}
}
