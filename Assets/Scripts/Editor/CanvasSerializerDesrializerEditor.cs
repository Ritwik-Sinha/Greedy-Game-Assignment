using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// This class is used to create custom inspector for CanvasSerializerDeserializer script
/// </summary>

[CustomEditor(typeof(CanvasSerializerDeserializer))]
[CanEditMultipleObjects]
public class CanvasSerializerDesrializerEditor : Editor 
{
    SerializedProperty jsonObject;
    SerializedProperty fileLocation;
    SerializedProperty canvasObjects;

    private bool displayJsonWarning=false;
    private bool saveJsonWarning=false;

    void OnEnable()
    {
        jsonObject = serializedObject.FindProperty("jsonObject");
        fileLocation = serializedObject.FindProperty("fileLocation");
        canvasObjects = serializedObject.FindProperty("canvasObjects");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Select the Json Asset file :");
        EditorGUILayout.PropertyField(jsonObject);
        EditorGUILayout.Space(5);
    
        if(GUILayout.Button("Load Json"))
            LoadJson();
    
        if(displayJsonWarning)
        {
            GUIStyle style=new GUIStyle();
            style.alignment=TextAnchor.MiddleCenter;
            style.fontSize=15;
            
            EditorGUILayout.Space(10);
            GUILayout.Label("Invalid json file", style);
            Debug.LogWarning("Invalid json file");
            EditorGUILayout.Space(10);
        }
        else
        {
            EditorGUILayout.Space(10);
            
            EditorGUILayout.PropertyField(canvasObjects);

            EditorGUILayout.Space(10);
            if(GUILayout.Button("Instantiate Canvas as above"))
            {
                InstantiateStructure();
            }
            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Enter the file location to save :");
            EditorGUILayout.PropertyField(fileLocation);
            EditorGUILayout.Space(5);

            if(GUILayout.Button("Save Canvas to json"))
            {
                SaveStructureToJson();
            }
            if(saveJsonWarning)
            {
                GUIStyle style=new GUIStyle();
                style.alignment=TextAnchor.MiddleCenter;
                style.fontSize=15;
            
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField("Invalid saving file location", style);
            }
        }
        
        serializedObject.ApplyModifiedProperties();
    }

    public void LoadJson()
    {
        if(CanvasSerializerDeserializer.instance==null) return;
        
        TextAsset file=(TextAsset)jsonObject.GetUnderlyingValue();

        try
        { 
            if(JsonConvert.DeserializeObject(file.text)==null)
                displayJsonWarning=true;
            else
            {
                CanvasSerializerDeserializer.instance.LoadFromJson();
                displayJsonWarning=false;
            }
        }
        catch(Exception e)
        {
            displayJsonWarning=true;
        }
    }
    public void InstantiateStructure()
    {
        if(CanvasSerializerDeserializer.instance==null) return;
        
        CanvasSerializerDeserializer.instance.InstanstiateFromJson();
    }
    public void SaveStructureToJson()
    {
        var path= fileLocation.stringValue;
        var folderPath=path[..path.LastIndexOf('/')];
        if(folderPath.Length!=0)
        {
            var fullFolderPath=Path.Combine( Application.dataPath ,folderPath);
            if(!Directory.Exists(fullFolderPath))
            {
                saveJsonWarning=true;
                Debug.Log("Not Found : "+fullFolderPath);
                return;
            }
        }
        saveJsonWarning=false;
        Debug.Log("Saved Path : "+Path.Combine( Application.dataPath ,path));
        File.WriteAllText(Path.Combine( Application.dataPath ,path), JsonConvert.SerializeObject(CanvasSerializerDeserializer.instance.canvasObjects));
        AssetDatabase.Refresh();
    }
}
