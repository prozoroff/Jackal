using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.
using Object = UnityEngine.Object;

namespace Jackal.Domain
{
	/// <summary>
	/// Playing field, square area of some size. Generally, array of Game objects.
	/// </summary>
	public class BoardManager : MonoBehaviour
	{
		public enum FieldType
		{
			ftCLASSIC
		}
		public FieldCell[,] FieldArray;

		private static int _fieldSize;
		private Transform boardHolder; //A variable to store a reference to the transform of our Board object.

		public GameObject hideFieldTile; 
		public GameObject waterFieldTile; 

		public GameObject aborigensTiles;
		public GameObject arrow3;
		public GameObject arrowDiagonal1Tiles;
		public GameObject arrowDiagonal2Tiles;
		public GameObject arrowDiagonal4Tiles;
		public GameObject arrowStraight1Tiles;
		public GameObject arrowStraight2Tiles;
		public GameObject arrowStraight4Tiles;
		public GameObject balloonTiles;
		public GameObject cannonTiles;
		public GameObject crocodileTiles;
		public GameObject forest1Tiles;
		public GameObject forest2Tiles;
		public GameObject forest3Tiles;
		public GameObject forest4Tiles;
		public GameObject fortressTiles;
		public GameObject horseTiles;
		public GameObject iceTiles;
		public GameObject manEaterTiles;
		public GameObject planeTiles;
		public GameObject rumTiles;
		public GameObject skip2Tiles;
		public GameObject skip3Tiles;
		public GameObject skip4Tiles;
		public GameObject skip5Tiles;
		public GameObject trapTiles;
		public GameObject treasure1Tiles;
		public GameObject treasure2Tiles;
		public GameObject treasure3Tiles;
		public GameObject treasure4Tiles;
		public GameObject treasure5Tiles;


		private int columns = 12;
		private int rows = 12;
		private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.

		public BoardManager():this(8){
		}
		
		public BoardManager (int fieldSize)
		{
			_fieldSize = fieldSize;
			FieldArray = new FieldCell[_fieldSize,_fieldSize];
		}

		//Clears our list gridPositions and prepares it to generate a new board.
		void InitialiseList ()
		{
			//Clear our list gridPositions.
			gridPositions.Clear ();

			for(int x = 0; x <= columns; x++)
			{
				//Loop along y axis, starting from -1 to place floor or outerwall tiles.
				for(int y = 0; y <= rows; y++)
				{
					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
					GameObject toInstantiate = hideFieldTile;
					
					//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
					if(
						x == 0 || x == columns || y == 0 || y == rows ||
						x == 1 && y == 1 || 
						x == columns - 1 && y == rows - 1 || 
						x == columns - 1 && y == 1 ||
						x == 1 && y == rows -1
						)
						continue;
					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
		}

		//RandomPosition returns a random position from our list gridPositions.
		Vector3 RandomPosition ()
		{
			//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
			int randomIndex = UnityEngine.Random.Range (0, gridPositions.Count);
			
			//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
			Vector3 randomPosition = gridPositions[randomIndex];
			
			//Remove the entry at randomIndex from the list so that it can't be re-used.
			gridPositions.RemoveAt (randomIndex);
			
			//Return the randomly selected Vector3 position.
			return randomPosition;
		}

		//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
		void LayoutObjectAtRandom (GameObject tile, int objectCount)
		{
			//Instantiate objects until the randomly chosen limit objectCount is reached
			for(int i = 0; i < objectCount; i++)
			{
				//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
				Vector3 randomPosition = RandomPosition();
				
				//Choose a random tile from tileArray and assign it to tileChoice
				GameObject tileChoice = tile;
				
				//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
				Instantiate(tileChoice, randomPosition, Quaternion.identity);
			}
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

		public void SetupScene( FieldType fieldType )
		{
			if( fieldType != FieldType.ftCLASSIC )
				return;

			// Созадание воды + закрытых полей
			BoardSetup ();

			//Reset our list of gridpositions.
			InitialiseList ();

			LayoutObjectAtRandom(aborigensTiles, 1);
			LayoutObjectAtRandom(arrow3, 3);
			LayoutObjectAtRandom(arrowDiagonal1Tiles, 3);
			LayoutObjectAtRandom(arrowDiagonal2Tiles, 3);
			LayoutObjectAtRandom(arrowDiagonal4Tiles, 3);
			LayoutObjectAtRandom(arrowStraight1Tiles, 3);
			LayoutObjectAtRandom(arrowStraight2Tiles, 3);
			LayoutObjectAtRandom(arrowStraight4Tiles, 3);
			LayoutObjectAtRandom(balloonTiles, 2);
			LayoutObjectAtRandom(cannonTiles, 2);
			LayoutObjectAtRandom(crocodileTiles, 4);
			LayoutObjectAtRandom(forest1Tiles, 10);
			LayoutObjectAtRandom(forest2Tiles, 10);
			LayoutObjectAtRandom(forest3Tiles, 10);
			LayoutObjectAtRandom(forest4Tiles, 10);
			LayoutObjectAtRandom(fortressTiles, 2);
			LayoutObjectAtRandom(horseTiles, 2);
			LayoutObjectAtRandom(iceTiles, 6);
			LayoutObjectAtRandom(manEaterTiles, 1);
			LayoutObjectAtRandom(planeTiles, 1);
			LayoutObjectAtRandom(rumTiles, 4);
			LayoutObjectAtRandom(skip2Tiles, 5);
			LayoutObjectAtRandom(skip3Tiles, 4);
			LayoutObjectAtRandom(skip4Tiles, 2);
			LayoutObjectAtRandom(skip5Tiles, 1);
			LayoutObjectAtRandom(trapTiles, 3);
			LayoutObjectAtRandom(treasure1Tiles, 5);
			LayoutObjectAtRandom(treasure2Tiles, 5);
			LayoutObjectAtRandom(treasure3Tiles, 3);
			LayoutObjectAtRandom(treasure4Tiles, 2);
			LayoutObjectAtRandom(treasure5Tiles, 1);
		}

		void BoardSetup(){
			//Instantiate Board and set boardHolder to its transform.
			boardHolder = new GameObject ("Board").transform;
			
			//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
			for(int x = 0; x <= columns; x++)
			{
				//Loop along y axis, starting from -1 to place floor or outerwall tiles.
				for(int y = 0; y <= rows; y++)
				{
					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
					GameObject toInstantiate = hideFieldTile;
					
					//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
					if(
						x == 0 && y == 0 || 
						x == columns && y == rows || 
						x == columns && y == 0 ||
						x == 0 && y == rows
					)
						continue;

					if(
						x == 0 || x == columns || y == 0 || y == rows ||
						x == 1 && y == 1 || 
						x == columns - 1 && y == rows - 1 || 
						x == columns - 1 && y == 1 ||
						x == 1 && y == rows -1
					)
						toInstantiate = waterFieldTile;

					
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					GameObject instance =
						Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					
					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent (boardHolder);
				}
			}
		}

	}
}

