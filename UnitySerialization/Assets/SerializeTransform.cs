using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class SerializeTransform : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Component comp = gameObject.GetComponent<Transform>();
        PropertyInfo[] props = comp.GetType().GetProperties();
        foreach(PropertyInfo pi in props)
        {
            try
            {
                Debug.Log(pi.Name);
                Debug.Log(pi.GetValue(comp).ToString());
            }
            catch
            {
                Debug.LogError("Could not write : " + pi.Name);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
