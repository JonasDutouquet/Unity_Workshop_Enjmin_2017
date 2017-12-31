using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransitionCurve
{
	//[Header("Transition")]
	public bool transition = false;
	public AnimationCurve transitionCurve = AnimationCurve.Linear (0f, 0f, 1f, 1f);

	private float _attackTime = 0f;
		
	public float GetTransitionSpeed()
	{
		float transitionSpeed = transitionCurve.Evaluate (_attackTime);
		_attackTime += Time.deltaTime;
		return transitionSpeed;
	}

	public void ResetAttackTime() 
	{
		_attackTime = 0f;
	}
}
