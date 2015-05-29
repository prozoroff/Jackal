using System;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// Common field unit: pirate, coin etc.
	/// </summary>
	public class FieldUnit : PlayingUnit
	{
		public FieldUnit ()
		{
			Height = 0;
		}

		public void SetGameObject(GameObject gameObject, string name){
			this.GameObject = gameObject;
			this.GameObject.name = name;
		}
	}
}

