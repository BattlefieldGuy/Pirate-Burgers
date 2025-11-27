using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    /// <summary>
    /// This code is essentially responsible for being able to load and save data so the player can hop in and out without losing progress.
    /// 
    /// At the time of writing this comment, the only thing that is being saved and loaded are ingredients placed in the world.
    /// This'll be expanded as we include more items that require saving
    /// </summary>

    public static SaveManager Instance;

    [SerializeField] List<string> foodIds = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static string CreateUniqueID()
    {
        string newID = System.Guid.NewGuid().ToString();
        while (Instance.foodIds.Contains(newID))
        {
            newID = System.Guid.NewGuid().ToString();
        }
        Instance.foodIds.Add(newID);
        return newID;
    }

    public void SaveIngredients()
    {

    }
}
