using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubePool))]
public class Platform : MonoBehaviour
{
    [SerializeField] private float _minTimeLife = 2.0f;
    [SerializeField] private float _maxTimeLife = 5.0f;
    [SerializeField] private CubePool _cubePool;

    private void Awake()
    {
        _cubePool = GetComponent<CubePool>();
    }

    public void StartDisappearing(Cube cube)
    {
        StartCoroutine(DisappearAfterDelay(cube));
    }

    private IEnumerator DisappearAfterDelay(Cube cube)
    {
        yield return new WaitForSeconds(Random.Range(_minTimeLife, _maxTimeLife));
        _cubePool.ReleaseCube(cube);
    }
}
