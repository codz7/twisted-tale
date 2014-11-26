using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class FirstPerson_controller : MonoBehaviour {

	public float Movementspeed = 3.0f;
	public float UpDownRange = 90.0f;
	public float verticalRotation = 0f;
	public float mouseSens = 1.5f;
	public float jumpspeed = 10f;
	public float gravity = -9.81f;
	public float verticalVelocity = 0;
	public bool upGravity = false; // false är ner, true är upp
	public float rotSpeed = 200;
	float rotationZ = 0;
	float rotationY = 0;
	//float rotationX = 0;
	public bool RotStatus;
	float rotLeftRight = 0;

	CharacterController characterController;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		//vsync
		//QualitySettings.vSyncCount = 2;
		
		//kolla runt
		if (upGravity == false) 
		{
			rotLeftRight = Input.GetAxis ("Mouse X") * mouseSens;
		}
		else 
		{
			rotLeftRight = Input.GetAxis ("Mouse X") * mouseSens * -1;
		}

		rotationY += rotLeftRight;
		// snetripp
		//transform.Rotate (0, 0,rotLeftRight);

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSens;
		verticalRotation = Mathf.Clamp (verticalRotation, -90, 90);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation,0,0);


		//run

		if (characterController.isGrounded && Input.GetKeyDown (KeyCode.Z)) 
		{
			Movementspeed = 10;
		}
		else 
		{
			Movementspeed = 5;
		}


		//jump
		if (/*characterController.isGrounded &&*/ Input.GetKeyDown (KeyCode.X)) 
		{

				verticalVelocity = jumpspeed;

		}

		// flip gravity
		if (Input.GetKeyDown ("space"))
		{
			verticalVelocity = 0;
			if(upGravity == false)
			{
				upGravity = true;
			}
			else
			{
				upGravity = false;
			}
		}

		//lugn rotation
		//Skall rotera till 180
		if (upGravity == true && rotationZ <= 180)
		{
			RotStatus=true;
			rotationZ += rotSpeed * Time.deltaTime;
			if( rotationZ >= 180)
			{
				rotationZ = 180;
				RotStatus=false;
			}
		}
		//Skall rotera till noll
		if (upGravity == false && rotationZ >= 0) 
		{
			RotStatus=true;
			rotationZ -= rotSpeed * Time.deltaTime;
			if( rotationZ <= 0)
			{
				rotationZ = 0;
				RotStatus=false;
			}
		}

		/*
		if (Physics.Raycast(transform.position, Vector3.down, 0.1,1))
		{
			Debug.Log ("lajkfnuhbg");		
		}
		*/
		
	
	
		//gravitation
		verticalVelocity += gravity * Time.deltaTime;

		//allt rör på sig
		float forwardspeed = Input.GetAxis("Vertical") * Movementspeed;
		float Sidespeed = Input.GetAxis("Horizontal") * Movementspeed;
		if (RotStatus == true) 
		{
			verticalVelocity = 0;
		}
		Vector3 speed = new Vector3 (Sidespeed, verticalVelocity, forwardspeed);
		transform.rotation = Quaternion.Euler(0, rotationY,rotationZ);
		speed = transform.rotation * speed;	

		characterController.Move(speed * Time.deltaTime);


	}
}