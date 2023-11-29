using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 directionView = new Vector3(0, (float)3.36, 0);

    void LateUpdate()
    {
        transform.position = player.transform.position + directionView;
    }
}
