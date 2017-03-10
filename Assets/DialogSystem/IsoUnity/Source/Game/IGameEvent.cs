﻿using UnityEngine;
using System.Collections;

public interface IGameEvent : JSONAble {

	string Name {
		get; set;
	}

	/**
	 * Parameters
	 **/

	object getParameter (string param);
	void setParameter (string param, object content);
	void removeParameter (string param);
	string[] Params {
		get;
	}
		
	/**
	 * Belongs
	 **/

	bool belongsTo (GameObject g);
	bool belongsTo (ScriptableObject so);
	bool belongsTo (string tag);

	bool belongsTo (GameObject g, string parameter);
	bool belongsTo (ScriptableObject so, string parameter);
	bool belongsTo (string tag, string parameter);

	/**
     * Operators 
     **/
	int GetHashCode ();
	bool Equals (object o);
	//static bool operator ==(IGameEvent ge1, IGameEvent ge2);
	//static bool operator !=(IGameEvent ge1, IGameEvent ge2);

}
