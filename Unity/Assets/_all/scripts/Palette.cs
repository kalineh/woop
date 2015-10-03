using UnityEngine;

public class Palette
	: MonoBehaviour
{
    public GameObject Instance;
    public SfxrParams Params;

    void Start()
    {
        Params = new SfxrParams();
        Params.GeneratePickupCoin();
    }

    public GameObject Spawn()
    {
        var child = Object.Instantiate<GameObject>(Instance);
        var bouncer = child.GetComponent<Bouncer>();

        bouncer.Params = Params;

        return child;
    }
}
