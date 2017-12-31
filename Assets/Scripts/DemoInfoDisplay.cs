using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoInfoDisplay : MonoBehaviour 
{
	[SerializeField] private Text _inputStatus;
	[SerializeField] private Text _speed;
	[SerializeField] private MovementControl movement;
	private float _currentSpeed;

	void Update()
	{
		_currentSpeed = movement._currentSpeed;
		_speed.text = _currentSpeed.ToString ();

		Vector3 _movement = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
		if (_movement == Vector3.zero)
			_inputStatus.text = "No input";
		else _inputStatus.text = "...";
	}
}
