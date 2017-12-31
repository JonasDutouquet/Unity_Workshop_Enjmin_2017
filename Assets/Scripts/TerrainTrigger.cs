using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTrigger : MonoBehaviour 
{
	[SerializeField] private Terrain _terrain;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			other.GetComponent<MovementControl> ().ChangeTerrain (_terrain.index);
		}
	}
}
