using UnityEngine;
using UnityEditor;
using System.Collections;

public class DebugSceneName : MonoBehaviour
{
	void Start ()
	{
		GetComponent<UnityEngine.UI.Text>().text = EditorApplication.currentScene;
	}
}
