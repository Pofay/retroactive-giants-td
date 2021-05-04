public class MountedNodeState : INodeState
{
    public void OnPointerDown(Node context)
    {
        // Show sell/upgrade prompt
    }

    public void OnPointerEnter(Node context)
    {
        // Change material to something else
        context.MakeMaterialGreen();
    }

    public void OnPointerExit(Node context)
    {
        // Back to Material Default
        context.MakeMaterialDefault();
    }
}
