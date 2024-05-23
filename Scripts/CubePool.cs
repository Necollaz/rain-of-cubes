using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectPool<Cube> _pool;   

    private void Awake()
    {
        InitializePool();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Issue), 0.0f, _repeatRate);
    }

    private void InitializePool()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = _spawnPoint.transform.position;
        cube.GetComponent<Rigidbody>().velocity = Vector3.down;
        cube.gameObject.SetActive(true);
    }

    public void Issue()
    {
        _pool.Get();
    }

    public void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
    }
}
