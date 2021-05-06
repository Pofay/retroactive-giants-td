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
        // Change material to something else
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
}
