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
        return turretConstructor.CanBuildTurret();
    }

    public void ShowTurretPrompt()
    {
        var turretPrompt = FindObjectOfType<TurretPromptUI>();
        turretPrompt.Show();
        turretPrompt.TransferPosition(this.transform.position);
    }

    public void HideTurretPrompt()
    {
        var turretPrompt = FindObjectOfType<TurretPromptUI>();
        turretPrompt.Hide();
    }
}
