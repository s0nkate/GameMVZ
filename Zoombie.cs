using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Zoom{
public enum Type
	{
		Walker,
		Runner,
		Hulker,
		Exploder
	}


public class Zoombie : MonoBehaviour {

	public Transform target;
	public int heath =100;
	public int maxHeath =100;
	public float timeDelay= .3f;
	public int damage;
	public float speed;
	public Slider heathSlider;
	
	public int currentStage = 1;
	public Transform zoombie;
	public const int STAGE_IDLE = 0;
	public const int STAGE_WALK = 1;
	public const int STAGE_ATTACK = 2;
	public const int STAGE_DEAD = 3;
	public Type type;
	public Direction currentDirection = Direction.Right;
	public GameObject explosion;
	private int direction;
	private Animator animator;
	private bool isExploded = false;
	
	void Start () {
		heathSlider.maxValue = maxHeath;
		animator = GetComponentInChildren<Animator>();
		if(currentDirection == Direction.Left)
			zoombie.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
		InitZoombie();
	}
	
	// Update is called once per frame
	void Update () {
		
		heathSlider.value = heath;
		if(Input.GetKeyDown(KeyCode.A))
		{
			ChangeStage();
		}
		CheckStage();
		
	}

	void CheckStage()
	{
		if(heath <= 0 )
		{
			currentStage = STAGE_DEAD;
		}
		switch (currentStage)
		{
			// case STAGE_IDLE:
			// 	Idle();
			// 	break;
			case STAGE_WALK:				
				Walk();
				break;
			case STAGE_ATTACK:
				Attack();
				break;
			case STAGE_DEAD:				
				Dead();
				break;
			default: 
				Walk();
				break;
		}

	}

	void InitZoombie()
	{
		switch (type)
		{
			case Type.Walker :
				maxHeath = 100;
				damage = 10;
				speed = .1f;
				break;
			case Type.Runner :
				maxHeath = 80;
				damage = 15;
				speed = .5f;
				break;
			case Type.Hulker :
				maxHeath = 300;
				damage = 20;
				speed = .1f;
				transform.localScale = new Vector3(3, 3, 0);
				break;
			case Type.Exploder :
				maxHeath = 10;
				damage = 50;
				speed = .1f;
				break;
		}
	}

	// void Idle()
	// {
	// 	animator.SetInteger("stage", STAGE_IDLE);
	// 	Stop();
	// }
	void Walk()
	{
		animator.SetInteger("stage", STAGE_WALK);
		if(currentDirection == Direction.Left)
		{
			direction = -1;
		}	
		else
		{
			direction = 1;
		}

		transform.Translate(speed * Time.deltaTime * direction, 0 , 0);
	}

	void Dead()
	{
		if(type == Type.Exploder)
		{
			Explosion();
			Destroy(gameObject);
			return;
		}
		animator.SetInteger("stage", STAGE_DEAD);
		Stop();
	}

	void Attack()
	{
		animator.SetInteger("stage", STAGE_ATTACK);
		if(type == Type.Exploder)
		{
			Dead();
		}
		Stop();
	}

	void Stop()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0).normalized;
	}

	public void Explosion()
	{
		if(!isExploded)
		{
			explosion = Instantiate(explosion, transform.position, transform.rotation);
			isExploded = true;
		}
	}

	public void TakeDamage(int damage)
	{
		if(heath > 0)
		{
			heath -= damage;
		}
	}

	public void ChangeStage()
	{
		if(currentStage == STAGE_WALK)
		{
			currentStage = STAGE_ATTACK;
		}else
		{
			currentStage = STAGE_WALK;
		}
	}
	
}
}