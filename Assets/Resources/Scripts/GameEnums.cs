using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums : MonoBehaviour {

	/* Team Enum */
	public enum Team
	{
		Blue,
		Red
	}

	/* Projectile Enum. Not implemented yet */
	public enum ProjectileType
	{
		Laser,
		Bullet
	}

	/* State of AI enum. Use in to check what flight routine to use */
	public enum AIState
	{
		Idle,
		OnPursuit,
		OnSearch
	}
}
