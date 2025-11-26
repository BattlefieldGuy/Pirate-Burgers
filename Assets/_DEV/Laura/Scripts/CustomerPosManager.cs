using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.InputAction;

public class CustomerPosManager : MonoBehaviour
{
    // Script that manages customer positions when they first come up to the window.

    // Reference position for where the frontmost leftmost customer will be.
    [SerializeField] private Vector3 basePosition = Vector3.zero;

    // The amount of customers per row.
    private static int ROWCUSTOMERLIMIT = 3;

    // Horizontal distance between each customer, and longitudal distance between each customer row.
    [SerializeField] [Range(0, 5)] private float xBaseDistance;
    [SerializeField] [Range(0, 5)] private float zBaseDistance;

    [SerializeField] [Range(0, 5)] private float xMinRandomness;
    [SerializeField] [Range(0, 5)] private float xMaxRandomness;
    [SerializeField] [Range(0, 5)] private float zMinRandomness;
    [SerializeField] [Range(0, 5)] private float zMaxRandomness;

    // The list of where each customer should go.
    private List<Vector3> customerPositions = new();
    private List<GameObject> spawnedCustomers = new();

    // Testing variable and functions, should be managed by other customer scripts in the final game.
    [SerializeField] private GameObject customerPrefab;
    public void SpawnCustomers(CallbackContext context)
    {
        if (context.performed && !context.canceled)
        {
            AddNewCustomer(Instantiate(customerPrefab, Vector3.back * 100, Quaternion.identity));
        }
    }
    public void DeleteCustomer(CallbackContext context)
    {
        if (context.performed && !context.canceled)
        {
            GameObject tempObject = spawnedCustomers[Random.Range(0, Mathf.Min(3, spawnedCustomers.Count))];
            RemoveCustomer(tempObject);
            Destroy(tempObject);
        }
    }

    private void FixedUpdate()
    {
        // Smoothly moves every customer to their respective waiting position.
        for (int i = 0; i < spawnedCustomers.Count; i++)
        {
            if (spawnedCustomers[i].transform.position != customerPositions[i])
                spawnedCustomers[i].transform.position = Vector3.Lerp(spawnedCustomers[i].transform.position, customerPositions[i], 10f * Time.deltaTime);
        }
    }

    public void AddNewCustomer(GameObject customerObject)
    {
        // Adds a created customer to the list of customers to manage. Make a customer object first, then assign it through this.
        customerPositions.Add(GetNewCustomerPosition());
        spawnedCustomers.Add(customerObject);
    }

    public void RemoveCustomer(GameObject customerObject)
    {
        // Whenever a customer is done ordering and wants to leave, remove them with this script. Do this BEFORE you destroy
        // their GameObject, otherwise this function won't be able to properly remove them and move the line along.
        int _totalCount = spawnedCustomers.Count;
        int _counter = spawnedCustomers.IndexOf(customerObject);
        while(_counter + ROWCUSTOMERLIMIT < _totalCount)
        {
            spawnedCustomers[_counter] = spawnedCustomers[_counter + ROWCUSTOMERLIMIT];
            _counter += ROWCUSTOMERLIMIT;
        }
        if(_counter % ROWCUSTOMERLIMIT < (float)ROWCUSTOMERLIMIT / 2f)
            spawnedCustomers.RemoveAt(_counter);
        else
        {
            spawnedCustomers[_counter] = spawnedCustomers[^1];
            spawnedCustomers.RemoveAt(spawnedCustomers.Count - 1);
        }
        customerPositions.RemoveAt(customerPositions.Count - 1);
    }

    private Vector3 GetNewCustomerPosition()
    {
        // Gets a new slightly randomized customer position to assign a customer to.
        int _customerCount = customerPositions.Count;
        Vector3 _position = basePosition;
        while(_customerCount >= ROWCUSTOMERLIMIT)
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
}
