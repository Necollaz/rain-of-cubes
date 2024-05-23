using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _minTimeLife = 2.0f;
    [SerializeField] private float _maxTimeLife = 5.0f;

    public void StartDisappearing(Cube cube)
    {
        StartCoroutine(DisappearAfterDelay(cube));
    }

    private IEnumerator DisappearAfterDelay(Cube cube)
    {
        yield return new WaitForSeconds(Random.Range(_minTimeLife, _maxTimeLife));
        cube.ReleaseFromPlatform();
    }
}
