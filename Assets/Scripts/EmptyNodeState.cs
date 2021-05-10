using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EmptyNodeState : INodeState
{
    public void OnPointerDown(Node context)
    {
        if (context.CanBuildTurret())
        {
            context.BuildTurret();
            context.MakeMaterialDefault();
            context.SetState(new MountedNodeState());
        }
    }

    public void OnPointerEnter(Node context)
    {
        if (context.CanBuildTurret())
        {
            context.MakeMaterialGreen();
        }
    }

    public void OnPointerExit(Node context)
    {
        context.MakeMaterialDefault();
    }

    public void SellTurret(Node context)
    {
    }

    public void UpgradeTurret(Node context)
    {
    }
}

