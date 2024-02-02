using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GateType { Power, Rate, Range };
public class GateController : MonoBehaviour
{
    public GateType gateType;
    [SerializeField] int gateValue;
    [SerializeField] Material[] materials;
    [SerializeField] TextMeshProUGUI gateText;
    Animator animator;
    [SerializeField] AnimationClip gateHitAnim;
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        ChangeGateColor();
        ChangeGateType();
    }
    private void Update()
    {
        ChangeGateColor();
        ChangeGateType();
    }

    void ChangeGateColor()
    {
        if (gateValue < 0)
        {
            var glass = transform.GetComponentInChildren<Renderer>();
            glass.material = materials[0];
        }
        if (gateValue > 0)
        {
            var glass = transform.GetComponentInChildren<Renderer>();
            glass.material = materials[1];
        }
    }
    void ChangeText(GateType gateType)
    {
        gateText.text = gateValue.ToString() + " " + gateType;
    }

    void ChangeGateType()
    {
        switch (gateType)
        {
            case GateType.Power:
                ChangeText(GateType.Power);
                break;
            case GateType.Rate:
                ChangeText(GateType.Rate);
                break;
            case GateType.Range:
                ChangeText(GateType.Range);
                break;
        }
    }

    public void StartAnim()
    {
        animator.Play(gateHitAnim.name);
    }
    public void IncreaseGateValue(int power)
    {
        gateValue += power;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerController"))
        {
            var playerController = other.GetComponent<PlayerController>();
            playerController.gateModule.GateController(gateType, gateValue);
            gameObject.SetActive(false);
        }
    }
}
