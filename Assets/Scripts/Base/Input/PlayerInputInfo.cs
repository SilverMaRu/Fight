using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerInputInfo")]
[System.Serializable]
public class PlayerInputInfo : ScriptableObject
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode baseSkillKey1 = KeyCode.J;
    public KeyCode baseSkillKey2 = KeyCode.K;
    public KeyCode baseSkillKey3 = KeyCode.L;
    public KeyCode baseSkillKey4 = KeyCode.U;
    public KeyCode baseSkillKey5 = KeyCode.I;
    public KeyCode baseSkillKey6 = KeyCode.O;

    public static string ToSaveData(object saveObj)
    {
        string lineFormat = "{0} = {1}\n";
        string dataStr = string.Empty;
        System.Type type = saveObj.GetType();
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo tempFieldInfo in fields)
        {
            object value = tempFieldInfo.GetValue(saveObj);
            if (tempFieldInfo.FieldType.IsEnum)
            {
                value = (int)tempFieldInfo.GetValue(saveObj);
            }
            dataStr += string.Format(lineFormat, tempFieldInfo.Name, value);
        }
        Debug.Log(dataStr);
        return dataStr;
    }

    public static T FromSaveData<T>(string saveData) where T : class, new()
    {
        System.Type typeOfInt = typeof(int);
        System.Type typeOfFloat = typeof(float);
        System.Type typeOfString = typeof(string);

        T resultObj = new T();
        System.Type objType = resultObj.GetType();

        string[] allLines = saveData.Split('\n');
        System.Reflection.FieldInfo[] fields = objType.GetFields();
        foreach (string line in allLines)
        {
            string[] splitLine = line.Split('=');
            if (splitLine.Length < 2) continue;
            string fieldName = splitLine[0].Trim();
            string value = splitLine[1].Trim();

            int lastIndex = 0;
            int length = fields.Length;
            for (int i = 0; i < length; i++)
            {
                int thisIndex = i + lastIndex;
                System.Reflection.FieldInfo thisField = fields[thisIndex];
                if (thisField.Name.Equals(fieldName))
                {
                    System.Type fieldType = thisField.FieldType;
                    if (fieldType.Equals(typeOfInt))
                    {
                        thisField.SetValue(resultObj, System.Convert.ToInt32(value));
                    }
                    else if (fieldType.Equals(typeOfFloat))
                    {
                        thisField.SetValue(resultObj, System.Convert.ToSingle(value));
                    }
                    else if (fieldType.Equals(typeOfString))
                    {
                        thisField.SetValue(resultObj, value);
                    }
                    else if (fieldType.IsEnum)
                    {
                        thisField.SetValue(resultObj, System.Enum.ToObject(fieldType, System.Convert.ToInt32(value)));
                    }
                    break;
                }
            }
        }
        return resultObj;
    }
}
