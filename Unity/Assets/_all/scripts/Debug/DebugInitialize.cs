using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DebugInitialize))]
class DebugInitializeEditor : Editor
{
	private object[] debugResources;
	private Dictionary<object, bool> debugResourcesEnable;

	private void CheckLoad()
	{
		if (debugResources != null)
			return;

		debugResources = Resources.LoadAll("Debug");
		debugResourcesEnable = new Dictionary<object, bool>();

		foreach (object o in debugResources)
		{
			debugResourcesEnable.Add(o, true);
		}
	}

	public override void OnInspectorGUI()
	{
		CheckLoad();

		foreach (object o in debugResources)
		{
			GameObject go = o as GameObject;
			debugResourcesEnable[o] = GUILayout.Toggle(debugResourcesEnable[o], go.name);
		}

		var di = target as DebugInitialize;

		foreach (object o in debugResourcesEnable.Keys)
		{
			var go = o as GameObject;
			var enabled = debugResourcesEnable[o];

			di.EditorSetEntry(go.name, enabled);
		}

		EditorUtility.SetDirty(target);
	}
}


public class DebugInitialize : MonoBehaviour
{
	private Dictionary<string, bool> debugObjectEnable = new Dictionary<string, bool>();
	private List<GameObject> debugObjects;

	[SerializeField] private List<string> debugObjectNamesSerialize;
	[SerializeField] private List<bool> debugObjectEnablesSerialize;

	private GameObject Add(string name)
	{
		var resource = Resources.Load("Debug/" + name);
		var object_ = Instantiate(resource, Vector3.zero, Quaternion.identity) as GameObject;

		return object_;
	}

	public void EditorSetEntry(string name, bool enabled)
	{
		if (debugObjectEnable.ContainsKey(name))
		{
			debugObjectEnable[name] = enabled;
			RebuildSerializeData();
			return;
		}

		debugObjectEnable.Add(name, enabled);
		RebuildSerializeData();
	}

	private void RebuildSerializeData()
	{
		debugObjectNamesSerialize = new List<string>();
		debugObjectEnablesSerialize = new List<bool>();

		foreach (KeyValuePair<string, bool> kp in debugObjectEnable)
		{
			debugObjectNamesSerialize.Add(kp.Key);
			debugObjectEnablesSerialize.Add(kp.Value);
		}
	}

	private void InitializeFromSerializeData()
	{
		for (int i = 0; i < debugObjectNamesSerialize.Count; ++i)
		{
			var name = debugObjectNamesSerialize[i];
			var enabled = debugObjectEnablesSerialize[i];

			debugObjectEnable.Add(name, enabled);
		}
	}

	void Start()
	{
		InitializeFromSerializeData();

		debugObjects = new List<GameObject>();

		Debug.LogFormat("DebugInitialize(): {0} types...", debugObjectEnable.Count);

		foreach (string name in debugObjectEnable.Keys)
		{
			var enabled = debugObjectEnable[name];

			Debug.LogFormat("DebugInitialize(): > {0} enabled: {1}", name, enabled);

			if (enabled)
				Add(name);
		}
	}
	
	void Update ()
	{
	}
}
