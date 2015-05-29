using UnityEngine;
using System.Collections;
using System.Linq;
using Jackal.Domain;

public class GameManager : MonoBehaviour {
	
	public int GameState = 0;
	public int FieldSize = 8;
	public int PlayersNumber = 2;

	public PlayingField PlayingField;
	PlayingUnit[] Units;
	GameObject SelectedUnit;

	private IObjectsFactory _objectsFactory = new DefaultObjectsFactory();


	// Use this for initialization
	void Start () {

		InitPlayingField ();
		InitUnits ();
		ClearUselessResources ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void InitPlayingField(){
		PlayingField = new PlayingField(FieldSize);
		PlayingField.Init (_objectsFactory);
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
