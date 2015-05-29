using System;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// Common field unit: pirate, coin etc.
	/// </summary>
	public class FieldCell : IJackalGameObject
	{
		public FieldCell ()
		{
			_height = -1;

		}

		public void SetGameObject(GameObject gameObject, string name){
			_gameObject = gameObject;
			_gameObject.name = name;
		}

		private GameObject _gameObject;
		public GameObject GameObject {
			get {
				return _gameObject;
			}
		}

		private int _height;
		public int Height {
			get {
				return _height;
			}
		}
	}
}

