﻿using UnityEngine;
using System.Collections;

namespace IsoUnity.Types
{
    public abstract class IsoUnityType : ScriptableObject, JSONAble
    {
        public abstract bool canHandle(object o);
        public abstract IsoUnityType clone();
        public abstract object Value
        {
            get;
            set;
        }
        public abstract JSONObject toJSONObject();

        public abstract void fromJSONObject(JSONObject json);
    }
}