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
		public MovableUnit (string type)
		{
			_type = type;
			Height = -1;
			this.GameObject = GameObject.Instantiate (Resources.Load ("Prefabs/MovableUnits/" + _type)) as GameObject; 
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
	}
}

