using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

	[SerializeField] GameObject Bullet; // declare bullet as variable
	[SerializeField] AudioClip shootSfx;
	AudioSource audioSource;
	[SerializeField] float timeSinceShot = 0f;

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
		audioSource = gameObject.GetComponent<AudioSource>();
	}


	// Update is called once per frame
	void Update()
	{
		timeSinceShot += Time.deltaTime;
		if (!GetComponent<CloneState>().isMaster)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0) && timeSinceShot > .2f)
		{
			GameObject bullet = ClonesManager.singleton.Spawn(Bullet, transform.position + transform.right, transform.rotation);
			audioSource.pitch = Random.Range(.7f, 1f);
			audioSource.PlayOneShot(shootSfx, Random.Range(.7f, 1f));
			timeSinceShot = 0;
		}
	}

}
