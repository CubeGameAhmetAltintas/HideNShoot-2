using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerBaseModel : ObjectModel
{
    public virtual void ResetHandler()
    {

    }


    private void Reset()
    {
        transform.localPosition = Vector3.zero;
        transform.name = GetType().Name;
    }
}