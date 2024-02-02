using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementModule movementModule;
    public FireModule fireModule;
    public GateModule gateModule;

    void Start()
    {
        movementModule.Init(this);
        fireModule.Init(this);
        gateModule.Init(this);
        StartCoroutine(fireModule.FireSystem());
    }


    void Update()
    {
        movementModule.PlayerMovement();

    }


    [Serializable]
    public class MovementModule
    {
        PlayerController playerController;
        [SerializeField] float playerSpeed = 5f;
        float xSpeed;
        float maxXValue = -.5f;
        float RmaxXValue = -10f;
        bool canMove = true;
        public void Init(PlayerController playerController)
        {
            this.playerController = playerController;
        }
        public void PlayerMovement()
        {
            if (!canMove)
            {
                return;
            }
            float touchX = 0;
            float newXValue;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                xSpeed = 250f;
                touchX = Input.GetTouch(0).deltaPosition.x / Screen.width;
            }
            else if (Input.GetMouseButton(0))
            {
                xSpeed = 350f;
                touchX = Input.GetAxis("Mouse X");
            }
            newXValue = playerController.transform.position.x + xSpeed * touchX * Time.deltaTime;
            newXValue = Mathf.Clamp(newXValue, RmaxXValue, maxXValue);
            Vector3 playerNewPosition = new Vector3(newXValue, playerController.transform.position.y, playerController.transform.position.z + playerSpeed * Time.deltaTime);
            playerController.transform.position = playerNewPosition;

        }
    }

    [Serializable]
    public class FireModule
    {
        PlayerController playerController;
        [SerializeField] ObjectPool ObjectPool;
        public Transform firePoint;
        public float rate = 0.1f;
        public float power = 1;
        public float range = 15;
        public void Init(PlayerController playerController)
        {
            this.playerController = playerController;
        }
        public IEnumerator FireSystem()
        {
            while (true)
            {
                var obj = ObjectPool.GetObjectPool();
                obj.transform.position = firePoint.position;

                yield return new WaitForSeconds(rate);
            }
        }
    }

    [Serializable]
    public class GateModule
    {
        PlayerController playerController;
        [SerializeField] float powerMultiply = 0.001f;
        [SerializeField] float rangeMultiply = 0.001f;
        [SerializeField] float rateMultiply = 0.001f;
        public void Init(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void GateController(GateType gateType, int gateValue)
        {
            switch (gateType)
            {
                case GateType.Power:
                    playerController.fireModule.power += gateValue * powerMultiply;
                    break;
                case GateType.Range:
                    playerController.fireModule.range += gateValue * rangeMultiply;
                    break;
                case GateType.Rate:
                    playerController.fireModule.rate += gateValue * rateMultiply;
                    break;
            }
        }

    }
}
