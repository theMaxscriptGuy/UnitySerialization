using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ClassFromType : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("WRITING A SAMPLE BASE");
        WriteBase(new SampleBase());

        Debug.Log("WRITING A LIGHT SAMPLE BASE");
        WriteBase(new LightSampleBase());

        Component[] components = GameObject.FindObjectsOfType<Component>();
        foreach(Component c in components)
        {
            var comp = System.Activator.CreateInstance(c.GetType());
            WriteBase(comp);
        }
    }

    public void WriteBase<T>(T value)
    {
        PropertyInfo[] props = value.GetType().GetProperties();
        foreach (PropertyInfo pi in props)
        {
            try
            {
                Debug.Log(pi.Name);
                Debug.Log(pi.GetValue(value).ToString());
            }
            catch
            {
                Debug.LogError("Could not write : " + pi.Name);
            }
        }
    }
}


public class SampleBase
{
    public string SampleName { get; set; }
    public int BaseParameter { get; set; }
}

public class LightSampleBase : SampleBase
{
    public string LightName { get; set; }
    public float LightParam { get; set; }
}