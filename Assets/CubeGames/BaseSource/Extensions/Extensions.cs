using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public static class Extensions
{
    #region Properties

    public static string Insert(this string target, string inserAfterThis, string value, bool checkIsExist)
    {
        if (checkIsExist)
        {
            if (target.Contains(value))
            {
                return target;
            }
        }

        int index = target.IndexOf(inserAfterThis);
        target = target.Insert(index + 1, value);
        return target;
    }

    public static string Insert(this string target, string searhWord, char afterThisChar, string value, bool checkIsExist)
    {
        if (checkIsExist)
        {
            if (target.Contains(value))
            {
                return target;
            }
        }

        int index = target.IndexOf(searhWord);
        int finalIndex = 0;
        for (int i = index; i < target.Length; i++)
        {
            char val = target[i];
            if (val == afterThisChar)
            {
                finalIndex = i + 1;
                break;
            }
        }

        target = target.Insert(finalIndex, value);
        return target;
    }

    public static string Replace(this string target, string searhWord, char lastChar, string value)
    {
        int index = target.IndexOf(searhWord) + searhWord.Length;
        string getValue = searhWord;
        for (int i = index; i < target.Length; i++)
        {
            getValue += target[i];
            if (target[i] == lastChar)
            {
                break;
            }
        }

        target = target.Replace(getValue, value);
        return target;
    }

    public static string Get(this string target, string searhWord, char lastChar)
    {
        int index = target.IndexOf(searhWord) + searhWord.Length;
        string getValue = searhWord;
        for (int i = index; i < target.Length; i++)
        {
            getValue += target[i];
            if (target[i] == lastChar)
            {
                break;
            }
        }
        return getValue;
    }

    public static string ToSingle(this string[] value, char divider)
    {
        string strValue = "";
        for (int i = 0; i < value.Length; i++)
        {
            strValue += value[i] + (i < value.Length - 1 ? divider.ToString() : "");
        }

        return strValue;
    }

    public static string ToSingle(this List<string> value, char divider)
    {
        string strValue = "";
        for (int i = 0; i < value.Count; i++)
        {
            strValue += value[i] + (i < value.Count - 1 ? divider.ToString() : "");
        }

        return strValue;
    }

    public static List<string> SplitList(this string value, params char[] seperator)
    {
        return value.Split(seperator).ToList();
    }

    public static float GetRandom(this float value, float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static int GetRandom(this int value, int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static long Lerp(this long value, long target, float t)
    {
        float val = 1 - t;

        if (value < target)
        {
            if (value >= target * val)
            {
                return target;
            }
        }
        else
        {
            if (value <= target * val)
            {
                return target;
            }
        }

        return (long)((1 - t) * (float)value + t * (float)target);
    }

    public static int Lerp(this int value, int target, float t)
    {
        float val = 1 - t;

        if (value < target)
        {
            if (value >= target * val)
            {
                return target;
            }
        }
        else
        {
            if (value <= target * val)
            {
                return target;
            }
        }

        return (int)((1 - t) * (float)value + t * (float)target);
    }

    public static float Lerp(this float value, float target, float t)
    {
        return Mathf.Lerp(value, target, t);
    }


    public static float ToFloat(this string value)
    {
        float val = 0;
        float.TryParse(value, out val);
        return val;
    }

    public static int ToInt(this string value)
    {
        int val = 0;
        int.TryParse(value, out val);
        return val;
    }

    public static string ToCoinValues(this int value)
    {
        if (value > 999999999 || value < -999999999)
        {
            return value.ToString("0,,,.###B", System.Globalization.CultureInfo.InvariantCulture);
        }
        else if (value > 999999 || value < -999999)
        {
            return value.ToString("0,,.##M", System.Globalization.CultureInfo.InvariantCulture);
        }
        else
        {
            return value.ToString("n0");
        }
    }

    public static string ToCoinValues(this long value)
    {
        if (value > 999999999 || value < -999999999)
        {
            return value.ToString("0,,,.###B", System.Globalization.CultureInfo.InvariantCulture);
        }
        else if (value > 999999 || value < -999999)
        {
            return value.ToString("0,,.##M", System.Globalization.CultureInfo.InvariantCulture);
        }
        else
        {
            return value.ToString("n0");
        }
    }

    public static string FirstUpperCase(this string val)
    {
        char chr = char.ToUpper(val[0]);
        val = val.Remove(0, 1);
        val = chr + val;
        return val;
    }

    #endregion

    #region Unity Properties

    public static Texture2D ToTexture2D(this Texture texture)
    {
        return Texture2D.CreateExternalTexture(
            texture.width,
            texture.height,
           TextureFormat.RGBA32,
            false, false,
            texture.GetNativeTexturePtr());
    }

    public static Color ToInvert(this Color color)
    {
        return Helpers.Colors.InvertColor(color);
    }

    public static Color32 ToInvert(this Color32 color)
    {
        return Helpers.Colors.InvertColor(color);
    }

    public static float FillImage(this Image target, float value, float maxValue)
    {
        target.fillAmount = value / maxValue;
        return value / maxValue;
    }

    public static float GetRandom(this Vector2 val)
    {
        return UnityEngine.Random.Range(val.x, val.y);
    }

    public static bool HasComponent<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponent<T>() != null;
    }

    public static bool HasComponent<T>(this Component col) where T : Component
    {
        return col.gameObject.GetComponent<T>() != null;
    }

    public static bool HasComponent<T>(this Collision col) where T : Component
    {
        return col.gameObject.GetComponent<T>() != null;
    }

    public static bool HasComponent<T>(this RaycastHit hit) where T : Component
    {
        return hit.transform.GetComponent<T>() != null;
    }

    public static T GetComponent<T>(this Collision col) where T : Component
    {
        return col.gameObject.GetComponent<T>();
    }

    public static void SetActive(this Component comp, bool statue)
    {
        comp.gameObject.SetActive(statue);
    }

    public static Transform CopyTransform(this Transform obj, Transform target)
    {
        obj.position = target.position;
        obj.rotation = target.rotation;
        return obj;
    }

    public static Vector3 ToV3(this Vector2 value, float z)
    {
        return new Vector3(value.x, value.y, z);
    }

    public static Vector3 ToV3XZ(this Vector2 value, float y)
    {
        return new Vector3(value.x, y, value.y);
    }

    public static Transform ResetLocal(this Transform target)
    {
        target.localPosition = Vector3.zero;
        target.localRotation = Quaternion.identity;
        return target;
    }

    public static Vector2 GetXZ(this Vector3 value)
    {
        return new Vector2(value.x, value.z);
    }

    public static Vector2 GetXY(this Vector3 value)
    {
        return new Vector2(value.x, value.y);
    }

    public static Vector2 GetYZ(this Vector3 value)
    {
        return new Vector2(value.y, value.z);
    }

    public static Text SetText(this Button btn, string value)
    {
        if (btn.GetComponentInChildren<Text>() != null)
        {
            btn.GetComponentInChildren<Text>().text = value;
            return btn.GetComponentInChildren<Text>();
        }
        else
        {
            if (btn.GetComponent<Text>() != null)
            {
                btn.GetComponent<Text>().text = value;
                return btn.GetComponent<Text>();
            }
            else
            {
                return null;
            }
        }
    }

    public static Image SetSprite(this Button btn, Sprite value)
    {
        if (btn.transform.GetChild(0).GetComponent<Image>() != null)
        {
            btn.transform.GetChild(0).GetComponent<Image>().sprite = value;
            return btn.transform.GetChild(0).GetComponent<Image>();
        }
        else
        {
            if (btn.GetComponent<Image>() != null)
            {
                btn.GetComponent<Image>().sprite = value;
                return btn.GetComponent<Image>();
            }
            else
            {
                return null;
            }
        }
    }

    public static string GetText(this Button btn)
    {
        if (btn.GetComponentInChildren<Text>() != null)
        {
            return btn.GetComponentInChildren<Text>().text;
        }
        else
        {
            if (btn.GetComponent<Text>() != null)
            {
                return btn.GetComponent<Text>().text;
            }
            else
            {
                return "";
            }
        }
    }

    public static Vector3 IsNan(this Vector3 val, Vector3 defaultValue)
    {
        if (float.IsNaN(val.x) || float.IsInfinity(val.x))
        {
            val.x = defaultValue.x;
        }

        if (float.IsNaN(val.y) || float.IsInfinity(val.y))
        {
            val.y = defaultValue.y;
        }

        if (float.IsNaN(val.z) || float.IsInfinity(val.z))
        {
            val.z = defaultValue.z;
        }

        return val;
    }

    public static Transform LerpRotation(this Transform target, Quaternion targetRot, float t, bool lockX, bool lockY,
        bool lockZ)
    {
        Vector3 targetEuler = targetRot.eulerAngles;
        if (lockX)
            targetEuler.x = target.localEulerAngles.x;

        if (lockY)
            targetEuler.y = target.localEulerAngles.y;

        if (lockZ)
            targetEuler.z = target.localEulerAngles.z;


        target.rotation = Quaternion.Lerp(target.rotation, Quaternion.Euler(targetEuler), t);
        return target;
    }

    public static Transform MoveLerp(this Transform target, Transform point, float t)
    {
        target.position = Vector3.Lerp(target.position, point.position, t);
        target.rotation = Quaternion.Lerp(target.rotation, point.rotation, t);
        return target;
    }

    public static Transform MoveLerp(this Transform target, Vector3 position, Quaternion rotation, float t)
    {
        target.position = Vector3.Lerp(target.position, position, t);
        target.rotation = Quaternion.Lerp(target.rotation, rotation, t);
        return target;
    }

    public static Transform MoveLerp(this Transform target, Vector3 position, Vector3 rotation, float t)
    {
        target.position = Vector3.Lerp(target.position, position, t);
        target.rotation = Quaternion.Lerp(target.rotation, Quaternion.Euler(rotation), t);
        return target;
    }

    public static Transform MoveToward(this Transform target, Transform point, float t)
    {
        target.position = Vector3.MoveTowards(target.position, point.position, t);
        target.rotation = Quaternion.Lerp(target.rotation, point.rotation, t);
        return target;
    }

    public static Transform MoveToward(this Transform target, Vector3 position, Quaternion rotation, float t)
    {
        target.position = Vector3.MoveTowards(target.position, position, t);
        target.rotation = Quaternion.Lerp(target.rotation, rotation, t);
        return target;
    }

    public static Transform MoveToward(this Transform target, Vector3 position, Vector3 rotation, float t)
    {
        target.position = Vector3.MoveTowards(target.position, position, t);
        target.rotation = Quaternion.Lerp(target.rotation, Quaternion.Euler(rotation), t);
        return target;
    }

    public static Vector3 SetRandom(this Vector3 target, Vector3 min, Vector3 max)
    {
        target = new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y),
            UnityEngine.Random.Range(min.z, max.z));
        return target;
    }

    public static Quaternion RandomRotation(this Quaternion target, Vector3 min, Vector3 max)
    {
        target = Quaternion.Euler(new Vector3().SetRandom(min, max));
        return target;
    }

    #endregion

    #region Collections

    public static T AddItem<T>(this List<T> list, T item)
    {
        if (list == null)
            list = new List<T>();

        list.Add(item);

        return item;
    }

    public static T GetLastItem<T>(this List<T> list)
    {
        return list[list.Count - 1];
    }

    public static T GetLastItem<T>(this T[] list)
    {
        return list[list.Length - 1];
    }

    public static List<T> CreateOrClear<T>(this List<T> list)
    {
        if (list == null)
        {
            list = new List<T>();
        }
        else
        {
            list.Clear();
        }

        return list;
    }

    public static List<T> AddWithNullCheck<T>(this List<T> list, T item)
    {
        if (list == null)
        {
            list = new List<T>();
        }

        list.Add(item);

        return list;
    }

    public static List<T> AddWithNullCheck<T>(this List<T> list, List<T> items)
    {
        if (list == null)
        {
            list = new List<T>();
        }

        list.AddRange(items);

        return list;
    }

    public static List<T> AddWithNullCheck<T>(this List<T> list, T[] items)
    {
        if (list == null)
        {
            list = new List<T>();
        }

        list.AddRange(items);

        return list;
    }

    public static List<T> DoWithCondition<T>(this List<T> targetList, Func<T, bool> condition, Action<T> action)
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            if (condition.Invoke(targetList[i]))
            {
                action.Invoke(targetList[i]);
            }
        }

        return targetList;
    }

    public static T[] DoWithCondition<T>(this T[] targetList, Func<T, bool> condition, Action<T> action)
    {
        for (int i = 0; i < targetList.Length; i++)
        {
            if (condition.Invoke(targetList[i]))
            {
                action.Invoke(targetList[i]);
            }
        }

        return targetList;
    }

    public static T Find<T>(this T[] array, Predicate<T> match)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (match(array[i]))
            {
                return array[i];
            }
        }

        return default;
    }

    public static List<T> FindAll<T>(this T[] array, Predicate<T> match)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < array.Length; i++)
        {
            if (match(array[i]))
            {
                list.Add(array[i]);
            }
        }

        return list;
    }

    public static T GetRandom<T>(this IEnumerable<T> values)
    {
        return values.ElementAt(UnityEngine.Random.Range(0, values.Count()));
    }

    public static T GetRandomWithLuck<T>(this List<T> values, List<float> lucks)
    {
        int index = (int)Helpers.Maths.GetItemByLuck(lucks);
        return values[index];
    }

    public static T GetRandomWithLuck<T>(this List<T> values, float[] lucks)
    {
        int index = (int)Helpers.Maths.GetItemByLuck(lucks);
        return values[index];
    }

    public static T GetRandomWithCondition<T>(this List<T> list, Func<T, bool> condition)
    {
        return list.Where(condition).GetRandom();
    }

    public static T GetRandomWithCondition<T>(this T[] list, Func<T, bool> condition)
    {
        return list.Where(condition).GetRandom();
    }

    public static T GetIndexOrDefault<T>(this List<T> values, int index)
    {
        if (index < values.Count)
        {
            return values[index];
        }
        else
        {
            return default;
        }
    }

    public static T DoWithCheckNull<T>(this T value, Action<T> action)
    {
        if (value != null)
        {
            action.Invoke(value);
        }

        return value;
    }

    public static List<List<T>> GroupByCount<T>(this List<T> list, int count)
    {
        List<List<T>> newList = new List<List<T>>();
        List<T> a = new List<T>();
        int index = 0;
        for (int i = 0; i < list.Count; i++)
        {
            a.Add(list[i]);

            index++;
            if (index == count - 1)
            {
                index = 0;
                newList.Add(a);
                a = new List<T>();
            }
        }

        return newList;
    }

    public static T GetRandom<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T GetRandom<T>(this T[] list)
    {
        return list[UnityEngine.Random.Range(0, list.Length)];
    }

    public static int GetRandomIndex<T>(this T[] list)
    {
        return UnityEngine.Random.Range(0, list.Length);
    }

    public static int GetRandomIndex<T>(this List<T> list)
    {
        return UnityEngine.Random.Range(0, list.Count);
    }

    public static T[] GetRandoms<T>(this T[] list, int count)
    {
        T[] arrays = new T[count];
        System.Random r = new System.Random();
        int diff = list.Length / count;
        int startIndex = 0;

        for (int i = 0; i < count; i++)
        {
            int endIndex = (i * diff) + diff;
            int index = UnityEngine.Random.Range(startIndex, endIndex);
            arrays[i] = list[index];
            startIndex = endIndex;
        }

        return arrays;
    }

    public static T[] ShuffleArray<T>(this T[] list)
    {
        System.Random r = new System.Random();
        for (int i = 0; i < list.Length; i++)
        {
            var obj = list[i];
            int index = r.Next(0, list.Length);
            var randomObj = list[index];
            list[index] = obj;
            list[i] = randomObj;
        }

        return list;
    }

    public static List<T> ShuffleList<T>(this List<T> list)
    {
        System.Random r = new System.Random();
        for (int i = 0; i < list.Count; i++)
        {
            var obj = list[i];
            list.Remove(obj);
            int index = r.Next(0, list.Count);
            list.Insert(index, obj);
        }

        return list;
    }

    public static List<T> SetIndex<T>(this List<T> list, int index, int targetIndex)
    {
        if (targetIndex < list.Count)
        {
            T targetObj = list[targetIndex];
            T obj = list[index];

            list[targetIndex] = obj;
            list[index] = targetObj;
        }

        return list;
    }

    public static T[] SetIndex<T>(this T[] list, int index, int targetIndex)
    {
        if (targetIndex < list.Length)
        {
            T targetObj = list[targetIndex];
            T obj = list[index];

            list[targetIndex] = obj;
            list[index] = targetObj;
        }

        return list;
    }

    #endregion

    #region Models

    public static ObjectModel SetScale(this ObjectModel model, Vector3 scale)
    {
        model.transform.localScale = scale;
        return model;
    }

    public static ObjectModel SetPosition(this ObjectModel model, Vector3 position)
    {
        model.transform.position = position;
        return model;
    }

    public static ObjectModel SetLocalPosition(this ObjectModel model, Vector3 position)
    {
        model.transform.localPosition = position;
        return model;
    }

    public static ObjectModel SetLocalRotation(this ObjectModel model, Vector3 rotation)
    {
        model.transform.localEulerAngles = rotation;
        return model;
    }

    public static ObjectModel SetLocalRotation(this ObjectModel model, Quaternion rotation)
    {
        model.transform.localRotation = rotation;
        return model;
    }

    public static ObjectModel SetRotation(this ObjectModel model, Quaternion rotation)
    {
        model.transform.rotation = rotation;
        return model;
    }

    #endregion
}
