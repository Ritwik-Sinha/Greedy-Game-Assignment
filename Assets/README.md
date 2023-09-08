# Unity Object Template Generator

## Introduction
This is a sample project showing the process/method to generate a canvas heirarchy recursively using json file that can be loaded. The loaded json can be modified and saved in another file.

## Process 
The project involves the following
### 1. Loading the json file into the inspector window. 

![1](https://adorable-sprinkles-708361.netlify.app/3.png)

The above example comtains the following properties
* Position
* Rotation
* Scale
* TextMeshPro
    * FontSize
    * FontColor
    * Text

* ChildObjects

It also contains a paramter named "ChildObjects", which nests the other canvas objects which can be read and instantiated recursively into the UGUI.

if an invalid json file is passed, it shows the following error message

![5](https://adorable-sprinkles-708361.netlify.app/5.png)

### 2. Modifying the loaded json file.

![2](https://adorable-sprinkles-708361.netlify.app/1.png)

The above example shows the CanvasSerializerDeserializer.cs script which allows the loading and manipulation of the json object when loaded. The heirarchy postion along with any property value can be modified easily here.

### 3. Instantiating canvas as per the modified loaded json object

![3](https://adorable-sprinkles-708361.netlify.app/2.png)

The above example shows the rendering of the loaded canvas, when the "Instantiate canvas as above" button is clicked from the inspector window.

### 4. Saving canvas by passing the file location

The file location needs to be passed to save the modified canvas. On Writing a error free folder path, the file gets saved into the location, else it shows an invalid path message like below.

![4](https://adorable-sprinkles-708361.netlify.app/4.png)


## Working

The project used serialization and deserializaion technique for achieving the required output.

EditorGUI has been used along with UnityEditor class to generate the custom Inspector for achieving the required results.

The project also used recursive method for generation of Unity UGUI from the loaded json, 


### Thank you.