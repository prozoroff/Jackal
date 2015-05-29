using System;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// Objects factory interface to get objects of specified type.
	/// </summary>
	public interface IObjectsFactory
	{
		GameObject Get(Constants.ObjectTypes objectType);
		void Dispose();
	}
}

