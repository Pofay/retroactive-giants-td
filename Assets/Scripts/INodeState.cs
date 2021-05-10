public interface INodeState
{
    void OnPointerEnter(Node context);
    void OnPointerDown(Node context);
    void OnPointerExit(Node node);
    void SellTurret(Node context);
    void UpgradeTurret(Node context);
}


