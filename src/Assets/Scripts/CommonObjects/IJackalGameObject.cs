using System;
using UnityEngine;

namespace Jackal.Domain
{
	/// <summary>
	/// General jackal game object. Now it has height property only.
	/// </summary>
	public interface IJackalGameObject
	{
		GameObject GameObject { get; }
		int Height { get; }
	}
}

