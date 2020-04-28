using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public GameObject turret;

    private TurretConstructor turretConstructor;
    private Color startColor;
    private Renderer materialRenderer;

    private void OnMouseEnter()
    {
        materialRenderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        materialRenderer.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't Build there.");
        }

        turret = turretConstructor.GetTurretToBuild();
        var turretPosition = transform.position;
        turretPosition.y = 0.5f;
        Instantiate(turret, turretPosition, transform.rotation);
    }

    // Start is called before the first frame update
    void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
        startColor = materialRenderer.material.color;
        turretConstructor = FindObjectOfType<TurretConstructor>();
    }
}
