﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public GameObject turret;

    private TurretConstructor turretConstructor;
    private Color startColor;
    private Renderer materialRenderer;
    private Vector3 positionOffset;

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (IsTurretToBuildNotNull())
        {
            materialRenderer.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (IsTurretToBuildNotNull())
        {
            materialRenderer.material.color = startColor;
        }
    }

    private void OnMouseDown()
    {
        if (IsTurretBuiltAlready())
        {
            Debug.Log("Can't Build there.");
        }
        else
        {
            if (IsTurretToBuildNotNull())
            {
                turret = turretConstructor.GetTurretToBuild();
                Build(turret);
            }
        }
    }

    private bool IsTurretToBuildNotNull()
    {
        return turretConstructor.GetTurretToBuild() != null;
    }

    private bool IsTurretBuiltAlready()
    {
        return turret != null;
    }

    private void Build(GameObject turrret)
    {
        var turretPosition = transform.position + positionOffset;
        Instantiate(turret, turretPosition, transform.rotation);
    }

    // Start is called before the first frame update
    void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
        startColor = materialRenderer.material.color;
        turretConstructor = FindObjectOfType<TurretConstructor>();
        positionOffset = new Vector3(0, 0.5f, 0);
    }
}
