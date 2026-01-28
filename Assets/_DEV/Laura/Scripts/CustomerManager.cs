using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomerManager : MonoBehaviour
{
    // Script that manages customer positions when they first come up to the window. Attach this to a GameObject in the scene,
    // then call functions from said object whenever a customer is added/removed.

    // Reference position for where the frontmost leftmost customer will be.
    [SerializeField] private Vector3 basePosition = Vector3.zero;

    // Reference position for where each customer will be spawned.
    [SerializeField] private Vector3 spawnPosition = Vector3.back * 100;

    // The amount of customers per row.
    private static int ROWCUSTOMERLIMIT = 3;
    private static int TOTALCUSTOMERLIMIT = 10;

    // Horizontal distance between each customer, and longitudal distance between each customer row.
    [SerializeField][Range(0, 5)] private float xBaseDistance;
    [SerializeField][Range(0, 5)] private float zBaseDistance;

    [SerializeField][Range(0, 5)] private float xMinRandomness;
    [SerializeField][Range(0, 5)] private float xMaxRandomness;
    [SerializeField][Range(0, 5)] private float zMinRandomness;
    [SerializeField][Range(0, 5)] private float zMaxRandomness;

    // List of customers, and a list of the positions made for spawned customers.
    private List<GameObject> spawnedCustomers = new();
    private List<Vector3> customerPositions = new();

    // Customer prefab.
    //[SerializeField] private GameObject customerPrefab;
    [SerializeField] private List<GameObject> customerPrefabs;
    private int currentSprite = 0;
    public void SpawnCustomer(BonnetjesManager.Item receipt)
    {
        if (customerPrefabs.Count == 0) return;

        GameObject _customerPrefab = customerPrefabs[currentSprite];

        AddNewCustomer(_customerPrefab);
        spawnedCustomers[^1].transform.GetChild(0).GetComponent<Customer>().receipt = receipt;

        currentSprite = (currentSprite + 1) % customerPrefabs.Count;
    }

    // Call this when you a customer's order is done, and you just want to remove them from the scene.
    public void DeleteCustomer(GameObject customerObject)
    {
        RemoveCustomer(customerObject);
        Destroy(customerObject);
    }

    // Calls DeleteCustomer() on a customer based on the given receipt.
    public void DeleteCustomerByReceipt(BonnetjesManager.Item receipt)
    {
        foreach (GameObject obj in spawnedCustomers)
        {
            if (obj.GetComponent<Customer>().receipt == receipt)
            {
                DeleteCustomer(obj);
            }
        }
    }

    #region -- Customer Position Management --

    private void FixedUpdate()
    {
        // Smoothly moves every customer to their respective waiting position.
        /*
         for (int i = 0; i < spawnedCustomers.Count; i++)
        {
            if (spawnedCustomers[i].transform.position != customerPositions[i])
            {
                spawnedCustomers[i].transform.position = Vector3.Lerp(spawnedCustomers[i].transform.position, customerPositions[i], 10f * Time.deltaTime);
            }
        }
         */
    }

    public bool AddNewCustomer(GameObject customerObject)
    {
        // Adds a created customer to the list of customers to manage. Make a customer object first, then assign it through this.
        if (TOTALCUSTOMERLIMIT > spawnedCustomers.Count)
        {
            GameObject ActualCustomer = Instantiate(customerObject, spawnPosition, Quaternion.identity);
            customerPositions.Add(GetNewCustomerPosition());
            spawnedCustomers.Add(ActualCustomer);

            ActualCustomer.transform.DOMove(customerPositions[customerPositions.Count - 1], 0.5f).SetEase(Ease.OutQuad);

            // If there's room to add a new customer, returns true. Else, returns false.
            return true;
        }
        return false;
    }

    public bool RemoveCustomer(GameObject customerObject)
    {
        // Whenever a customer is done ordering and wants to leave, remove them with this function. Do this BEFORE you destroy
        // their GameObject, otherwise this function won't be able to properly remove them and move the line along.
        // You can also just call DeleteCustomer() to simplify things.
        if (spawnedCustomers.Contains(customerObject))
        {
            int _totalCount = spawnedCustomers.Count;
            int _counter = spawnedCustomers.IndexOf(customerObject);
            while (_counter + ROWCUSTOMERLIMIT < _totalCount)
            {
                spawnedCustomers[_counter] = spawnedCustomers[_counter + ROWCUSTOMERLIMIT];
                _counter += ROWCUSTOMERLIMIT;
            }
            if (_counter % ROWCUSTOMERLIMIT < (float)ROWCUSTOMERLIMIT / 2f)
                spawnedCustomers.RemoveAt(_counter);
            else
            {
                spawnedCustomers[_counter] = spawnedCustomers[^1];
                spawnedCustomers.RemoveAt(spawnedCustomers.Count - 1);
            }
            customerPositions.RemoveAt(customerPositions.Count - 1);

            // Returns whether the customer was removed successfully.
            return true;
        }
        return false;
    }

    private Vector3 GetNewCustomerPosition()
    {
        // Gets a new slightly randomized customer position to assign a customer to.
        int _customerCount = customerPositions.Count;
        Vector3 _position = basePosition;
        while (_customerCount >= ROWCUSTOMERLIMIT)
        {
            _position += Vector3.back * zBaseDistance;
            _customerCount -= ROWCUSTOMERLIMIT;
        }
        _position += _customerCount * xBaseDistance * Vector3.left;
        float xRandModifier = Random.Range(-xMaxRandomness, xMaxRandomness);
        float zRandModifier = Random.Range(-zMaxRandomness, zMaxRandomness);
        if (Mathf.Abs(xRandModifier) < xMinRandomness) xRandModifier *= Mathf.Abs(xMinRandomness / xRandModifier);
        if (Mathf.Abs(zRandModifier) < zMinRandomness) zRandModifier *= Mathf.Abs(zMinRandomness / zRandModifier);
        _position += new Vector3(xRandModifier, 0, zRandModifier);
        return _position;
    }

    #endregion
}
