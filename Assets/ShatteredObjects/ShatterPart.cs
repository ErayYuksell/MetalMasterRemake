using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterPart : MonoBehaviour
{
    [SerializeField] int partSpeed;
    GameObject lavCan;
    public bool canMoveShatter = false;
    void Start()
    {
        lavCan = GameObject.FindGameObjectWithTag("LavCan");
    }


    void Update()
    {
        MoveShattter();
    }

    void MoveShattter()
    {
        if (canMoveShatter)
        {
            transform.position = Vector3.MoveTowards(transform.position, lavCan.transform.position, partSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LavCan"))
        {
            gameObject.SetActive(false);
        }
    }
}
