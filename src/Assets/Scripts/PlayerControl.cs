using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	
	private Camera PlayerCam;			
	private GameManager _gameManager; 	

	void Start () 
	{
		PlayerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 
		PlayerCam.transform.position = new Vector3 (0, 7, 1);
		PlayerCam.transform.rotation = Quaternion.Euler (70,87,0);
		_gameManager = gameObject.GetComponent<GameManager>();
	}

	void Update () {
		GetMouseInputs();
	}

	void GetMouseInputs()
	{	
		Ray _ray;
		RaycastHit _hitInfo;

		if(Input.GetMouseButtonDown(0))
		{
			_ray = PlayerCam.ScreenPointToRay(Input.mousePosition); 

			if(Physics.Raycast (_ray,out _hitInfo))
			{
				_gameManager.SelectUnit(_hitInfo.collider.gameObject);
			}
		}

		Vector2 selectedCoord;

		if(Input.GetMouseButtonDown(0))
		{
			_ray = PlayerCam.ScreenPointToRay(Input.mousePosition); 

			if(Physics.Raycast (_ray,out _hitInfo))
			{
				selectedCoord = new Vector2(_hitInfo.collider.gameObject.transform.position.x,
				                            _hitInfo.collider.gameObject.transform.position.z);
				_gameManager.MoveUnit(selectedCoord);
			}
		}	
	}
}
