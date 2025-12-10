using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using static BonnetjesManager;

public enum SaveableTypes
{
    Ingredient,
    AssembledFood
}

public class SaveManager : MonoBehaviour
{
    /// <summary>
    /// This code is essentially responsible for being able to load and save data so the player can hop in and out without losing progress.
    /// 
    /// At the time of writing this comment, the only thing that is being saved and loaded are ingredients placed in the world.
    /// This'll be expanded as we include more items that require saving
    /// Objects keep their momentum on save so don't toss shit and save right after they'll fling away when loading again lmao
    /// 
    /// THIS DOES NOT YET INCUDE LOADING AND SAVING ASSEMBLED FOODS
    /// </summary>

    [System.Serializable]
    public class SavedIngredient
    {
        public string id;
        public string IngredientType;
        public Vector3 worldPosition;
        public Vector3 worldRotation;
        public Vector3 LinearVelocity;
        public Vector3 AngularVelocity;
    } 


    public static SaveManager Instance;

    [SerializeField] List<SavedIngredient> foodIds = new List<SavedIngredient>();

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

    #region save & load runtime
    public static void AddIngredientID(string id)
    {
        SavedIngredient newIngredient = new SavedIngredient();
        newIngredient.id = id;
        bool AlreadyExists = false;
        foreach(var ing in Instance.foodIds)
        {
            if (ing.id == id)
            {
                AlreadyExists = true;
                break;
            }
        }
        if (AlreadyExists == false)
        {
            Instance.foodIds.Add(newIngredient);
        } else
        {
            Debug.LogWarning("Same object is being added. If this is meant to be overridden, this message can be ignored.");
        }
    }

    public static string CreateUniqueID()
    {
        string newID = System.Guid.NewGuid().ToString();
        bool IsUnique = false;
        while(IsUnique == false)
        {
            IsUnique = true;
            foreach (var ingredient in Instance.foodIds)
            {
                if (ingredient.id == newID)
                {
                    newID = System.Guid.NewGuid().ToString();
                    IsUnique = false;
                    break;
                }
            }
        }
        return newID;
    }

    public static void deleteIngredientID(string id)
    {
        foreach(var ingredient in Instance.foodIds)
        {
            if (ingredient.id == id)
            {
                Instance.foodIds.Remove(ingredient);
                break;
            }
        }
    }

    public Saveable_Ingredient FindIngredientByID(string id)
    {
        Saveable_Ingredient[] allIngredients = Object.FindObjectsByType<Saveable_Ingredient>(FindObjectsSortMode.None);
        foreach (Saveable_Ingredient ingredient in allIngredients)
        {
            if (ingredient.UUID == id)
            {
                return ingredient;
            }
        }
        return null;
    }

    public void LoadIngredients()
    {
        foreach(SavedIngredient ingredient in foodIds)
        {
            Saveable_Ingredient savable = FindIngredientByID(ingredient.id);
            if (savable != null)
            {
                Rigidbody rb = savable.GetComponent<Rigidbody>();
                savable.transform.position = ingredient.worldPosition;
                savable.transform.eulerAngles = ingredient.worldRotation;
                rb.linearVelocity = ingredient.LinearVelocity;
                rb.angularVelocity = ingredient.AngularVelocity;
            } else
            {
                Debug.LogWarning("ID not found in the scene, importing...");
                GameObject requiredPrefab = Resources.Load<GameObject>("ingredients/" + ingredient.IngredientType);
                Rigidbody rb = requiredPrefab.GetComponent<Rigidbody>();
                requiredPrefab.transform.position = ingredient.worldPosition;
                requiredPrefab.transform.eulerAngles = ingredient.worldRotation;
                Saveable_Ingredient script = requiredPrefab.GetComponent<Saveable_Ingredient>();
                script.UUID = ingredient.id;

                GameObject newIngredient = Instantiate(requiredPrefab, ingredient.worldPosition, Quaternion.Euler(ingredient.worldRotation));
                newIngredient.GetComponent<Saveable_Ingredient>().ApplyInstantiatedVelocity(ingredient.LinearVelocity, ingredient.AngularVelocity);
                newIngredient.name = ingredient.IngredientType;
            }
        }
    }

    public void SaveIngredients()
    {
        foreach(SavedIngredient ingredient in foodIds)
        {
            Saveable_Ingredient savable = FindIngredientByID(ingredient.id);
            if (savable != null)
            {
                Rigidbody rb = savable.GetComponent<Rigidbody>();
                ingredient.IngredientType = savable.gameObject.name;
                ingredient.worldPosition = savable.transform.position;
                ingredient.worldRotation = savable.transform.eulerAngles;
                ingredient.LinearVelocity = rb.linearVelocity;
                ingredient.AngularVelocity = rb.angularVelocity;
            } else
            {
                Debug.LogError("Could not find ingredient with ID: " + ingredient.id + " while saving. It may have been deleted.");
            }
        }
    }
    #endregion

    #region Json conversion



    #endregion
}
