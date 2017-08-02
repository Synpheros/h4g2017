﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;

namespace IsoUnity {
	public class ParamEditor {
		
		public class ParamType {
			public string name;
			public object defaultValue;
			public string fieldEditorName;

			public ParamType(string n, object def, string fn){
				this.name = n;
				this.defaultValue = def;
				this.fieldEditorName = fn;
			}
		}

	    public static object editorFor(Rect rect, string label, object v)
	    {
	        return editorFor(rect, label, v, true);
	    }

	    public static object editorFor(Rect rect, object v)
	    {
	        return editorFor(rect, string.Empty, v, true);
	    }

	    public static object editorFor(Rect rect, string label, object v, bool allowUnityObject)
	    {
	        return editorFor(rect, label, v, allowUnityObject, false);
	    }

	    public static object editorFor(Rect rect, string label, object v, bool allowUnityObject, bool fixedType)
	    {

	        Dictionary<System.Type, ParamEditor.ParamType> defaults = new Dictionary<System.Type, ParamEditor.ParamType>();

	        if (allowUnityObject)
	            defaults.Add(typeof(Object), new ParamType("UnityObject", null, "ObjectField"));

	        defaults.Add(typeof(int), new ParamType("Int", 0, "IntField"));
	        defaults.Add(typeof(bool), new ParamType("Boolean", false, "Toggle"));
	        defaults.Add(typeof(float), new ParamType("Float", 0f, "FloatField"));
	        defaults.Add(typeof(string), new ParamType("String", "", "TextField"));
	        defaults.Add(typeof(Vector2), new ParamType("Vector2", new Vector2(0, 0), "Vector2Field"));
	        defaults.Add(typeof(Vector3), new ParamType("Vector3", new Vector3(0, 0, 0), "Vector3Field"));

	        return editorFor(rect, label, v, defaults, fixedType);
	    }

	    public static object editorFor(Rect rect, string label, object v, Dictionary<System.Type, ParamEditor.ParamType> typesAccepted)
	    {
	        return editorFor(rect, label, v, typesAccepted, false);
	    }

	    public static object editorFor(Rect rect, string label, object v, Dictionary<System.Type, ParamEditor.ParamType> typesAccepted, bool fixedType)
	    {

	        EditorGUILayout.BeginHorizontal();

	        int pretipo = (v != null) ? new List<System.Type>(typesAccepted.Keys).IndexOf(v.GetType()) : 0;
	        if (pretipo == -1) pretipo = 0;

	        int tipo = pretipo;
	        if (!fixedType)
	        {
	            List<string> nombres = new List<string>();
	            foreach (ParamType p in typesAccepted.Values)
	                nombres.Add(p.name);
	            tipo = EditorGUI.Popup(new Rect(rect.position.x, rect.position.y, rect.width * .33f, rect.height), pretipo, nombres.ToArray() as string[]);
	        }

	        object returnable = v;

	        ParamType pt = new List<ParamType>(typesAccepted.Values)[tipo];

	        if (pretipo != tipo)
	            returnable = pt.defaultValue;

	        MethodInfo mi = null;

	        if (pt.fieldEditorName == "ObjectField")
	        {
	            returnable = EditorGUI.ObjectField(new Rect(rect.position.x + rect.width * .33f, rect.position.y, rect.width * .66f, rect.height), label, (Object)returnable, typeof(Object), true);
	        }
	        else
	        {
	            mi = typeof(EditorGUI).GetMethod(pt.fieldEditorName, new System.Type[] { typeof(Rect), typeof(string), new List<System.Type>(typesAccepted.Keys)[tipo]});
	            returnable = mi.Invoke(null, new object[] { new Rect(rect.position.x + rect.width * .33f, rect.position.y, rect.width * .66f, rect.height), label, returnable});
	        }



	        EditorGUILayout.EndHorizontal();

	        return returnable;
	    }

	    public static object LayoutEditorFor(string label, object v){
			return LayoutEditorFor(label, v, true);
		}

		public static object LayoutEditorFor(string label, object v, bool allowUnityObject){
			return LayoutEditorFor(label, v, allowUnityObject, false);
		}

		public static object LayoutEditorFor(string label, object v, bool allowUnityObject, bool fixedType){

			Dictionary<System.Type, ParamEditor.ParamType> defaults = new Dictionary<System.Type, ParamEditor.ParamType>();

			if(allowUnityObject)
				defaults.Add (typeof(Object), new ParamType("UnityObject", null, "ObjectField"));

			defaults.Add (typeof(int), new ParamType("Int", 0, "IntField"));
			defaults.Add (typeof(bool), new ParamType("Boolean", false, "Toggle"));
			defaults.Add (typeof(float), new ParamType("Float", 0f, "FloatField"));
			defaults.Add (typeof(string), new ParamType("String", "", "TextField"));
			defaults.Add (typeof(Vector2), new ParamType("Vector2", new Vector2(0,0), "Vector2Field"));
			defaults.Add (typeof(Vector3), new ParamType("Vector3", new Vector3(0,0,0), "Vector3Field"));

			return editorFor (label, v, defaults, fixedType); 
		}

		public static object LayoutEditorFor(string label, object v, Dictionary<System.Type, ParamEditor.ParamType> typesAccepted){
			return editorFor (label, v,typesAccepted, false);
		}

		public static object editorFor(string label, object v, Dictionary<System.Type, ParamEditor.ParamType> typesAccepted, bool fixedType){

			EditorGUILayout.BeginHorizontal();

			int pretipo = (v!=null)? new List<System.Type>(typesAccepted.Keys).IndexOf(v.GetType()):0;
			if(pretipo == -1) pretipo = 0;

			int tipo = pretipo;
			if(!fixedType){
				List<string> nombres = new List<string>();
				foreach(ParamType p in typesAccepted.Values)
					nombres.Add(p.name);
				tipo = EditorGUILayout.Popup(pretipo, nombres.ToArray() as string[]);
			}

			object returnable = v;

			ParamType pt = new List<ParamType>(typesAccepted.Values)[tipo];

			if(pretipo != tipo)
				returnable = pt.defaultValue;

			MethodInfo mi = null;

			if(pt.fieldEditorName == "ObjectField"){	
				returnable = EditorGUILayout.ObjectField(label,(Object)returnable, typeof(Object), true);
			}else{						
				mi = typeof(EditorGUILayout).GetMethod(pt.fieldEditorName, new System.Type[]{typeof(string), new List<System.Type>(typesAccepted.Keys)[tipo], typeof(GUILayoutOption)});
				returnable = mi.Invoke(null,new object[]{label,returnable, new GUILayoutOption[0]});
			}



			EditorGUILayout.EndHorizontal();

			return returnable;
		}
	}
}