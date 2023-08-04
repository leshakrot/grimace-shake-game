using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class PedestrianController : MonoBehaviour
{
    public GameObject[] pedPrefabs;
    public Transform player;
    public int maxPedestrians = 30;
    public float spawnDistance = 50f;
    public float wanderRadius = 10f;

    private List<GameObject> pedestrians = new List<GameObject>();
    private float timer;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        for (int i = pedestrians.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(pedestrians[i].transform.position, player.position) > spawnDistance)
            {
                Destroy(pedestrians[i]);
                pedestrians.RemoveAt(i);
            }
        }

        for (int i = pedestrians.Count - 1; i >= 0; i--)
        {
            if (pedestrians[i].gameObject.GetComponent<Pedestrian>().isTerrified)
            {
                StartCoroutine(WaitBeforeDestroyPed(pedestrians[i]));
            }
        }

        if (pedestrians.Count < maxPedestrians)
        {
            SpawnPedestrian();
        }
    }

    private void SpawnPedestrian()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnDistance;
        randomDirection += player.position;
        randomDirection.y = player.position.y;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, spawnDistance, NavMesh.AllAreas))
        {
            if (!IsPositionVisibleFromCamera(hit.position))
            {
                GameObject pedestrian = Instantiate(pedPrefabs[Random.Range(0, pedPrefabs.Length)], hit.position, Quaternion.identity);
                pedestrian.GetComponent<Pedestrian>().SetTargetPosition(GetRandomWanderPoint(pedestrian.transform.position));
                pedestrians.Add(pedestrian);
            }
        }
    }

    private Vector3 GetRandomWanderPoint(Vector3 origin)
    {
        Vector3 randomPoint = Random.insideUnitSphere * wanderRadius;
        randomPoint += origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, wanderRadius, NavMesh.AllAreas);
        return hit.position;
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

    IEnumerator WaitBeforeDestroyPed(GameObject pedestrian)
    {
        yield return new WaitForSeconds(2f);
        pedestrians.Remove(pedestrian);
        Destroy(pedestrian);
    }

}
