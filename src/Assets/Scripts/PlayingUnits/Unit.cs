using System;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// Common playing unit, cap.
	/// </summary>
	public class Unit : JackalGameObject
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
		protected GameObject GameObject {
			get {
				return _gameObject;
			}
			set{
				if(_gameObject!=value)
					_gameObject = value;
			}
		}

		public string Name{
			get {
				return _gameObject.name;
			}
			set{
				if(_gameObject.name!=value)
					_gameObject.name = value;
			}
		}

		public Vector2 Position{
			get {
				return new Vector2( _gameObject.transform.position.x, 
				                   _gameObject.transform.position.y);
			}
			set{
				_gameObject.transform.position = new Vector3(value.x, value.y, Height);
			}
		}

		public Quaternion Rotation{
			get {
				return _gameObject.transform.rotation;
			}
			set{
				_gameObject.transform.rotation = value;
			}
		}

		public void Rotate(float xAngle, float yAngle, float zAngle){
			_gameObject.transform.Rotate (xAngle, yAngle, zAngle);
		}

		public Unit ()
		{
		}
	}
}

