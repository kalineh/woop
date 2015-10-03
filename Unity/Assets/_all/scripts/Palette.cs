using UnityEngine;

public class Palette
	: MonoBehaviour
{
    public GameObject Instance;

    public GameObject Spawn()
    {
        var child = Object.Instantiate<GameObject>(Instance);

        return child;
    }
}
