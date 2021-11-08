using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Color hoverColor;
    public GameObject mountedTurretGO;

    private Renderer materialRenderer;
    private TurretConstructor turretConstructor;
    private Color startColor;
    private Vector3 positionOffset;
    private INodeState currentState;
    private TurretPromptUI turretPrompt;

    void Awake()
    {
        currentState = new EmptyNodeState();
        materialRenderer = GetComponent<Renderer>();
        startColor = materialRenderer.material.color;
        turretConstructor = FindObjectOfType<TurretConstructor>();
        positionOffset = new Vector3(0, 0.5f, 0);
        turretPrompt = FindObjectOfType<TurretPromptUI>();
    }

    #region State forwarding functions
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

    public void SellTurret()
    {
        currentState.SellTurret(this);
        HideTurretPrompt();
    }

    public void UpgradeTurret()
    {
        currentState.UpgradeTurret(this);
    }
    #endregion

    public void BuildTurret()
    {
        turretConstructor.BuildTurret(this, positionOffset);
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

    public bool CanBuildTurret()
    {
        if (turretConstructor != null)
        {
            return turretConstructor.CanBuildTurret();
        }
        else
        {
            return false;
        }
    }

    public void ShowTurretPrompt()
    {
        turretPrompt.Show(mountedTurretGO.GetComponent<Turret>());
        turretPrompt.TransferPosition(this.transform.position);
        turretPrompt.AttachButtonEvents(this);
    }

    public void HideTurretPrompt()
    {
        turretPrompt.DetachButtonEvents();
        turretPrompt.Hide();
    }

    public void ReplaceWithUpgradedVersion()
    {
        var turret = mountedTurretGO.GetComponent<Turret>();
        if (turret.upgradedVersion != null)
        {
            if (turretConstructor.CanBuildUpgradedTurret(turret))
            {
                turretConstructor.BuildUpgradedTurret(this, positionOffset);
                HideTurretPrompt();
            }
        }
    }

    public void RefundTurret()
    {
        turretConstructor.RefundTurret(mountedTurretGO.GetComponent<Turret>());
    }
}
