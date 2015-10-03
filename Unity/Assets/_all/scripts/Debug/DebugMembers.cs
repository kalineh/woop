using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;

public class DebugMembers<T>
	: Editor
{
    bool foldout = true;

	public override void OnInspectorGUI()
	{
        foldout = EditorGUILayout.Foldout(foldout, "Debug.Methods");
        if (!foldout)
        {
            base.OnInspectorGUI();
            return;
        }
 
		var type = typeof(T);
		var members = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var m in members)
        {
            if (Application.isPlaying)
            {
                if (m.Name == "Start")
                    continue;
                if (m.Name == "Update")
                    continue;
                if (m.Name.StartsWith("Editor"))
                    continue;
                if (m.Name.StartsWith("On"))
                    continue;
                if (m.Name.StartsWith("_"))
                    continue;
            }
            else
            {
                if (!m.Name.StartsWith("Editor"))
                    continue;
            }

            if (m.ReturnType != typeof(void) && m.ReturnType != typeof(IEnumerator))
                continue;

            if (m.GetParameters().Length > 0)
                continue;

            if (m.DeclaringType == typeof(T))
			{
				string prefix = m.ReturnType == typeof(IEnumerator) ? "* " : "";
				string s = string.Format("{0}{1}.{2}()", prefix, m.DeclaringType.ToString(), m.Name);
				
				if (Application.isPlaying || true)
				{
					if (GUILayout.Button(s))
					{
						Invoke(m);
					}
				}
			}
		}

        EditorGUILayout.Separator();
		
		base.OnInspectorGUI();
	}
	
	private void Invoke(MethodInfo method)
	{
		if (method.ReturnType == typeof(IEnumerator))
		{
			var go = target as MonoBehaviour;
            go.StopCoroutine(method.Name);
			go.StartCoroutine(method.Name);
		}
		else
		{
			method.Invoke(target, null);
		}
	}
}