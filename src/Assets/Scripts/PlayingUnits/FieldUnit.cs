using System;
using System.Linq;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// Common field unit: water, forest etc.
	/// </summary>
	public class FieldUnit : Unit
	{
		public FieldUnit (string type)
		{
			_type = type;
			_isOpen = false;
			Height = 0;
			this.GameObject = GameObject.Instantiate (Resources.Load ("Prefabs/FieldCells/" + _type)) as GameObject; 
			this.GameObject.name = _type + Guid.NewGuid ().ToString ();
			UpdateSprite ();
		}

		public void UpdateSprite(){

			if (!_isOpen) {
				this.GameObject.GetComponent<SpriteRenderer> ().sprite = 
					Resources.LoadAll<Sprite> ("Sprites/field").FirstOrDefault(x=>x.name=="Shirt");
				}
			else 
				this.GameObject.GetComponent<SpriteRenderer> ().sprite = 
					Resources.LoadAll<Sprite> ("Sprites/field").FirstOrDefault(x=>x.name==_type);
		}

		public FieldUnit() : this("Water"){
		}

		string _type;
		public string Type { 
			get{ 
				return _type;
			}
		}

		bool _isOpen;
		public bool IsOpen { 
			get{ 
				return _isOpen;
			}
			set{
				_isOpen = value;
				UpdateSprite ();
			}
		}

		public void SetGameObject(GameObject gameObject, string name){
			this.GameObject = gameObject;
			this.GameObject.name = name;
		}

		public void Open(){
		}
	}
}

