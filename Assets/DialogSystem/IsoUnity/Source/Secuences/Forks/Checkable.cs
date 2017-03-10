using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class Checkable : ScriptableObject, IFork
{
	public abstract bool check();

}