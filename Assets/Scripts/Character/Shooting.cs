using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject Bullet; // declare bullet as variable

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Bullet); // spawn in bullet
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
         {
             Debug.Log("Left mouse clicked");
            Instantiate(Bullet); // spawn in bullet
            Instantiate(Bullet, transform.position, transform.rotation);
        }
    }

}
