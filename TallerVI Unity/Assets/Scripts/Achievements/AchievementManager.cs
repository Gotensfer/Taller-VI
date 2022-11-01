using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class AchievementManager : MonoBehaviour
{
    public Achievement[] trackedAchievements;

    [SerializeField] DistanceTracker_Module distanceTracker_Module;
    [SerializeField] AltitudeTracker_Module altitudeTracker_Module;
    [SerializeField] CollectedCountTracker_Module collectedCountTracker_Module;
    [SerializeField] EventCountTracker_Module eventCountTracker_Module;
    // Tracker de eventos

    [SerializeField] float displayTimeForAwardedAchievement;
    [SerializeField] RectTransform polaroidSpawnPosition;
    [SerializeField] GameObject goToAlbumPolaroid;

    Queue<Achievement> wonAchievementQueue;

    private void Start()
    {
        wonAchievementQueue = new Queue<Achievement>();
    }

    public void CheckAchievements()
    {
        float startTime = Time.realtimeSinceStartup; // Para motivos de debug

       
        int trackedDistance = (int)distanceTracker_Module.travelledDistance;
        int trackedMaxAltitude = (int)altitudeTracker_Module.MaxAltitude;

        int collectedPidgeonsThisRun = collectedCountTracker_Module.pidgeonsThisRun;
        int collectedChillisThisRun = collectedCountTracker_Module.chillisThisRun;
        int collectedRocketsThisRun = collectedCountTracker_Module.rocketsThisRun;

        int bouncesThisRun = eventCountTracker_Module.bouncesCountThisRun;

        int totalLaunchCount = ShenaniganData.launchesCount;
        int totalCrashCount = ShenaniganData.crashesCount;


        int len = trackedAchievements.Length;
        for (int i = 0; i < len; i++)
        {
            // Si ya se ganó el logro, omitir el check e ir al siguiente logro.
            if (PlayerPrefs.GetInt($"{trackedAchievements[i]._name}") == 1) continue;

            switch (trackedAchievements[i]) // No esperaba que esto funcionará sin tanta dificultad -JF
            {
                case DistanceAchievement:
                    // Esto probablemente es una terriiiible idea: 
                    // esto generará un potencial memory leak al realizar esta operación en cada caso -JF
                    DistanceAchievement distanceAchievement = trackedAchievements[i] as DistanceAchievement;
                    
                    if (trackedDistance >= distanceAchievement.distanceNeededForAchievement)
                    {
                        print($"Got achievement {distanceAchievement._name}");
                        // Funcionalidad
                        AwardAchievement(distanceAchievement);
                        EnqueueAchievementWon(distanceAchievement);
                    }

                    break;

                case AltitudeAchievement:
                    AltitudeAchievement altitudeAchievement = trackedAchievements[i] as AltitudeAchievement;

                    if (trackedMaxAltitude >= altitudeAchievement.altitudeNeededForAchievement)
                    {
                        print($"Got achievement {altitudeAchievement._name}");
                        // Funcionalidad
                        AwardAchievement(altitudeAchievement);
                        EnqueueAchievementWon(altitudeAchievement);
                    }

                    break;

                case CollectAchievement:
                    CollectAchievement collectAchievement = trackedAchievements[i] as CollectAchievement;

                    bool gotCollectAchievement = false;
                    switch (collectAchievement.gameCollectType) // Comparar valores dependiendo de que requiere el logro.
                    {
                        case GameCollectType.Chilli:
                            if (collectedChillisThisRun >= collectAchievement.collectsNeededForAchievement) gotCollectAchievement = true;
                            break;
                        case GameCollectType.Pidgeon:
                            if (collectedPidgeonsThisRun >= collectAchievement.collectsNeededForAchievement) gotCollectAchievement = true;
                            break;
                        case GameCollectType.Rocket:
                            if (collectedRocketsThisRun >= collectAchievement.collectsNeededForAchievement) gotCollectAchievement = true;
                            break;
                    }
                     
                    if (gotCollectAchievement) // En caso de obtenerse el logro
                    {
                        print($"Got achievement {collectAchievement._name}");

                        AwardAchievement(collectAchievement);
                        EnqueueAchievementWon(collectAchievement);
                    }

                    break;

                case EventAchievement:
                    EventAchievement eventAchievement = trackedAchievements[i] as EventAchievement;

                    bool gotEventAchievement = false;
                    switch (eventAchievement.gameEventType)
                    {
                        case GameEventType.Launch:
                            if (totalLaunchCount >= eventAchievement.timesNeededForAchievement) gotEventAchievement = true;
                            break;
                        case GameEventType.Bounce:
                            if (bouncesThisRun >= eventAchievement.timesNeededForAchievement) gotEventAchievement = true;
                            break;
                        case GameEventType.Crash:
                            if (totalCrashCount >= eventAchievement.timesNeededForAchievement) gotEventAchievement = true;
                            break;
                    }

                    if (gotEventAchievement) // En caso de obtenerse el logro
                    {
                        print($"Got achievement {eventAchievement._name}");

                        AwardAchievement(eventAchievement);
                        EnqueueAchievementWon(eventAchievement);
                    }

                    break;
            }
        }

        StartCoroutine(DisplayAllAchievements());
        print($"Achievement manager took {Math.Round(startTime - Time.realtimeSinceStartup, 2)}s to finish");
    }


    void AwardAchievement(Achievement achievement)
    {
        PlayerPrefs.SetInt(achievement._name, 1);
    }

    void EnqueueAchievementWon(Achievement achievement)
    {
        wonAchievementQueue.Enqueue(achievement);
    }

    IEnumerator DisplayAllAchievements()
    {
        yield return new WaitForSeconds(1.5f);

        int len = wonAchievementQueue.Count;
        Achievement achievement;
        RectTransform polaroidPrefab;

        for (int i = 0; i < len; i++)
        {
            achievement = wonAchievementQueue.Dequeue();

            Sequence tween = DOTween.Sequence();

            polaroidPrefab = Instantiate(achievement.polaroidPrefabForAchievement, polaroidSpawnPosition.position, polaroidSpawnPosition.rotation, polaroidSpawnPosition).GetComponent<RectTransform>();

            polaroidPrefab.localScale = Vector3.zero;

            tween.Append(polaroidPrefab.DOScale(Vector3.one, displayTimeForAwardedAchievement * (1f / 4f)))
                .AppendInterval(displayTimeForAwardedAchievement * (2.5f / 4f))
                .Append(polaroidPrefab.DOScale(Vector3.zero, displayTimeForAwardedAchievement * (0.5f / 4f)))
                .OnComplete(() => Destroy(polaroidPrefab.gameObject));

            if (i == len - 1)
            {
                RectTransform endPolaroid = Instantiate(goToAlbumPolaroid, polaroidSpawnPosition.position, polaroidSpawnPosition.rotation, polaroidSpawnPosition).GetComponent<RectTransform>();
                endPolaroid.localScale = Vector3.zero;

                tween.Append(endPolaroid.DOScale(Vector3.one, displayTimeForAwardedAchievement * (1f / 4f)));
            }

            yield return new WaitForSeconds(displayTimeForAwardedAchievement + 0.1f);
        }
    }

    /* Codigo de pruebas rapidas
    [SerializeField] Achievement testAchievement;
    [SerializeField] GameObject goToAlbumPolaroid;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RectTransform polaroidPrefab;

            Sequence tween = DOTween.Sequence();

            polaroidPrefab = Instantiate(testAchievement.polaroidPrefab, polaroidSpawnPosition.position, polaroidSpawnPosition.rotation, polaroidSpawnPosition).GetComponent<RectTransform>();

            polaroidPrefab.localScale = Vector3.zero;

            tween.Append(polaroidPrefab.DOScale(Vector3.one, displayTimeForAwardedAchievement * (1f/4f)))
                .AppendInterval(displayTimeForAwardedAchievement * (2.5f/4f))
                .Append(polaroidPrefab.DOScale(Vector3.zero, displayTimeForAwardedAchievement * (0.5f/4f)))
                .OnComplete(() => Destroy(polaroidPrefab.gameObject));

            if (true)
            {
                RectTransform endPolaroid = Instantiate(goToAlbumPolaroid, polaroidSpawnPosition.position, polaroidSpawnPosition.rotation, polaroidSpawnPosition).GetComponent<RectTransform>();
                endPolaroid.localScale = Vector3.zero;

                tween.Append(endPolaroid.DOScale(Vector3.one, displayTimeForAwardedAchievement * (1f / 4f)));
            }
        }
    }
    */

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
