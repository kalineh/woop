using UnityEngine;

public class ApplicationFocusState
	: MonoBehaviour
{
    public static bool Focused = false;

    void OnApplicationFocus(bool focus)
    {
        Focused = true;
    }
}
