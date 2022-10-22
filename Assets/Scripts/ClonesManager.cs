using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ClonesManager : MonoBehaviour
{
	public static ClonesManager singleton;
	private Hashtable stateTable = new Hashtable();
	private Hashtable clonesTable = new Hashtable();
	private Hashtable listenerTable = new Hashtable();
	private Vector3[] offsets;

	void Awake()
	{
		singleton = this;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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

	public GameObject Spawn(GameObject gameObject, Vector3 position, Quaternion rotation)
	{
		GameObject master = Instantiate(gameObject, position, rotation);
		master.SetActive(false);
		master.GetComponent<CloneState>().isMaster = true;
		master.GetComponent<CloneState>().master = master;
		clonesTable.Add(master, new GameObject[4]);

		CreateClones(master);

		master.SetActive(true);

		return master;
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
		clone.SetActive(false);
		clone.GetComponent<CloneState>().isMaster = false;
		clone.GetComponent<CloneState>().master = master;
		Transform cloneTransform = clone.GetComponent<Transform>();
		cloneTransform.position = cloneTransform.position + offset;
		cloneTransform.localScale = master.transform.localScale;

		clone.SetActive(true);

		return clone;
	}

	public void setState<T>(GameObject go, string key, T value)
	{
		Hashtable state;

		if (stateTable.Contains(go))
		{
			state = stateTable[go] as Hashtable;
		}
		else
		{
			stateTable.Add(go, new Hashtable());
			state = stateTable[go] as Hashtable;
		}
		if (state.ContainsKey(key))
		{
			state[key] = value;
		}
		else
		{
			state.Add(key, value);
		}
	}

	public T getState<T>(GameObject go, string key)
	{
		return (T)(stateTable[go] as Hashtable)[key];
	}

	public void destroyClones(GameObject gObj)
	{
		for (int i = 0; i < 4; i++)
		{
			Destroy((clonesTable[gObj] as GameObject[])[i]);
		}
	}

	public void enableClones(GameObject go)
	{
		GameObject[] clones = clonesTable[go] as GameObject[];
		foreach (GameObject clone in clones)
		{
			clone.SetActive(true);
		}
	}
}
