using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectPool<Cube> _pool;   

    public static CubePool Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        InitializePool();
    }

    private void Start()
    {
        StartCoroutine(IssueCubeCoroutine());
    }

    public IEnumerator IssueCubeCoroutine()
    {
        while (true)
        {
            _pool.Get();
            yield return new WaitForSeconds(_repeatRate);
        }
    }

    public void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
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
}
