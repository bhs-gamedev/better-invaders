using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ClonesManager : MonoBehaviour
{
	public static ClonesManager singleton;
	private Hashtable stateTable = new Hashtable();
	private Hashtable clonesTable = new Hashtable();
	private Vector3[] offsets;

	void Awake()
	{
		singleton = this;
	}

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
	}

	public void Spawn(GameObject gameObject)
	{
		GameObject master = Instantiate(gameObject);
		master.GetComponent<CloneState>().setMaster(true);
		clonesTable.Add(master, new GameObject[4]);

		CreateClones(master);
	}

	void Update()
	{
		float cameraHeight = Camera.main.orthographicSize * 2f;
		float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;

		List<GameObject> deletedObjects = new List<GameObject>();

		foreach (DictionaryEntry item in clonesTable)
		{
			try
			{
				Transform transform = ((GameObject)(item.Key)).transform; // lol
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
					((GameObject[])(item.Value))[i].transform.position = transform.position + offsets[i];
					((GameObject[])(item.Value))[i].transform.rotation = transform.rotation;
				}
			}
			catch (MissingReferenceException)
			{
				deletedObjects.Add((GameObject)(item.Key));
			}
		}

		foreach (GameObject go in deletedObjects)
		{
			clonesTable.Remove(go);
		}
	}

	void CreateClones(GameObject master)
	{
		GameObject[] clonesArray = clonesTable[master] as GameObject[];
		for (int i = 0; i < 4; i++)
		{
			clonesArray[i] = makeClone(master, offsets[i]);
		}
	}

	GameObject makeClone(GameObject master, Vector3 offset)
	{
		GameObject clone = Instantiate(master);
		clone.GetComponent<CloneState>().setMaster(false);
		Transform cloneTransform = clone.GetComponent<Transform>();
		cloneTransform.position = cloneTransform.position + offset;
		cloneTransform.localScale = master.transform.localScale;

		return clone;
	}
}
