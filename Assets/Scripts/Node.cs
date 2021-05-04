using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Color hoverColor;
    public GameObject turret;

    private Renderer materialRenderer;
    private TurretConstructor turretConstructor;
    private Color startColor;
    private Vector3 positionOffset;
    private INodeState currentState;

    void Awake()
    {
        currentState = new EmptyNodeState();
        materialRenderer = GetComponent<Renderer>();
        startColor = materialRenderer.material.color;
        turretConstructor = FindObjectOfType<TurretConstructor>();
        positionOffset = new Vector3(0, 0.5f, 0);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        currentState.OnPointerEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentState.OnPointerExit(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentState.OnPointerDown(this);
    }

    public void BuildTurret()
    {
        var turret = turretConstructor.GetTurretToBuild();
        var turretPosition = transform.position + positionOffset;
        Instantiate(turret, turretPosition, transform.rotation);
    }

    public void MakeMaterialDefault()
    {
        materialRenderer.material.color = startColor;
    }

    public void MakeMaterialGreen()
    {
        materialRenderer.material.color = hoverColor;
    }

    public void SetState(INodeState state)
    {
        currentState = state;
    }

    public bool HasTurretToBuild()
    {
        return turretConstructor.GetTurretToBuild() != null;
    }
}
