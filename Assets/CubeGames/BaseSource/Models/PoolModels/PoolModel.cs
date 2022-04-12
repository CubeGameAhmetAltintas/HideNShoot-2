using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PoolModel : ObjectModel
{
    public List<ObjectModel> Items;
    public int ItemCount;
    public bool GetChildsOnOnInit;

    public override void Initialize()
    {
        if (Items == null)
            Items = new List<ObjectModel>();

        if (GetChildsOnOnInit)
            GetItemsFromChilds();

        ItemCount = Items.Count;

        InitializeItems();

        base.Initialize();
    }

    protected virtual void InitializeItems()
    {
        foreach (var item in Items)
        {
            item.Initialize();
        }
    }

    public virtual ObjectModel GetDeactiveItem()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].gameObject.activeInHierarchy == false)
            {
                return Items[i];
            }
        }

        return null;
    }

    [EditorButton]
    public virtual void SetDeactiveItems()
    {
        foreach (var item in Items)
        {
            item.SetDeactive();
        }
    }

    protected virtual void GetItemsFromChilds()
    {
        if (Items == null)
            Items = new List<ObjectModel>();

        for (int i = 0; i < transform.childCount; i++)
        {
            ObjectModel item = transform.GetChild(i).GetComponent<ObjectModel>();
            if (item != null)
            {
                Items.Add(item);
            }
        }
    }

    [EditorButton]
    public void GetItemsEditor()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "GetItems");
        if (Items != null)
            Items.Clear();

        GetItemsFromChilds();
#endif
    }

    public void SetActiveWithPos(Vector3 pos)
    {
        ObjectModel item = GetDeactiveItem();

        if (item != null)
        {
            item.SetPosition(pos);
            item.SetActive();
        }
    }
}
