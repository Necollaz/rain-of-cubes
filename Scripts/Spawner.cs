using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class Spawner : MonoBehaviour
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
        StartCoroutine(SpawnCubeCoroutine());
    }

    public void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
    }

    private void InitializePool()
    {
        _pool = new ObjectPool<Cube>(
            CreateCubeInstance,
            actionOnGet: (cube) => PrepareCube(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private Cube CreateCubeInstance()
    {
        Cube cube = Instantiate(_prefab);
        cube.OnReleased += ReleaseCube;
        return cube;
    }

    private void PrepareCube(Cube cube)
    {
        cube.transform.position = _spawnPoint.transform.position;
        cube.GetComponent<Rigidbody>().velocity = Vector3.down;
        cube.gameObject.SetActive(true);
    }

    private IEnumerator SpawnCubeCoroutine()
    {
        while (true)
        {
            _pool.Get();
            yield return new WaitForSeconds(_repeatRate);
        }
    }
}
