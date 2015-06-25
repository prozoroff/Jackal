using System;
using UnityEngine;
using System.Collections;

namespace Jackal.Domain
{
	/// <summary>
	/// Common movable unit: pirate, coin etc.
	/// </summary>
	public class MovableUnit : Unit
	{
		public float moveTime = 0.1f;
		private float inverseMoveTime;

		public MovableUnit (string type)
		{
			_type = type;
			Height = -1;
			this.GameObject = GameObject.Instantiate (Resources.Load ("Prefabs/MovableUnits/" + _type)) as GameObject;
			this.GameObject.name = _type + " " + Guid.NewGuid ().ToString ();
			inverseMoveTime = 1 / moveTime;
		}
		
		public MovableUnit() : this("Pirate"){
		}

		string _type;
		public string Type { 
			get{ 
				return _type;
			}
		}

		public void Unselect(){
			this.GameObject.transform.localScale = new Vector3 (1, 1, 1);
		}

		public void Select(){
			this.GameObject.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
		}

		public IEnumerator Action (Unit targetUnit)
		{
			return Enumerator.Concat(Flip (targetUnit), Move (targetUnit), UnselectUnit());
		}

		private IEnumerator Flip(Unit targetUnit){
			if (targetUnit is FieldUnit && !(targetUnit as FieldUnit).IsOpen) {
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
		}

		private IEnumerator Move(Unit targetUnit){
			float sqrRemainingDistance = (Position - targetUnit.Position).sqrMagnitude;
			while(sqrRemainingDistance > float.Epsilon)
			{
				Vector3 newPostion = Vector3.MoveTowards(Position, targetUnit.Position, inverseMoveTime * Time.deltaTime);
				Position = newPostion;
				sqrRemainingDistance = (Position - targetUnit.Position).sqrMagnitude;
				yield return null;
			}
		}

		private IEnumerator UnselectUnit(){
			Unselect ();
			yield return null;
		}
	}
}

