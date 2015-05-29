using System;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// Default objects factory to get default objects of specified type.
	/// </summary>
	public class DefaultObjectsFactory : IObjectsFactory
	{
		private GameObject _defaultFieldCellGameObject;
		private GameObject _defaultFieldUnitGameObject;

		public DefaultObjectsFactory ()
		{

		}

		public GameObject Get (Constants.ObjectTypes objectType)
		{
			//Instantiate default objects for the first call
			if(_defaultFieldCellGameObject == null) 
				_defaultFieldCellGameObject = GameObject.CreatePrimitive (PrimitiveType.Cube);
			if(_defaultFieldUnitGameObject == null) 
				_defaultFieldUnitGameObject = GameObject.CreatePrimitive (PrimitiveType.Capsule);

			if(objectType == Constants.ObjectTypes.FieldCell) return _defaultFieldCellGameObject;
			else if(objectType == Constants.ObjectTypes.FieldUnit) return _defaultFieldUnitGameObject;

			return null;
		}

		public void Dispose ()
		{
			GameObject.Destroy (_defaultFieldCellGameObject, 0.0f);
			GameObject.Destroy (_defaultFieldUnitGameObject, 0.0f);
		}

	}
}

