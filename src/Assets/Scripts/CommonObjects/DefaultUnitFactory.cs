using System;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// Default objects factory to get default objects of specified type.
	/// </summary>
	public class DefaultUnitFactory : IUnitFactory
	{
		public DefaultUnitFactory()
		{

		}

		public Unit Get (Constants.ObjectType unitType, string unit)
		{
			switch (unitType)
			{
				case Constants.ObjectType.FieldUnit: return new FieldUnit(unit);
				case Constants.ObjectType.MovableUnit: return new MovableUnit(unit);
				default: return new WaterUnit();
			}

		}
	

		public void Dispose ()
		{
			//TODO
		}

	}
}

