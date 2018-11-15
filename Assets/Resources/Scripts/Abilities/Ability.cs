using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability {

	/*Template for abilities classes*/

	public string aName = "New Ability";
	public Sprite aSprite;
	public AudioClip aSound;
	public cooldownTimer coolDown;
	public ParticleSystem partSys;

	//Override this on children
	public abstract void TriggerAbility();
}
