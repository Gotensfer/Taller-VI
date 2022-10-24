using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.Events;
using Input = UnityEngine.Input;

public class RealtimeDatabase : MonoBehaviour
{
    public static RealtimeDatabase Instance { get; private set; }
    private DatabaseReference reference;
    public Dictionary<string, float> playerScores { get; private set; }

    public UnityEvent DataLoaded;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            playerScores = new Dictionary<string, float>();
            DataLoaded = new UnityEvent();
        }
    }

    public void SaveData(string user, float score)
    {
        reference.Child("Users").Child(user).Child("Score").SetValueAsync(score).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Data Successfully Added To Database: " + reference);
            }
            else
            {
                Debug.Log("There Was An Error Adding The Data To The Database");
            }
        });
    }

    public void LoadData()
    {
        reference.Child("Users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.ChildrenCount == 0)
                {
                    Debug.Log("No Data To Load");
                }

                playerScores.Clear();

                foreach (DataSnapshot s in snapshot.Children)
                {
                    playerScores.Add(s.Key, float.Parse(s.Child("Score").Value.ToString()));
                }

                Debug.Log("Data Successfully Loaded From Database: " + reference);
                DataLoaded.Invoke();
            }
            else
            {
                Debug.Log("There Was An Error Loading The Data From The Database");
            }
        });
    }
}
