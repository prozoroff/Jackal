using System;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// Common playing unit, cap.
	/// </summary>
	public class PlayingUnit : IJackalGameObject
	{
		private int _height;
		public int Height {
			get {
				return _height;
			}
			protected set{
				if(_height != value)
				_height = value;
			}
		}

		private GameObject _gameObject;
		public GameObject GameObject {
			get {
				return _gameObject;
			}
			protected set{
				if(_gameObject!=value)
					_gameObject = value;
			}
		}

		public PlayingUnit ()
		{
		}
	}
}

