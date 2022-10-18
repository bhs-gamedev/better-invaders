using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenwrap : MonoBehaviour
{
    private GameObject[] clones = new GameObject[4];
    private Vector3[] offsets;

    // Start is called before the first frame update
    void Start()
    {
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
        offsets = new Vector3[] {
            new Vector3(0, cameraHeight),
            new Vector3(0, -cameraHeight),
            new Vector3(cameraWidth, 0),
            new Vector3(-cameraWidth, 0),
        };
        for (int i = 0; i < 4; i++)
        {
            clones[i] = makeClone(offsets[i]);
        }
    }

    GameObject makeClone(Vector3 offset)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        GameObject clone = new GameObject(gameObject.name + " Clone");
        Transform cloneTransform = clone.GetComponent<Transform>();
        cloneTransform.position = cloneTransform.position + offset;
        cloneTransform.localScale = transform.localScale;
        SpriteRenderer cloneSprite = clone.AddComponent<SpriteRenderer>();
        cloneSprite.sprite = sprite.sprite;
        cloneSprite.color = sprite.color;
        return clone;
    }

    void Update()
    {
        float cameraHeight = Camera.main.orthographicSize * 2f ;
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;

        if (transform.position.x < -cameraWidth / 2)
        {
            transform.position = transform.position + new Vector3(cameraWidth, 0, 0);
        }
        else if (transform.position.x > cameraWidth / 2)
        {
            transform.position = transform.position + new Vector3(-cameraWidth, 0, 0);
        }
        else if (transform.position.y < -cameraHeight / 2)
        {
            transform.position = transform.position + new Vector3(0, cameraHeight, 0);
        }
        else if (transform.position.y > cameraHeight / 2)
        {
            transform.position = transform.position + new Vector3(0, -cameraHeight, 0);
        }

        for (int i = 0; i < 4; i++)
        {
            clones[i].transform.position = gameObject.transform.position + offsets[i];
            clones[i].transform.rotation = gameObject.transform.rotation;
        }
    }
}