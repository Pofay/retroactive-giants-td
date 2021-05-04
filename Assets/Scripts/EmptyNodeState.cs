using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EmptyNodeState : INodeState
{
    public void OnPointerDown(Node context)
    {
        if (context.HasTurretToBuild())
        {
            context.BuildTurret();
            context.MakeMaterialDefault();
            context.SetState(new MountedNodeState());
        }
    }

    public void OnPointerEnter(Node context)
    {
        if(context.HasTurretToBuild())
        {
            context.MakeMaterialGreen();
        }
    }

    public void OnPointerExit(Node context)
    {
        context.MakeMaterialDefault();
    }
}

