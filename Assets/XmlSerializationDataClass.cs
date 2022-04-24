using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum XmlSerializationSkillType
{
    Normal,
    Attack,
    Buff,
    Utility,

    Max,
}

public class XmlSerializationSkillData
{

    [System.Xml.Serialization.XmlAttribute("m_ID")] public int m_ID;
    [System.Xml.Serialization.XmlAttribute("m_Type")] public XmlSerializationSkillType m_Type;
    [System.Xml.Serialization.XmlAttribute("m_Level")] public int m_Level;

    public int ID => m_ID;
    public XmlSerializationSkillType Type => m_Type;
    public int Level => m_Level;

    public XmlSerializationSkillData() { }

    public XmlSerializationSkillData(int id, XmlSerializationSkillType type, int level)
    {
        m_ID = id;
        m_Type = type;
        m_Level = level;
    }
}

[System.Xml.Serialization.XmlRoot("XmlSerializationDataClass")]
public class XmlSerializationDataClass
{
    [System.Xml.Serialization.XmlAttribute("m_Name")] public string m_Name;
    [System.Xml.Serialization.XmlAttribute("m_Level")] public int m_Level;
    public Vector3 m_Position;
    public Quaternion m_Rotation;
    [System.Xml.Serialization.XmlAttribute("m_Height")] public float m_Height;
    [System.Xml.Serialization.XmlArray("m_Skills"), System.Xml.Serialization.XmlArrayItem("XmlSerializationSkillData")] public XmlSerializationSkillData[] m_Skills;

    public string Name => m_Name;
    public int Level => m_Level;
    public Vector3 Position => m_Position;
    public Quaternion Rotation => m_Rotation;
    public float Height => m_Height;
    public IEnumerable<XmlSerializationSkillData> Skills => m_Skills;


    [System.Xml.Serialization.XmlAttribute("m_Position")]
    public string Position_Surrogate
    {
        get
        {
            return m_Position.ToString();
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                m_Position = Vector3.zero;
                return;
            }

            var posStrSplited = value.Substring(1, value.Length - 2).Split(',');
            m_Position = new Vector3(
                posStrSplited == null || posStrSplited.Length < 1 ? 0f : float.Parse(posStrSplited[0].Trim()),
                posStrSplited == null || posStrSplited.Length < 2 ? 0f : float.Parse(posStrSplited[1].Trim()),
                posStrSplited == null || posStrSplited.Length < 3 ? 0f : float.Parse(posStrSplited[2].Trim()));
        }
    }

    [System.Xml.Serialization.XmlAttribute("m_Rotation")]
    public string Rotation_Surrogate
    {
        get
        {
            return m_Rotation.ToString();
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                m_Rotation = Quaternion.identity;
                return;
            }

            var rotStrSplited = value.Substring(1, value.Length - 2).Split(',');
            m_Rotation = new Quaternion(
                rotStrSplited == null || rotStrSplited.Length < 1 ? 0f : float.Parse(rotStrSplited[0].Trim()),
                rotStrSplited == null || rotStrSplited.Length < 2 ? 0f : float.Parse(rotStrSplited[1].Trim()),
                rotStrSplited == null || rotStrSplited.Length < 3 ? 0f : float.Parse(rotStrSplited[2].Trim()),
                rotStrSplited == null || rotStrSplited.Length < 3 ? 0f : float.Parse(rotStrSplited[2].Trim()));
        }
    }

    public void CreateDummy()
    {
        m_Name = "Á¾ÀÕÀå";
        m_Level = 33;
        m_Position = new Vector3(1.2f, 3.8f, 19.2f);
        m_Rotation = Quaternion.Euler(113.2f, 35.5f, 2f);
        m_Height = 174.5f;

        List<XmlSerializationSkillData> skillList = new List<XmlSerializationSkillData>();
        for (int i = 0; i < 100000; i++)
        {
            skillList.Add(new XmlSerializationSkillData(UnityEngine.Random.Range(1000, 9999), (XmlSerializationSkillType)UnityEngine.Random.Range(0, (int)XmlSerializationSkillType.Max), UnityEngine.Random.Range(1, 100)));
        }
        m_Skills = skillList.ToArray();
    }

    public static void SaveToXML_XMLDocument(XmlSerializationDataClass obj)
    {
        var xmlDocument = new System.Xml.XmlDocument();
        xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        var rootNode = xmlDocument.CreateNode(System.Xml.XmlNodeType.Element, "Root", string.Empty);
        var rootElement = rootNode as System.Xml.XmlElement;

        rootElement.SetAttribute("m_Name", obj.m_Name);
        rootElement.SetAttribute("m_Level", obj.m_Level.ToString());
        rootElement.SetAttribute("m_Position", obj.Position_Surrogate);
        rootElement.SetAttribute("m_Rotation", obj.Rotation_Surrogate);
        rootElement.SetAttribute("m_Height", obj.m_Height.ToString());
        xmlDocument.AppendChild(rootNode);

        for (int i = 0; i < obj.m_Skills.Length; i++)
        {
            System.Xml.XmlElement skillDataNode = xmlDocument.CreateElement("SkillData");
            skillDataNode.SetAttribute("m_ID", obj.m_Skills[i].ID.ToString());
            skillDataNode.SetAttribute("m_Type", obj.m_Skills[i].Type.ToString());
            skillDataNode.SetAttribute("m_Level", obj.m_Skills[i].Level.ToString());
            rootNode.AppendChild(skillDataNode);
        }

        xmlDocument.Save(System.IO.Path.Combine(Application.dataPath, "Data1.xml"));
    }

    public static void SaveToXML_Serializer(XmlSerializationDataClass obj)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlSerializationDataClass));
        var stream = new System.IO.FileStream(System.IO.Path.Combine(Application.dataPath, "Data2.xml"), System.IO.FileMode.Create);
        serializer.Serialize(stream, obj);
        stream.Close();
    }

    public static XmlSerializationDataClass LoadFromXML_XMLDocument()
    {
        var xmlDocument = new System.Xml.XmlDocument();
        xmlDocument.Load(System.IO.Path.Combine(Application.dataPath, "Data1.xml"));

        var data = new XmlSerializationDataClass();

        foreach (System.Xml.XmlNode node in xmlDocument.ChildNodes)
        {
            if (node == null) continue;
            if (node.Name == "Root")
            {
                var rootElement = node as System.Xml.XmlElement;
                if (rootElement == null) continue;

                data.m_Name = rootElement.GetAttribute("m_Name");
                data.m_Level = int.Parse(rootElement.GetAttribute("m_Level"));
                data.Position_Surrogate = rootElement.GetAttribute("m_Position");
                data.Rotation_Surrogate = rootElement.GetAttribute("m_Rotation");
                data.m_Height = float.Parse(rootElement.GetAttribute("m_Height"));

                List<XmlSerializationSkillData> skillDataList = new List<XmlSerializationSkillData>();
                foreach(System.Xml.XmlNode node2 in rootElement.ChildNodes)
                {
                    if (node2 == null) continue;
                    if (node2.Name == "SkillData")
                    {
                        var skillDataElement = node2 as System.Xml.XmlElement;
                        if (skillDataElement == null) continue;

                        var skillData = new XmlSerializationSkillData();
                        skillData.m_ID = int.Parse(skillDataElement.GetAttribute("m_ID"));
                        skillData.m_Type = System.Enum.Parse<XmlSerializationSkillType>(skillDataElement.GetAttribute("m_Type"));
                        skillData.m_Level = int.Parse(skillDataElement.GetAttribute("m_Level"));
                        skillDataList.Add(skillData);
                    }
                }
                data.m_Skills = skillDataList.ToArray();
            }
        }

        return data;
    }

    public static XmlSerializationDataClass LoadFromXML_Serializer()
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlSerializationDataClass));
        var stream = new System.IO.FileStream(System.IO.Path.Combine(Application.dataPath, "Data2.xml"), System.IO.FileMode.Open);
        var data = serializer.Deserialize(stream) as XmlSerializationDataClass;
        stream.Close();
        return data;
    }
}
