using System.Collections.Generic;
using UnityEngine;

//Class format to be saved to file (put any information you want to save in here)
[System.Serializable]
public class SaveFile
{
	public List<SerialVector3> cratePositions = new List<SerialVector3>();
	public SerialVector3 playerPosition;
	public float yawRotation;
}

//NOTE: We do not save Vector3 itself because it's not allowed to be saved and additionally it contains lot of other info that we don't need to save
//That's why we've created a simple struct to save Vector3 info
//A struct that will allow us to save a Vector3 to file
[System.Serializable]
public struct SerialVector3
{
	public float x;
	public float y;
	public float z;

	public SerialVector3(Vector3 v)
	{
		x = v.x;
		y = v.y;
		z = v.z;
	}

	//These two methods allow us to use the equal sign between a Vector3
	//and a SerialVector3
	public static implicit operator Vector3(SerialVector3 rValue)
	{
		return new Vector3(rValue.x, rValue.y, rValue.z);
	}

	public static implicit operator SerialVector3(Vector3 rValue)
	{
		return new SerialVector3(rValue);
	}
}
