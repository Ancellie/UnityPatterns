using System;
using UnityEngine;
using UnityEngine.UI;

public class Plant1 : MonoBehaviour
{
    [SerializeField] private PlantData info;
    SetPlantInfo spi;

    void Start()
    {
        spi = GameObject.FindWithTag("PlantInfo").GetComponent<SetPlantInfo>();
    }

    void OnMouseDown()
    {
        spi.OpenPlantPanel();
        spi.plantName.text = info.Name;
        spi.threatLevel.text = info.Threat.ToString();
        spi.plantIcon.GetComponent<RawImage>().texture = info.Icon;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (info.Threat.ToString() == "High")
        {
            PlayerController1.dead = true;
        }
    }
}
