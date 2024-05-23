using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private bool _hasTouchedPlatform = false;

    private MeshRenderer _renderer;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _renderer.material.color = Color.red;
    }

    private void OnEnable()
    {
        _hasTouchedPlatform = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _renderer.material.color = Color.red;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Platform platform) && _hasTouchedPlatform == false)
        {
            _hasTouchedPlatform = true;
            platform.StartDisappearing(this);
            _renderer.material.color = Color.blue;
        }
    }
}
