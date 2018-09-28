﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	Left,
	Right
}

public class Player : MonoBehaviour, IShopItems {

	public int damage;
	public int defend;
	public int speedFight;
	public int coin;
	public int score;
	public Direction currentDirection;

	public void Fight () {

	}

	public void ChangeDirection () {
		if (currentDirection == Direction.Left)
			currentDirection = Direction.Right;
		else
			currentDirection = Direction.Left;
	}

	public void DisplayInfo () {

	}
	void Start () {
		currentDirection = Direction.Left;
	}

	// Update is called once per frame
	void Update () {

	}

    public string GetInfo()
    {
        return "\nDamage: "+ damage +" \nDefend: "+ defend +"\nSpeed Fight: "+ speedFight;
    }
}