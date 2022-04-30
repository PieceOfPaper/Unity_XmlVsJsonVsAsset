using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerialzedDataClass
{
    [SerializeField] string m_MyName;
    [SerializeField] int m_Level;
    [SerializeField] Vector3 m_Position;
    [SerializeField] Quaternion m_Rotation;
    [SerializeField] float m_Height;
    [SerializeField] SkillData[] m_Skills;

    public string MyName => m_MyName;
    public int Level => m_Level;
    public Vector3 Position => m_Position;
    public Quaternion Rotation => m_Rotation;
    public float Height => m_Height;
    public IEnumerable<SkillData> Skills => m_Skills;


    [System.Serializable]
    public class SkillData
    {
        public enum SkillType
        {
            Normal,
            Attack,
            Buff,
            Utility,

            Max,
        }

        [SerializeField] int m_ID;
        [SerializeField] SkillType m_Type;
        [SerializeField] int m_Level;

        public int ID => m_ID;
        public SkillType Type => m_Type;
        public int Level => m_Level;

        public SkillData(int id, SkillType type, int level)
        {
            m_ID = id;
            m_Type = type;
            m_Level = level;
        }
    }

    public void CreateDummy()
    {
        m_MyName = "¡æ¿’¿Â";
        m_Level = 33;
        m_Position = new Vector3(1.2f, 3.8f, 19.2f);
        m_Rotation = Quaternion.Euler(113.2f, 35.5f, 2f);
        m_Height = 174.5f;

        List<SkillData> skillList = new List<SkillData>();
        for (int i = 0; i < 100000; i ++)
        {
            skillList.Add(new SkillData(UnityEngine.Random.Range(1000, 9999), (SkillData.SkillType)UnityEngine.Random.Range(0, (int)SkillData.SkillType.Max), UnityEngine.Random.Range(1, 100)));
        }
        m_Skills = skillList.ToArray();
    }

    public static void SaveToJSON(SerialzedDataClass obj)
    {
        var str = JsonUtility.ToJson(obj, false);
        System.IO.File.WriteAllText(System.IO.Path.Combine(Application.dataPath, "Resources/data_json.json"), str);
    }

    public static SerialzedDataClass LoadFromJSON()
    {
        var textAsset = Resources.Load<TextAsset>("data_json");
        if (textAsset == null) return null;
        return JsonUtility.FromJson<SerialzedDataClass>(textAsset.text);
    }

    public static void JSONToAssetBundle()
    {
#if UNITY_EDITOR
        UnityEditor.AssetBundleBuild[] bundles = new UnityEditor.AssetBundleBuild[]
        {
            new UnityEditor.AssetBundleBuild()
            {
                assetBundleName = "assetbundle_json",
                assetNames = new string[]
                {
                    "Assets/Resources/data_json.json",
                }
            }
        };
        UnityEditor.BuildPipeline.BuildAssetBundles("Assets/Resources", bundles, UnityEditor.BuildAssetBundleOptions.None, UnityEditor.EditorUserBuildSettings.activeBuildTarget);
#endif
    }

    public static SerialzedDataClass LoadFromJSONAssetBundle(AssetBundle assetBundle)
    {
        //var assetBundle = Resources.Load<AssetBundle>("assetbundle_json");
        if (assetBundle == null) return null;

        var textAsset = assetBundle.LoadAsset<TextAsset>("data_json");
        if (textAsset == null) return null;
        return JsonUtility.FromJson<SerialzedDataClass>(textAsset.text);
    }
}