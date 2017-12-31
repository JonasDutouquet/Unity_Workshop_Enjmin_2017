using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TerrainTrigger : MonoBehaviour 
{
	[SerializeField] private Terrain _terrain;

	void Start()
	{
		GetComponent<BoxCollider> ().isTrigger = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if (other.GetComponent<MovementControl> ())
				other.GetComponent<MovementControl> ().ChangeTerrain (_terrain.index);
			else
				Debug.LogWarning ("The collider currently on " + name + " does not have a Movement Control script. \n Please consider attaching one and setting it up.");
		}
	}
}
