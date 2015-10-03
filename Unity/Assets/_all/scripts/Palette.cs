using UnityEngine;

public class Palette
	: MonoBehaviour
{
    public GameObject Instance;
    public SfxrParams Params = new SfxrParams();
    private string ParamsString;

    void OnDisable()
    {
        ParamsString = Params.GetSettingsString();
    }

    void OnEnable()
    {
        if (ParamsString != null)
        {
            Params.SetSettingsString(ParamsString);
        }
    }


    void Start()
    {
        Params.GeneratePickupCoin();
        ParamsString = Params.GetSettingsString();
    }

    public GameObject Spawn()
    {
        var child = Object.Instantiate<GameObject>(Instance);
        var bouncer = child.GetComponent<Bouncer>();

        bouncer.Synth.parameters = Params;

        return child;
    }
}
