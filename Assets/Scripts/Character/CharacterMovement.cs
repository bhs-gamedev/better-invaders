using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	[SerializeField] private float speed = 5.0f;

	private Rigidbody2D _RB;
	private Vector2 movementVector;
	private float rotationEuler;
	[SerializeField] private GameObject thrusters;

	void Awake()
	{
		_RB = GetComponent<Rigidbody2D>();

		if (!GetComponent<CloneState>().isMaster)
		{
			Destroy(this);
		}
	}

	void FixedUpdate()
	{
		_RB.velocity = movementVector * speed;
		_RB.rotation = rotationEuler;
	}

	void Update()
	{
		movementVector = GetMovement();
		rotationEuler = GetMouseAngle();

		if (movementVector.x == 0 && movementVector.y == 0)
		{
			thrusters.SetActive(false);
		}
		else
		{
			thrusters.SetActive(true);
		}
	}

	Vector2 GetMovement()
	{
		return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	float GetMouseAngle()
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 characterPosition = transform.position;
		Vector2 characterToMouse = mousePosition - characterPosition;

		float rotation = Mathf.Atan2(characterToMouse.y, characterToMouse.x);

		return rotation * Mathf.Rad2Deg;
	}
}
