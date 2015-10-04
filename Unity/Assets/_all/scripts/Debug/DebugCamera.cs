using UnityEngine;

public class DebugCamera
	: MonoBehaviour
{
    private float sensitivity = 8.0f;
    private float speed = 0.05f;

    float heading = 0.0f;
    float pitch = 0.0f;

    private GameObject follow = null;
    //private float orbitRotate = 0.0f; // TODO
    private float orbitRange = 1.5f;

    void Start()
    {
        transform.position = new Vector3(0.0f, 2.5f, -2.5f);

        DisableMainCamera();
    }

    void DisableMainCamera()
    {
        foreach (var camera in Camera.allCameras)
        {
            if (camera.gameObject != gameObject)
            {
                camera.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (follow == null)
            {
                follow = ScanCursor();
            }
            else
            {
                follow = null;
            }
        }

        if (follow != null)
        {
            UpdateFollow(follow);
            return;
        }

        if (!Input.GetMouseButton(1))
            return;

        float deltaX = Input.GetAxis("Mouse X") * sensitivity;
        float deltaY = Input.GetAxis("Mouse Y") * sensitivity;

        Vector3 mv = Vector3.zero;

        float speed_boost = Input.GetButton("Fire3") ? 2.0f : 1.0f;

        if (Input.GetKey(KeyCode.W)) mv += Vector3.forward * +speed * speed_boost;
        if (Input.GetKey(KeyCode.A)) mv += Vector3.right * -speed * speed_boost;
        if (Input.GetKey(KeyCode.S)) mv += Vector3.forward * -speed * speed_boost;
        if (Input.GetKey(KeyCode.D)) mv += Vector3.right * speed * speed_boost;
        if (Input.GetKey(KeyCode.Q)) mv += Vector3.up * -speed * speed_boost;
        if (Input.GetKey(KeyCode.E)) mv += Vector3.up * speed * speed_boost;

        Vector3 mvt = transform.localToWorldMatrix.MultiplyVector(mv);

        transform.position += mvt;

        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            Strafe(deltaX);
            ChangeHeight(deltaY);
        }
        else
        {
            if (Input.GetMouseButton(0) && false)
            {
                MoveForwards(deltaY);
                ChangeHeading(deltaX);
            }
            else if (Input.GetMouseButton(1))
            {
                ChangeHeading(deltaX);
                ChangePitch(-deltaY);
            }
        }
    }

    void MoveForwards(float aVal)
    {
        Vector3 fwd = transform.forward;
        fwd.y = 0.0f;
        fwd.Normalize();
        transform.position += aVal * fwd;
    }

    void Strafe(float aVal)
    {
        transform.position += aVal * transform.right;
    }

    void ChangeHeight(float aVal)
    {
        transform.position += aVal * Vector3.up;
    }

    void ChangeHeading(float aVal)
    {
        heading += aVal;
        WrapAngle(ref heading);
        transform.localEulerAngles = new Vector3(pitch, heading, 0);
    }

    void ChangePitch(float aVal)
    {
        pitch += aVal;
        WrapAngle(ref pitch);
        transform.localEulerAngles = new Vector3(pitch, heading, 0);
    }

    public static void WrapAngle(ref float angle)
    {
        if (angle < -360.0f)
            angle += 360.0f;
        if (angle > 360.0f)
            angle -= 360.0f;
    }

    void UpdateFollow(GameObject go)
    {
        var ofs = go.transform.position - transform.position;
        var len = Vector3.Magnitude(ofs);
        var dir = Vector3.Normalize(ofs);
        var ideal = go.transform.position + dir * -orbitRange;

        transform.position = Vector3.Lerp(transform.position, ideal, 0.1f);
        transform.LookAt(go.transform, Vector3.up);

        heading = transform.rotation.eulerAngles.z;
        pitch = transform.rotation.eulerAngles.x;

        var mv = V3._000();
        float speed_boost = 1.0f;

        if (Input.GetKey(KeyCode.W)) orbitRange -= 0.1f;
        if (Input.GetKey(KeyCode.S)) orbitRange += 0.1f;

        if (Input.GetKey(KeyCode.A)) mv += transform.right * -speed * speed_boost;
        if (Input.GetKey(KeyCode.D)) mv += transform.right * speed * speed_boost;
        if (Input.GetKey(KeyCode.Q)) mv += transform.up * -speed * speed_boost;
        if (Input.GetKey(KeyCode.E)) mv += transform.up * speed * speed_boost;

        transform.position += mv;

        orbitRange = Mathf.Clamp(orbitRange, 1.0f, 128.0f);
    }

    GameObject ScanCursor()
    {
        var cam = GetComponent<Camera>();
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject;
        }

        return null;
    }
}