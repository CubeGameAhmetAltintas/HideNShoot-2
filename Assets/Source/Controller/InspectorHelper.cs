using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InspectorHelper : MonoBehaviour
{
    [EditorButton]
    public void setRandomX(float min, float max)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 pos = child.localPosition;
            pos.x = Random.Range(min, max);
            child.transform.localPosition = pos;
        }
    }

    [EditorButton]
    public void OrderByZ()
    {
        List<Transform> childs = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Add(transform.GetChild(i));
        }

        childs = childs.OrderBy(x => x.position.z).ToList();

        for (int i = 0; i < childs.Count; i++)
        {
            childs[i].SetSiblingIndex(i);
        }
    }

    [EditorButton]
    public void SetXPosition(float diff)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(i * diff, 0, 0);
        }
    }
}
