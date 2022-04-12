using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CubeEditor.Inspector
{
    public class EditorShowFieldView : FieldAttributeViewModel
    {
        public List<FieldView> RendererField;

        public override void CheckField(FieldInfo field, object target)
        {
            object[] attributeDatas = field.GetCustomAttributes(typeof(ShowField), true);

            if (attributeDatas.Length > 0)
            {
                if (RendererField == null)
                    RendererField = new List<FieldView>();
            }

            for (int j = 0; j < attributeDatas.Length; j++)
            {
                RendererField.Add(new FieldView(field,target));
            }

            base.CheckField(field,target);
        }

        public override void Draw(Object target)
        {
            base.Draw(target);

            if (RendererField == null)
                return;

            for (int i = 0; i < RendererField.Count; i++)
            {
                RendererField[i].Draw(target, true);
            }

            base.Draw(target);

        }
    }
}