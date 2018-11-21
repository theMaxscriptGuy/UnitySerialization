using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;

public class Serialize : MonoBehaviour {

    public string outFileName;
    public string outFilePath;

	// Use this for initialization
	void Start ()
    {
        List<GameObjectInformation> goInfo = new List<GameObjectInformation>();
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
        foreach(GameObject go in objectsInScene)
        {
            GameObjectInformation serializedObject = new GameObjectInformation(go.name);
            Component[] componentsOnObject = go.GetComponents<Component>();
            //foreach(Component comp in componentsOnObject)
            for(int j = 0; j < componentsOnObject.Length;j++)
            {
                SerializedComponent sComp = new SerializedComponent();
                sComp.assemblyName = componentsOnObject[j].GetType().AssemblyQualifiedName;
                sComp.componentData = new List<ComponentInfo>();

                PropertyInfo[] properties = componentsOnObject[j].GetType().GetProperties();
                for(int i = 0; i < properties.Length;i++)
                {
                    try
                    {
                        ComponentInfo cInfo = new ComponentInfo();
                        cInfo.property = properties[i].Name;
                        cInfo.value = componentsOnObject[j].GetType().GetProperty(cInfo.property).GetValue(componentsOnObject[j]).ToString();
                        sComp.componentData.Add(cInfo);
                    }
                    catch
                    {

                    }
                }
                serializedObject.componentArray.Add(sComp);
            }
            goInfo.Add(serializedObject);
        }
        string serializedData = JsonConvert.SerializeObject(goInfo, Formatting.Indented);
        WriteToFile(serializedData);
	}

    void WriteToFile(string data)
    {
        if(string.IsNullOrEmpty(data))
        {
            Debug.LogError("No data found!");
            return;
        }
        if(File.Exists(outFilePath + "\\" + outFileName + ".json"))
        {
            Debug.LogError("File already exists!");
            return;
        }
        StreamWriter fileWriter = new StreamWriter(outFilePath + "\\" + outFileName + ".json", false);
        fileWriter.Write(data);
        fileWriter.Close();
    }

    public void WriteData<T>()
    {

    }
}

public class GameObjectInformation
{
    public string name;
    public List<SerializedComponent> componentArray;

    public GameObjectInformation(string _name)
    {
        name = _name;
        componentArray = new List<SerializedComponent>();
    }
}

public struct ComponentInfo
{
    public string property;
    public string value;
}

public struct SerializedComponent
{
    public string assemblyName;
    public List<ComponentInfo> componentData;
}