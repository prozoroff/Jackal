using System;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// Common movable unit: pirate, coin etc.
	/// </summary>
	public class WaterUnit : Unit
	{
		public WaterUnit ()
		{
			Height = 0;
			this.GameObject = GameObject.Instantiate (Resources.Load ("Prefabs/FieldCells/Water")) as GameObject; 
			this.GameObject.name = "Water " + Guid.NewGuid ().ToString ();
		}
	}
}

