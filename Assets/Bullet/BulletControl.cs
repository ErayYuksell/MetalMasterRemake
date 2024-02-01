using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 0.1f;
    Transform firePoint;
    GameObject playerControllerObject;
    PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerObject = GameObject.FindWithTag("PlayerController");
        playerControllerScript = playerControllerObject.GetComponent<PlayerController>();
        firePoint = playerControllerScript.fireModule.firePoint;
    }
    private void Update()
    {
        BulletMovement();
        BulletDistance();
    }
    void BulletMovement()
    {
        transform.position += Vector3.forward * bulletSpeed;
    }
    public void BulletDistance()
    {
        float distance = Vector3.Distance(firePoint.transform.position,transform.position);

        if (distance > 15)
        {
            gameObject.SetActive(false);
        }
    }
}
