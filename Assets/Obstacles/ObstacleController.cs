using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerController"))
        {
            var playerController = other.GetComponent<PlayerController>();
            playerController.playerDamageModule.ObstacleDamage();
        }
    }
}
