using System;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// Objects factory interface to get objects of specified type.
	/// </summary>
	public interface IUnitFactory
	{
		Unit Get(Constants.ObjectType unitType, string unit);
		void Dispose();
	}
}

