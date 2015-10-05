using UnityEngine;

public class Palette
	: MonoBehaviour
{
    public GameObject Instance;
    public string ParamsString = "";

    void Start()
    {
    }

    public GameObject Spawn()
    {
        var child = Object.Instantiate<GameObject>(Instance);
        var bouncer = child.GetComponent<Bouncer>();

        bouncer.Synth.parameters.SetSettingsString(ParamsString);

        return child;
    }
}
