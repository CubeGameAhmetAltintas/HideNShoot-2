using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBaseModel : ObjectModel
{

    public virtual void ControllerUpdate()
    {

    }


    public virtual void ResetController()
    {

    }

    private void Reset()
    {
        transform.name = GetType().Name;
        transform.ResetLocal();
    }
}