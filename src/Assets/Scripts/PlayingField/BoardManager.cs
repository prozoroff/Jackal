using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;     
using System.Linq;
using Object = UnityEngine.Object;

namespace Jackal.Domain
{
	/// <summary>
	/// Playing field, square area of some size. Generally, array of Game objects.
	/// </summary>
	public class BoardManager : MonoBehaviour
	{
		public List<Unit> FieldArray;
		public List<Unit> MovableUnits;
		private Dictionary<string, Constants.ObjectType> _availableUnitTypes;
		private MovableUnit _selectedUnit;
		private Unit _targetUnit;
		private int _columns;
		private int _rows;
		private IUnitFactory _unitFactory = new DefaultUnitFactory ();

		private Rigidbody2D rb2D;
		public float moveTime = 0.1f;
		private float inverseMoveTime;
		
		public BoardManager ():this(10)
		{
		}
		
		public BoardManager (int fieldSize)
		{
			_columns = fieldSize;
			_rows = fieldSize;
			FieldArray = new List<Unit> ();
			MovableUnits = new List<Unit> ();
			InitAvailableUnitTypes ();
			inverseMoveTime = 1 / moveTime;
		}

		//Process action on unit after click(tap)
		public void ProcessUnitAction (GameObject objectToSelect, int xDir, int yDir)
		{
			//select Unit using clicked object
			_targetUnit = FieldArray.LastOrDefault (x => x.Name == objectToSelect.name);
			if(_targetUnit == null)
				_targetUnit = MovableUnits.LastOrDefault (x => x.Name == objectToSelect.name);

			//move
			if (_targetUnit != null && !(_targetUnit is MovableUnit)) {
				if (_selectedUnit != null) {
						Move();
					}
				}
			else if (_targetUnit != null && _targetUnit is MovableUnit) {
				//or unselect
				if(_selectedUnit == _targetUnit){
					_selectedUnit.Unselect();
					_selectedUnit = null;
				}
				//or select
				else{
					_selectedUnit = _targetUnit as MovableUnit;
					_selectedUnit.Select();
				}
			}
		}

		//Move unit
		protected bool Move (){
			StartCoroutine (SmoothMovement (_targetUnit, _selectedUnit));
			return true;
		}

		protected IEnumerator SmoothMovement (Unit targetUnit, Unit unit)
		{
			//If closed than flip
			if (targetUnit is FieldUnit &&  !(targetUnit as FieldUnit).IsOpen) {
				int step = 5;
				int rotationsNumber = 180 / step;
			
				for (int i = 0; i < rotationsNumber; i++) {
				
					if (i == rotationsNumber / 2) {
						(targetUnit as FieldUnit).IsOpen = !(targetUnit as FieldUnit).IsOpen;
					}
				
					(targetUnit as FieldUnit).Rotate (0, step, 0);
					yield return null;
				}
			}

			float sqrRemainingDistance = (unit.Position - targetUnit.Position).sqrMagnitude;
			
			while(sqrRemainingDistance > float.Epsilon)
			{
				Vector3 newPostion = Vector3.MoveTowards(unit.Position, targetUnit.Position, inverseMoveTime * Time.deltaTime);
				unit.Position = newPostion;
				sqrRemainingDistance = (unit.Position - targetUnit.Position).sqrMagnitude;
				yield return null;
			}

			_selectedUnit.Unselect ();
			_selectedUnit = null;
		}

		public void SetupScene ()
		{
			SetupMovableUnits ();
			SetupBoard ();
		}

		void SetupBoard ()
		{
			List<string> fieldUnitTypes = GetFieldUnitTypes ();

			for (int x = 0; x <_columns; x++)
				for (int y = 0; y <_rows; y++) {
					Unit newUnit = null;

					if (x > 0 && x < _columns - 1 && y > 0 && y < _rows - 1)
						newUnit = _unitFactory.Get (Constants.ObjectType.FieldUnit, fieldUnitTypes [(_columns - 2) * (y - 1) + x - 1]);
					else if (!(x == 0 && y == 0 || x == 0 && y == _rows - 1 || x == _columns - 1 && y == 0 || x == _columns - 1 && y == _rows - 1))
						newUnit = _unitFactory.Get (Constants.ObjectType.WaterUnit, "");

					if (newUnit != null) {
						newUnit.Position += new Vector2 (x, y); 
						FieldArray.Add (newUnit);
					}
				}
		}

		void SetupMovableUnits ()
		{
			var ship = _unitFactory.Get (Constants.ObjectType.MovableUnit, "ShipBlack");
			ship.Position = new Vector2 (4, 0);
			ship.Name = "ShipBlack";
			MovableUnits.Add (ship);
		}

		List<string> GetFieldUnitTypes ()
		{
			List<string> result = new List<string> ();
			result.AddRange (_availableUnitTypes.Where (x => x.Value == Constants.ObjectType.FieldUnit).Select (x => x.Key));
			for (int i = result.Count; i < (_rows-2)*(_columns-2); i++) {
				result.Add ("Shirt");
			}
			result.Shuffle ();
			return result;
		}

		void InitAvailableUnitTypes ()
		{
			_availableUnitTypes = new Dictionary<string, Constants.ObjectType> ();
			_availableUnitTypes.Add ("Water", Constants.ObjectType.WaterUnit);
			_availableUnitTypes.Add ("Arrow3", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("ArrowDiagonal1", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("ArrowDiagonal2", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("ArrowDiagonal4", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("ArrowStraight1", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("ArrowStraight2", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("ArrowStraight4", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Balloon", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Cannon", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Crocodile", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Forest1", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Forest2", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Forest3", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Forest4", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Fortress", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Horse", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Ice", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("ManEater", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Plane", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Rum", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Skip2", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Skip3", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Skip4", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Skip5", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Trap", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Treasure1", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Treasure2", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Treasure3", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Treasure4", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Treasure5", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Shirt", Constants.ObjectType.FieldUnit);
			_availableUnitTypes.Add ("Pirate", Constants.ObjectType.MovableUnit);
			_availableUnitTypes.Add ("Ship", Constants.ObjectType.MovableUnit);
			_availableUnitTypes.Add ("Coin", Constants.ObjectType.MovableUnit);
			_availableUnitTypes.Add ("Chest", Constants.ObjectType.MovableUnit);
		}

	}
}



