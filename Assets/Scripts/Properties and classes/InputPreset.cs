using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputPresetsEnum {ChoosePreset, AvatarMovement, CameraRotation, AvatarJump, AvatarShoot}

[System.Serializable]
public class InputPreset
{
	public bool usePreset;
	public InputPresetsEnum presets;
	public MovementControl avatarMovement;
	public Object controlScript;
}
