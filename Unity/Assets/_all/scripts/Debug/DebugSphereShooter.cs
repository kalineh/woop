using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DebugSphereShooter
	: MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            var c = Camera.main;
            var p = c.transform.position + c.transform.forward * 0.25f;
            var v = c.transform.forward * 10.0f;

            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            sphere.transform.localScale = V3._111() * 0.25f;

            sphere.AddComponent<Rigidbody>();
            sphere.AddComponent<SphereCollider>();

            sphere.GetComponent<Rigidbody>().position = p;
            sphere.GetComponent<Rigidbody>().AddForce(v, ForceMode.VelocityChange);

            sphere.tag = "Player";

            StartCoroutine("DestroySphere", sphere);
        }
    }

    IEnumerator DestroySphere(GameObject sphere)
    {
        yield return new WaitForSeconds(5.0f);

        sphere.transform.DOScale(0.0f, 1.0f);

        yield return new WaitForSeconds(1.0f);

        Destroy(sphere);
    }
}
