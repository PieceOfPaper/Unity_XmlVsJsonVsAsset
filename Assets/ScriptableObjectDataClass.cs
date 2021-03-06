using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectDataClass : ScriptableObject
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
        m_MyName = "??????";
        m_Level = 33;
        m_Position = new Vector3(1.2f, 3.8f, 19.2f);
        m_Rotation = Quaternion.Euler(113.2f, 35.5f, 2f);
        m_Height = 174.5f;

        List<SkillData> skillList = new List<SkillData>();
        for (int i = 0; i < 100000; i++)
        {
            skillList.Add(new SkillData(UnityEngine.Random.Range(1000, 9999), (SkillData.SkillType)UnityEngine.Random.Range(0, (int)SkillData.SkillType.Max), UnityEngine.Random.Range(1, 100)));
        }
        m_Skills = skillList.ToArray();
    }

    public static void SaveToASSET(ScriptableObjectDataClass obj)
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.CreateAsset(obj, "Assets/Resources/data_asset.asset");
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    public static ScriptableObjectDataClass LoadFromASSET()
    {
        return Resources.Load<ScriptableObjectDataClass>("data_asset");
    }

    public static void ASSETToAssetBundle()
    {
#if UNITY_EDITOR
        UnityEditor.AssetBundleBuild[] bundles = new UnityEditor.AssetBundleBuild[]
        {
            new UnityEditor.AssetBundleBuild()
            {
                assetBundleName = "assetbundle_asset",
                assetNames = new string[]
                {
                    "Assets/Resources/data_asset.asset",
                }
            }
        };
        UnityEditor.BuildPipeline.BuildAssetBundles("Assets/Resources", bundles, UnityEditor.BuildAssetBundleOptions.None, UnityEditor.EditorUserBuildSettings.activeBuildTarget);
#endif
    }

    public static ScriptableObjectDataClass LoadFromASSETAssetBundle(AssetBundle assetBundle)
    {
        //var assetBundle = Resources.Load<AssetBundle>("assetbundle_asset");
        if (assetBundle == null) return null;

        return assetBundle.LoadAsset<ScriptableObjectDataClass>("data_asset");
    }
}
