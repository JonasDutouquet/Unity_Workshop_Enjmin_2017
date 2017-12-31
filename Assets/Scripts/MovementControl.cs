using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (ResponseManager))]
public class MovementControl : MonoBehaviour
{
	public string associatedInput;
	[SerializeField] private string xInput = "Horizontal";
	[SerializeField] private string yInput = "";
	[SerializeField] private string zInput = "Vertical";
	[SerializeField] private bool _useRb = false;
	[SerializeField] private bool _allowRotation = false;
	[SerializeField] private float _rotateSpeed = 5f;
	[SerializeField] private float _currentSpeed = 0f;

	private List<Terrain> terrains = new List<Terrain> ();
	private ResponseCurve _currentCurve;
	private List<ResponseCurve> curves = new List<ResponseCurve>();
	private Rigidbody _rb;
	private ResponseControl.Input _input;
	private Movement _currentMovement;

	void Start()
	{
		_rb = GetComponent<Rigidbody> ();

		_input = AssignInput ();

		for (int i = 0 ; i< _input.curves.Count ; i++)
		{
			curves.Add (_input.curves[i]);
			curves [i].InitializeCurve (xInput, yInput, zInput);
			terrains.Add (new Terrain(curves[i].name, i));
		}
		_currentCurve = curves [0];
	}

	ResponseControl.Input AssignInput()
	{
		List<ResponseControl.Input> _inputs = GetComponent<ResponseManager> ().inputs;
		if (associatedInput != null)
		{
			for (int i = 0; i < _inputs.Count; i++)
				if (_inputs [i].name == associatedInput)
					return _inputs [i];
			Debug.LogError ("No input defined as '" + associatedInput + "'. \n Please check the names.");
			Debug.Break ();
			return null;
		} else
			return null;
	}

	ResponseCurve AssignTerrain(string newTerrain)
	{
		for(int i = 0 ; i < curves.Count ; i++)
		{
			string id = curves[i].name;
			if (id == newTerrain) 
			{
				Debug.Log ("This curve was assigned : " + curves [i].name);
				return curves [i];
			}
		}
		Debug.LogError ("No curve defined as '" + newTerrain + "'. \n Please check the names.");
		Debug.Break ();
		return null;
	}


	public void ChangeTerrain(int terrainIndex)
	{
		if(_input.isStateDependent) 
		{
			_currentCurve = curves [terrainIndex];
			_currentCurve.InitializeTimes (_currentSpeed);
		}
	}

	void FixedUpdate()
	{		
		_currentMovement = _currentCurve.GetMovement(_currentMovement);
		_currentSpeed = _currentMovement.speed;

		Vector3 direction = _currentMovement.GetMovement ();

		if (_useRb)
		{
			if (_allowRotation && direction != Vector3.zero)
				_rb.MoveRotation (Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * _rotateSpeed));
			_rb.MovePosition (transform.position + direction);
		}
		else 
		{
			if (_allowRotation && direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * _rotateSpeed);
			transform.Translate (direction, Space.World);
		}
	}
}
