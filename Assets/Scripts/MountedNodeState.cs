public class MountedNodeState : INodeState
{
    private int numberOfClicks = 0;

    public void OnPointerDown(Node context)
    {
        if (numberOfClicks == 0)
        {
            context.ShowTurretPrompt();
            numberOfClicks++;
        }
        else
        {
            context.HideTurretPrompt();
            numberOfClicks = 0;
        }
    }

    public void OnPointerEnter(Node context)
    {
        context.MakeMaterialGreen();
    }

    public void OnPointerExit(Node context)
    {
        context.MakeMaterialDefault();
    }

    public void SellTurret(Node context)
    {
        context.SetState(new EmptyNodeState());
        context.RefundTurret();
    }

    public void UpgradeTurret(Node context)
    {
        context.ReplaceWithUpgradedVersion();
    }
}
