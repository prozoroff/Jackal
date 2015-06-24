using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	
	private Camera PlayerCam;			
	private GameManager _gameManager; 	

	void Start () 
	{
		PlayerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 
		_gameManager = new GameManager ();
	}

	void Update () {
		_gameManager.Update ();
	}


}
