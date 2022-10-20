using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public Vector2 direction;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.position = rb.position + (direction * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterHealth>().Damage(1);
        }
    }
}
