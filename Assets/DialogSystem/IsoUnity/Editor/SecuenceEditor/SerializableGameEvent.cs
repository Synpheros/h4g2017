﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
[NodeContent("GameEvent", new string[] { "next" })]
public class SerializableGameEvent : ScriptableObject, IGameEvent {

	void OnEnable(){
		if (args == null || args.Count != keys.Count) {
			args = new Dictionary<string, Object>();
			for(int i = 0; i< keys.Count; i++)
				args.Add (keys[i], values[i]);
		}
	}

	[SerializeField]
	public string Name {
		get{ return name; }
		set{
            if(value != "")
                this.name = value;
        }
	}
	[SerializeField]
	private List<string> keys = new List<string> ();
	[SerializeField]
	private List<Object> values = new List<Object>();

	private Dictionary<string, Object> args = new Dictionary<string, Object>();
	public object getParameter(string param){
		param = param.ToLower();
		if (args.ContainsKey (param)) 
			return (args[param] is IsoUnityType)? ((IsoUnityType)args [param]).Value: args[param];
		else 
			return null;
	}

	public void setParameter(string param, object content){
		param = param.ToLower();

		if (content == null) {

			if(args.ContainsKey(param))	args[param] = null;
			else						args.Add(param, null);

		} else {
            bool saved = false;
            if (args.ContainsKey(param))
            {
                var pv = args[param];
                
                if (pv is IsoUnityType)
                {
                    var i = (pv as IsoUnityType);
                    if (i.canHandle(content))
                    {
                        i.Value = content;
                        saved = true;
                    }
                    else ScriptableObject.DestroyImmediate(i, true);
                }
            }

            if(!saved)
            {
                object c = IsoUnityTypeFactory.Instance.getIsoUnityType(content);

                if (c == null)
                    c = content;
                else
                {
                    (c as ScriptableObject).hideFlags = HideFlags.HideInHierarchy;
                    AssetDatabase.AddObjectToAsset(c as Object, this);
                }

                if (args.ContainsKey(param)) args[param] = (Object)c;
                else args.Add(param, (Object)c);
            }
		}
        

		this.keys = new List<string> (args.Keys);
		this.values = new List<Object> (args.Values);
	}

	public void removeParameter(string param){
		param = param.ToLower();
		if (args.ContainsKey(param))
		{
			UnityEngine.Object v = args[param];
			if (v is IsoUnityBasicType)
				IsoUnityTypeFactory.Instance.Destroy(v as IsoUnityBasicType);

			args.Remove(param);
		}

		this.keys = new List<string> (args.Keys);
		this.values = new List<Object> (args.Values);
	}

	public string[] Params{
		get{
			string[] myParams = new string[args.Keys.Count];
			int i = 0;
			foreach(string key in args.Keys){
				myParams[i] = key; i++;
			}
			return myParams;
		}
	}

	public override bool Equals (object o)
	{
		if (o is IGameEvent)
			return GameEvent.CompareEvents (this, o as IGameEvent);
		else
			return false;
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	/*
     * Belong methods
     */

	private const string OWNER_PARAM = "entity";

	public bool belongsTo(GameObject g) { return belongsTo(g, OWNER_PARAM); }
	public bool belongsTo(ScriptableObject so) { return belongsTo(so, OWNER_PARAM); }
	public bool belongsTo(string tag) { return belongsTo(tag, OWNER_PARAM); }

	public bool belongsTo(GameObject g, string parameter)
	{
		object entityParam = getParameter(parameter);
		if (entityParam == null || g == null)
			return false;

		return entityParam == g || ((entityParam is string) && ((string)entityParam) == g.tag);
	}

	public bool belongsTo(ScriptableObject so, string parameter)
	{
		object entityParam = getParameter(parameter);
		if (entityParam == null || so == null)
			return false;

		// Compare normal and name
		return entityParam == so || (entityParam is string && ((string)entityParam) == so.name);
	}

	public bool belongsTo(string tag, string parameter)
	{
		object entityParam = getParameter(parameter);
		if (entityParam == null || tag == null)
			return false;

		return (entityParam is string && ((string)entityParam) == tag)
			|| (entityParam is GameObject) ? (entityParam as GameObject).CompareTag(tag) : false
			|| (entityParam is Component) ? (entityParam as Component).CompareTag(tag) : false;
	}

	/*
     * Operators 
     **/

	public static bool operator ==(SerializableGameEvent ge1, IGameEvent ge2){
		//Debug.Log ("Comparing with operator of SerializableGameEvent");
		return GameEvent.CompareEvents (ge1, ge2);
	}

	public static bool operator !=(SerializableGameEvent ge1, IGameEvent ge2){
		return !(ge1 == ge2);
	}

	/// <summary>
	/// JSon serialization things
	/// </summary>

	public JSONObject toJSONObject()
	{
		JSONObject json = new JSONObject();
		json.AddField("name", name);
		JSONObject parameters = new JSONObject();
		foreach (KeyValuePair<string, Object> entry in args)
		{
			if (entry.Value is JSONAble)
			{
				var jsonAble = entry.Value as JSONAble;
				parameters.AddField(entry.Key, JSONSerializer.Serialize(jsonAble));
			}
			else
			{
				parameters.AddField(entry.Key, entry.Value.GetInstanceID());
			}
		}


		json.AddField("parameters", parameters);
		return json;
	}

    void OnDestroy()
    {
        destroyBasic(args);
    }

	private static void destroyBasic(Dictionary<string, Object> args)
	{
		if (args == null || args.Count == 0)
			return;

		foreach (KeyValuePair<string, Object> entry in args)
			if (entry.Value is IsoUnityBasicType)
				IsoUnityBasicType.DestroyImmediate(entry.Value, true);
	}

    public void fromJSONObject(JSONObject json)
	{
		this.name = json["name"].ToString();

		//Clean basic types
		destroyBasic(this.args);

		this.args = new Dictionary<string, Object>();

		JSONObject parameters = json["parameters"];
		foreach (string key in parameters.keys)
		{
			JSONObject param = parameters[key];
			JSONAble unserialized = JSONSerializer.UnSerialize(param);
			this.setParameter(key, unserialized);
		}
	}
}
