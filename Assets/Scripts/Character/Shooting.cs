using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

	public GameObject Bullet; // declare bullet as variable

	// Start is called before the first frame update
	void Awake()
	{
		if (!GetComponent<CloneState>().isMaster)
		{
			Destroy(this);
		}
	}
	void Start()
	{
		// Instantiate(Bullet); // spawn in bullet
	}

	// Update is called once per frame
	void Update()
	{
		if (!GetComponent<CloneState>().isMaster)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			// Debug.Log("Left mouse clicked");
			ClonesManager.singleton.Spawn(Bullet, transform.position + transform.right, transform.rotation);
		}
	}

}
