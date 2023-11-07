using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_bulletSpeed = 10f;
    private Vector3 initialDirection;

    public void SetInitialDirection(Vector3 direction)
    {
        initialDirection = direction;
    }

    void Update()
    {
        transform.Translate(initialDirection * Time.deltaTime * m_bulletSpeed);
    }
}
