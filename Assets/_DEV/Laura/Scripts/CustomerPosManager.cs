using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerPosManager : MonoBehaviour
{
    // Script that manages customer positions when they first come up to the window.

    // Reference position for where the frontmost center customer will be.
    [SerializeField] private Vector3 basePosition = Vector3.zero;

    private static int ROWCUSTOMERLIMIT = 3;

    // Horizontal distance between each customer, and longitudal distance between each customer row.
    [SerializeField] private float xBaseDistance;
    [SerializeField] private float zBaseDistance;

    [SerializeField] private float xMinRandomness;
    [SerializeField] private float xMaxRandomness;
    [SerializeField] private float zMinRandomness;
    [SerializeField] private float zMaxRandomness;

    public List<Vector3> CustomerPositions = new();

    public Vector3 AddNewCustomer()
    {
        CustomerPositions.Add(GetNewCustomerPosition());
        return CustomerPositions[^1];
    }

    public void RemoveCustomer(int index = 0)
    {
        CustomerPositions.RemoveAt(index);
    }

    private Vector3 GetNewCustomerPosition()
    {
        int _customerCount = CustomerPositions.Count;
        Vector3 _position = basePosition;
        while(_customerCount > ROWCUSTOMERLIMIT)
        {
            _position += Vector3.back * zBaseDistance;
            _customerCount -= ROWCUSTOMERLIMIT;
        }
        if (_customerCount == 1) _position += Vector3.left * xBaseDistance;
        if (_customerCount == 2) _position += Vector3.right * xBaseDistance;
        _position += new Vector3(Random.Range(xMinRandomness, xMaxRandomness), 0, Random.Range(zMinRandomness, xMaxRandomness));
        return _position;
    }
}
