using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Color hoverColor;
    public GameObject turret;

    private TurretConstructor turretConstructor;
    private Color startColor;
    private Renderer materialRenderer;
    private Vector3 positionOffset;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsTurretToBuildNotNull())
        {
            materialRenderer.material.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsTurretToBuildNotNull())
        {
            materialRenderer.material.color = startColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
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

    void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
        startColor = materialRenderer.material.color;
        turretConstructor = FindObjectOfType<TurretConstructor>();
        positionOffset = new Vector3(0, 0.5f, 0);
    }


}
