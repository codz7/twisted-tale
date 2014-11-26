using UnityEngine;
using System.Collections;

public class Ground_Check : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("colition");
	}

	void OnTriggerStay(Collider other)
	{
		Debug.Log ("inside");
	}

	void OnTriggerExit (Collider other)
	{
		Debug.Log ("outside");
	} 
}