using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public EventModel[] OnTrigs;

    public void Trigger(int eventIndex)
    {
        OnTrigs[eventIndex].Invoke();
    }
}
