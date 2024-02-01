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
    void Start()
    {
        ChangeGateColor();
        ChangeGateType();
    }
    void ChangeText(GateType gateType)
    {
        gateText.text = gateValue.ToString() + " " + gateType;
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
}
