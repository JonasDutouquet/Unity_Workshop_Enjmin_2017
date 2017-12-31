using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResponseCurve
{
	public string name = "New Curve";
	public AnimationCurve curve;
	[Tooltip("Which key is the end of the attack phase?")]public int key = 2;
	public float speedFactor = 1f;
	public TransitionCurve transitionCurve;

	private AnimationCurve _attackCurve;	//the attack curve extracted from the whole curve.
	private AnimationCurve _inverseAttack;	//the reverse curve, used to get the release time from the speed.
	private AnimationCurve _releaseCurve;	//the release curve extracted from the whole curve.
	private AnimationCurve _inverseRelease;	//the reverse curve, used to get the release time from the speed.

	private float attackTime = 0f;	//where to evaluate the attack curve.
	private float releaseTime = 0f;	//where to evaluate the release curve.

	private bool isTransition = false;

	private string _x = "";
	private string _y = "";
	private string _z = "";

	public bool isInvalid()
	{
		if (curve.length > 0 && key < curve.length && key >0) 
		{
			return (curve.keys [0].value != 0 || curve.keys [curve.length - 1].value != 0 || curve.keys[key].value != 1 ||curve.keys[key-1].value != 1) ? true : false;
		} else
			return false;
	}

	public void AdjustCurve()
	{
		if(curve.keys[0].value != 0)
		{
			//adjust first key
			Keyframe firstKf = curve.keys [0];
			Keyframe newFirstKf = new Keyframe (0f, 0f, firstKf.inTangent, firstKf.outTangent);
			curve.MoveKey (0, newFirstKf);
		}

		//adjust last key
		if(curve.keys[curve.length-1].value != 0)
		{
			Keyframe lastKf = curve.keys [curve.length - 1];
			Keyframe newLastKf = new Keyframe (lastKf.time, 0f, lastKf.inTangent, lastKf.outTangent);
			curve.MoveKey (curve.length-1, newLastKf);

		}

		//adjust first release key
		if(curve.keys[key].value != 1)
		{
			Keyframe maxKey = curve.keys [key];
			Keyframe newMaxKey = new Keyframe (maxKey.time, 1f, maxKey.inTangent, maxKey.outTangent);
			curve.MoveKey (key, newMaxKey);
		}

		//adjust max key
		if(curve.keys[key-1].value != 1)
		{
			Keyframe maxKey = curve.keys [key-1];
			Keyframe newMaxKey = new Keyframe (maxKey.time, 1f, maxKey.inTangent, maxKey.outTangent);
			curve.MoveKey (key-1, newMaxKey);
		}
	}

	//called by the MovementControl script at start().
	public void InitializeCurve(string x, string y, string z, bool xtrue, bool ytrue, bool ztrue)
	{
		InitializeReverseCurves ();
		if(xtrue) _x = x;
		if (ytrue) _y = y;
		if (ztrue) _z = z;
	}

	void InitializeReverseCurves()
	{
		//Get Attack Curve
		_attackCurve = new AnimationCurve();
		for (int i = 0 ; i <key ; i++)
			_attackCurve.AddKey (curve.keys[i]);

		//Create Reverse Attack Curve
		_inverseAttack = new AnimationCurve ();
		for (int i = 0; i < _attackCurve.length ; i++)
		{
			Keyframe inverseKey = new Keyframe (_attackCurve.keys [i].value, _attackCurve.keys [i].time);
			_inverseAttack.AddKey (inverseKey);
		}

		//Get Release Curve
		_releaseCurve = new AnimationCurve ();
		for (int i =key ; i < curve.length ; i++)
			_releaseCurve.AddKey (curve.keys [i].time - curve.keys [key].time, curve.keys [i].value);
	
		//Create Reverse Release Curve
		_inverseRelease = new AnimationCurve ();
		for (int i = 0; i < _releaseCurve.length ; i++)
		{
			Keyframe inverseKey = new Keyframe (_releaseCurve.keys [i].value, _releaseCurve.keys [i].time);
			_inverseRelease.AddKey (inverseKey);
		}
	}

	public void InitializeTimes(float speed)
	{
		attackTime = _inverseAttack.Evaluate(speed);
		releaseTime = _inverseRelease.Evaluate(speed);
		if (transitionCurve.transition)
			isTransition = true;
	}

	public Movement GetMovement(Movement currentMovement)
	{
		Vector3 movement = new Vector3 (GetInput (_x), GetInput (_y), GetInput (_z)); 

		//if input to move -> attack
		if(movement != Vector3.zero)
		{
			float speed = _attackCurve.Evaluate(attackTime);
			if (speed != 0)
				releaseTime = _inverseRelease.Evaluate(speed);
			speed *= speedFactor;
		
			if(isTransition)
			{
				float tranSpeed = transitionCurve.GetTransitionSpeed ();
				if (tranSpeed < 1) 
				{
					speed = currentMovement.speed + (speed - currentMovement.speed) * tranSpeed;
				} else {
					isTransition = false;
					transitionCurve.ResetAttackTime ();
				}
			}

			currentMovement.direction = movement;
			currentMovement.speed = speed;
			attackTime += Time.deltaTime;
		}
		else
		{
			float speed = _releaseCurve.Evaluate(releaseTime);

			if (speed != 0)
				attackTime = _inverseAttack.Evaluate(speed);

			speed *= speedFactor;

			if(isTransition) 
			{
				float tranSpeed = transitionCurve.GetTransitionSpeed ();
				if (tranSpeed < 1)
					speed = currentMovement.speed + (speed - currentMovement.speed) * tranSpeed;
				else
				{
					isTransition = false;
					transitionCurve.ResetAttackTime ();
				}
			}

			currentMovement.speed = speed;
			releaseTime += Time.deltaTime;
		}
		return currentMovement;
	}

	float GetInput(string s)
	{
		if (s != "")
			return Input.GetAxis (s);
		else
			return 0f;
	}
}

public struct Movement
{
	public Vector3 direction;	//input
	public float speed;			//speed value on the curve * speed factor
	public Vector3 GetMovement()
	{
		return (direction * speed * Time.deltaTime);
	}
}

[System.Serializable]
public class Terrain
{
	public string name;
	public int index;

	public Terrain(string n, int i)
	{
		name = n;
		index = i;
	}
}