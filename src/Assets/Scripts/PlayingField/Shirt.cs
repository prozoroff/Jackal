using UnityEngine;
using System.Collections;

public class Shirt : MonoBehaviour {
	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	public void Hide()
	{
		anim.SetTrigger("Hide");
		enabled = false;
		DestroyObject(this);
	}
}
