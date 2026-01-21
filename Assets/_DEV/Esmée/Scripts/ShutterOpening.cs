using DG.Tweening;
using UnityEngine;

public class ShutterOpening : MonoBehaviour
{
    [SerializeField] private GameObject door;
    void Start()
    {
        door.transform.DOLocalMoveY(4, 1);
    }


}
