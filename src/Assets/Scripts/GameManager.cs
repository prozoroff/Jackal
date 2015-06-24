using UnityEngine;
using System.Collections;
using System.Linq;
using Jackal.Domain;

public class GameManager : MonoBehaviour {

	private BoardManager _playingField;
	public static GameManager instance = null;

	void Awake(){

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);	

		InitGame();
	}

	void InitGame()
	{
		_playingField = GetComponent<BoardManager> ();
		_playingField.SetupScene( );
	}

	public void Update()
	{
		GetMouseInputs ();
	}

	void GetMouseInputs()
	{	
		Ray ray;
		
		if(Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
			if (hit) {
				_playingField.ProcessUnitAction(hit.collider.gameObject,(int)hit.collider.gameObject.transform.position.x,
				                               (int)hit.collider.gameObject.transform.position.y);
			}
		}
	}
	


}
