using System;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

namespace Jackal.Domain
{
	/// <summary>
	/// Playing field, square area of some size. Generally, array of Game objects.
	/// </summary>
	public class PlayingField 
	{
		private static int _fieldSize;
		public FieldCell[,] FieldArray;

		public PlayingField (int fieldSize)
		{
			_fieldSize = fieldSize;
			FieldArray = new FieldCell[_fieldSize,_fieldSize];
		}

		public PlayingField():this(8){
		}

		public void Init( IObjectsFactory objectFactory){
			_initField (objectFactory);
		}

		private void _initField(IObjectsFactory objectFactory)
		{
			for(int i = 0; i < _fieldSize; i++)
			{
				for(int j = 0; j < _fieldSize; j++)
				{
					FieldArray[i,j] = new FieldCell();
					FieldArray[i,j].SetGameObject(Object.Instantiate(
						objectFactory.Get(Constants.ObjectTypes.FieldCell),new Vector3(i,FieldArray[i,j].Height,j), 
						Quaternion.identity) as GameObject, "FieldCell: " + i.ToString()+j.ToString());
				}
			}

			objectFactory = null;
		}
	}
}

