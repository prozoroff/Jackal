using UnityEngine;
using System.Collections;

public class Shirt : MonoBehaviour {
	Animator anim;
	BoxCollider2D boxCollider;
	void Start()
	{
		anim = GetComponent<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
	}

	public void Hide()
	{
		anim.SetTrigger("Hide");
		boxCollider.enabled = false;
		enabled = false;
		DestroyObject(this);

	}
}
