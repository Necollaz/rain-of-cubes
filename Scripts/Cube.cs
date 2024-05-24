using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minTimeLife = 2.0f;
    [SerializeField] private float _maxTimeLife = 5.0f;

    private bool _hasTouchedPlatform = false;
    private MeshRenderer _renderer;
    private Rigidbody _rigidbody;
    private Spawner _spawner;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Spawner spawner)
    {
        _spawner = spawner;
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
        StartDisappearing();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform) && !_hasTouchedPlatform)
        {
            _hasTouchedPlatform = true;
            _renderer.material.color = Color.blue;
        }
    }

    private void StartDisappearing()
    {
        StartCoroutine(DisappearAfterDelay());
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(Random.Range(_minTimeLife, _maxTimeLife));
        ReleaseFromPlatform();
    }

    public void ReleaseFromPlatform()
    {
        _spawner?.ReleaseCube(this);
    }
}
