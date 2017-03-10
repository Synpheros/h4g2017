﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SerializableGameEvent))]
public class GameEventEditor : NodeContentEditor {
    
    private EventEditor currentEditor;

    protected override void NodeContentInspectorGUI()
    { 
        SerializableGameEvent ge = (SerializableGameEvent)target;
        string[] editors = EventEditorFactory.Intance.CurrentEventEditors;
        int editorSelected = 0;

        for (int i = 1; i < editors.Length; i++)
            if (ge != null && editors[i].ToLower() == ge.Name.ToLower())
                editorSelected = i;

        int was = editorSelected;

        editorSelected = EditorGUILayout.Popup(editorSelected, EventEditorFactory.Intance.CurrentEventEditors);
        if (currentEditor == null || was != editorSelected)
        {
            if (currentEditor != null && ge != null)
                currentEditor.detachEvent(ge);

            if (ge != null)
                ge.Name = "";

            currentEditor = EventEditorFactory.Intance.createEventEditorFor(editors[editorSelected]);
            currentEditor.useEvent(ge);
        }

        currentEditor.draw();

        /**
         *  Game event synchronization
         * */

        if (!(ge.getParameter("synchronous") is bool)) ge.setParameter("synchronous", false);
        ge.setParameter("synchronous", EditorGUILayout.Toggle("Synchronous",
            (ge.getParameter("synchronous") == null) ? false : (bool)ge.getParameter("synchronous")));

        if ((bool)ge.getParameter("synchronous"))
            EditorGUILayout.HelpBox("Notice that if there is no EventFinished event, the game will stuck.", MessageType.Warning);
    }
}
