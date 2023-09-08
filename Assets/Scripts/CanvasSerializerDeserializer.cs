using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to serialize canvas heirarchy into json
/// </summary>

[ExecuteInEditMode]
public class CanvasSerializerDeserializer : MonoBehaviour
{
    public TextAsset jsonObject;
    public string fileLocation;
    public List<CanvasObject> canvasObjects;

    public static CanvasSerializerDeserializer instance;

    void Awake()
    {
        if(instance==null)
            instance=this;
    }
    void OnEnable()
    {
        if(instance==null)
            instance=this;
        
        // LoadFromJson();
    }

    public void LoadFromJson()
    {
        string dataAsJson = jsonObject.text;
        // Debug.Log(dataAsJson);
        canvasObjects=JsonConvert.DeserializeObject<List<CanvasObject>>(dataAsJson);
        // Debug.Log(canvasObjects);
        Debug.Log("Loaded json successfully");
    }

    public void InstanstiateFromJson()
    {
        if(canvasObjects!=null)
        {
            var outsideCanvas=Instantiate(Globals.instance.canvasPrefab);
            InstantiateRecursively(outsideCanvas.transform, canvasObjects);
        }
    }

    public void InstantiateRecursively(Transform parentTransform, List<CanvasObject> canvasObjects)
    {
        foreach (var child in canvasObjects)
        {
            var vecRot=child.Transform.Rotation;
            var rotation=Quaternion.Euler(vecRot.x, vecRot.y, vecRot.z);
            
            var emptyObj=new GameObject(child.Name);
            emptyObj.transform.position=child.Transform.Position;
            emptyObj.transform.rotation=rotation;
            emptyObj.transform.localScale=child.Transform.Scale;
            
            emptyObj.transform.SetParent(parentTransform);

            if(child.TextMeshProUGUI.Text!=null && child.TextMeshProUGUI.FontColor!="" && child.TextMeshProUGUI.FontSize!=0)
            {
                Color newCol;
                if (ColorUtility.TryParseHtmlString(child.TextMeshProUGUI.FontColor, out newCol))
                {
                    var textComp=emptyObj.AddComponent<TextMeshProUGUI>();
                    
                    textComp.text=child.TextMeshProUGUI.Text;
                    textComp.color=newCol;
                    textComp.fontSize=child.TextMeshProUGUI.FontSize;
                }
            }

            InstantiateRecursively(emptyObj.transform,child.ChildObjects);
        }
    }

    public T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
}
