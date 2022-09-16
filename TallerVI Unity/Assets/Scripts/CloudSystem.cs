using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> clouds;
    int targetCloudIndex;

    [SerializeField] float timeBetweenCloudSpawnAttempts;
    float timeLeftForCloudSpawn;

    [SerializeField] float minimumTravelledDistanceForCloudSpawn;
    [Range(0, 1), SerializeField] float CloudSpawnChanceInAttempt;

    [SerializeField] float mininumAltitudeForClouds;
    [SerializeField] float maximumAltitudeForClouds;

    [SerializeField] float x_distanceFromPlayerWhenSpawned;
    [SerializeField] float y_distanceRangeFromPlayerWhenSpawned;

    [SerializeField] Transform player;
    float travelledDistanceSinceLastCloud;

    Vector3 previourPlayerPos;
    Vector3 actualPlayerPos;

    private void Start()
    {
        previourPlayerPos = player.position;
        actualPlayerPos = player.position;
        targetCloudIndex = 0;
    }

    private void Update()
    {
        if (player.position.y < mininumAltitudeForClouds || player.position.y > maximumAltitudeForClouds) return;

        CalculateTravelledDistance();
        TickCloudSpawnTime();
        if (timeLeftForCloudSpawn <= 0)
        {
            timeLeftForCloudSpawn = timeBetweenCloudSpawnAttempts;
            AttemptCloudSpawn();
        }
    }

    void CalculateTravelledDistance()
    {
        actualPlayerPos = player.position;

        travelledDistanceSinceLastCloud += Vector3.Distance(previourPlayerPos, actualPlayerPos);

        previourPlayerPos = player.position;
    }

    void TickCloudSpawnTime()
    {
        timeLeftForCloudSpawn -= Time.deltaTime;
    }

    void AttemptCloudSpawn()
    {
        if (travelledDistanceSinceLastCloud > minimumTravelledDistanceForCloudSpawn)
        {
            if (Random.Range(0, 1) > CloudSpawnChanceInAttempt)
            {
                clouds[targetCloudIndex].transform.position = player.position + SpawnCloudPositionOffset();
                targetCloudIndex++;

                if (targetCloudIndex >= clouds.Count)
                {
                    targetCloudIndex = 0;
                }               
            }
        }
    }

    Vector3 SpawnCloudPositionOffset()
    {
        return new Vector3(x_distanceFromPlayerWhenSpawned,
            Random.Range(-y_distanceRangeFromPlayerWhenSpawned, y_distanceRangeFromPlayerWhenSpawned), 0);
    }
}
