using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneState : MonoBehaviour
{
	private bool isMaster = false;

	public void setMaster(bool state)
	{
		isMaster = state;
	}

	public bool getMaster()
	{
		return isMaster;
	}
}
