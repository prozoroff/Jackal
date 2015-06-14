using UnityEngine;
using System.Collections;
using System.Linq;
using Jackal.Domain;

public class GameManager : MonoBehaviour {
	
	public int GameState = 0;
	public int FieldSize = 8;
	public int PlayersNumber = 2;
	public float turnDelay = 0.5f;
	[HideInInspector] public bool players1Turn = true;

	private BoardManager _playingField;
	PlayingUnit[] Units;
	GameObject SelectedUnit;

	public static GameManager instance = null;
	private bool timeout;

	private IObjectsFactory _objectsFactory = new DefaultObjectsFactory();

	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
			//if not, set instance to this
			instance = this;
		
		//If instance already exists and it's not this:
		else if (instance != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject); 

		_playingField = GetComponent<BoardManager>();
		InitGame();
	}

	void InitGame()
	{
		_playingField.SetupScene( BoardManager.FieldType.ftCLASSIC );
	}

	// Use this for initialization
	void Start () 
	{

		//InitPlayingField ();
		//InitUnits ();
		//ClearUselessResources ();

	}

	//Update is called every frame.
	void Update()
	{
		//Check that playersTurn or enemiesMoving or doingSetup are not currently true.
		if(players1Turn || timeout)
			
			//If any of these are true, return and do not start MoveEnemies.
			return;
		
		//Start moving enemies.
		StartCoroutine (ResetTurn ());
	}

	//Coroutine to move enemies in sequence.
	IEnumerator ResetTurn()
	{
		timeout = true;
		yield return new WaitForSeconds(turnDelay);
		players1Turn = true;
		timeout = false;
	}


	private void InitPlayingField(){
		_playingField = new BoardManager(FieldSize);
		_playingField.Init (_objectsFactory);
	}

	private void InitUnits(){
		int _unitsNumber = getUnitsNumber(PlayersNumber, FieldSize);
		Units = new PlayingUnit[_unitsNumber];
		Units [0] = new FieldUnit ();
		(Units [0] as FieldUnit).SetGameObject(Object.Instantiate(
			_objectsFactory.Get(Constants.ObjectTypes.FieldUnit),new Vector3(0,Units [0].Height,FieldSize/2), 
			Quaternion.identity) as GameObject, "Goddam pirate");
	}

	public void SelectUnit(GameObject unitToSelect)
	{
		if (!unitToSelect.name.Contains ("Cell")) {
			if (unitToSelect == SelectedUnit) {
				SelectedUnit.GetComponent<Renderer> ().material.color = Color.white;
				SelectedUnit = null;
			} else {
				if (SelectedUnit != null) {
					SelectedUnit.GetComponent<Renderer> ().material.color = Color.white;
				}
				SelectedUnit = unitToSelect;
				SelectedUnit.GetComponent<Renderer> ().material.color = Color.green;
			}
		}
	}

	public void MoveUnit(Vector2 coordToMove)
	{
		if (SelectedUnit != null) {
			bool validMovementBool = false;
			Vector2 _coordPiece = new Vector2 (SelectedUnit.transform.position.x, 
		                                  SelectedUnit.transform.position.z);

			if (coordToMove.x != _coordPiece.x || coordToMove.y != _coordPiece.y) {
				validMovementBool = true;
			}
		
			if (validMovementBool) {
				SelectedUnit.transform.position = new Vector3 (coordToMove.x, SelectedUnit.transform.position.y, coordToMove.y);		// Move the piece
				SelectedUnit.GetComponent<Renderer> ().material.color = Color.white;	
				SelectedUnit = null;									
			}
		}
	}
	
	private void ClearUselessResources(){
		_objectsFactory.Dispose ();
	}

	private int getUnitsNumber(int playersNumber, int boardSize){
		return 1; //fucking magic
	}
}
