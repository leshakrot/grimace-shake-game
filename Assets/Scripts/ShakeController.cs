using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShakeController : MonoBehaviour
{
    public Transform player;
    public float spawnDistance = 50f;
    public GameObject shakePrefab;
    private List<GameObject> shakes = new List<GameObject>();
    public int maxShakes;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (shakes.Count < maxShakes)
        {
            SpawnShake();
        }
        DestroyShake();
    }

    private void SpawnShake()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnDistance;
        randomDirection += player.position;
        randomDirection.y = player.position.y;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, spawnDistance, NavMesh.AllAreas))
        {
            if (!IsPositionVisibleFromCamera(hit.position))
            {
                GameObject shake = Instantiate(shakePrefab, hit.position, Quaternion.identity);
                shakes.Add(shake);
            }
        }
    }

    private bool IsPositionVisibleFromCamera(Vector3 position)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        if (GeometryUtility.TestPlanesAABB(planes, new Bounds(position, Vector3.zero)))
        {
            return true;
        }
        return false;
    }

    public void DestroyShake()
    {
        for (int i = shakes.Count - 1; i >= 0; i--)
        {
            if (shakes[i].gameObject.GetComponent<Shake>().isCollected)
            {
                Destroy(shakes[i]);
                shakes.RemoveAt(i);
            }
        }
    }
}
