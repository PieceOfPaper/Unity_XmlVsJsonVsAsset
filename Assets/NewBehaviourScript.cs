using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#if UNITY_EDITOR
    [ContextMenu("Create JSON")]
    public void CreateJSON()
    {
        if (System.IO.File.Exists(System.IO.Path.Combine(Application.dataPath, "Data.json"))) System.IO.File.Delete(System.IO.Path.Combine(Application.dataPath, "Data.json"));

        var data_json = new SerialzedDataClass();
        data_json.CreateDummy();

        UnityEngine.Profiling.Profiler.BeginSample("Create JSON");
        SerialzedDataClass.SaveToJSON(data_json);
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Create ASSET")]
    public void CreateASSET()
    {
        if (System.IO.File.Exists(System.IO.Path.Combine(Application.dataPath, "Data.asset"))) System.IO.File.Delete(System.IO.Path.Combine(Application.dataPath, "Data.asset"));

        var data_asset = ScriptableObjectDataClass.CreateInstance<ScriptableObjectDataClass>();
        data_asset.CreateDummy();

        UnityEngine.Profiling.Profiler.BeginSample("Create ASSET");
        ScriptableObjectDataClass.SaveToASSET(data_asset);
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Create XML (XMLDocument)")]
    public void CreateXML1()
    {
        if (System.IO.File.Exists(System.IO.Path.Combine(Application.dataPath, "Data1.xml"))) System.IO.File.Delete(System.IO.Path.Combine(Application.dataPath, "Data1.xml"));

        var data_xml = new XmlSerializationDataClass();
        data_xml.CreateDummy();

        UnityEngine.Profiling.Profiler.BeginSample("Create XML (XMLDocument)");
        XmlSerializationDataClass.SaveToXML_XMLDocument(data_xml);
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Create XML (Serializer)")]
    public void CreateXML2()
    {
        if (System.IO.File.Exists(System.IO.Path.Combine(Application.dataPath, "Data2.xml"))) System.IO.File.Delete(System.IO.Path.Combine(Application.dataPath, "Data2.xml"));

        var data_xml = new XmlSerializationDataClass();
        data_xml.CreateDummy();

        UnityEngine.Profiling.Profiler.BeginSample("Create XML (Serializer)");
        XmlSerializationDataClass.SaveToXML_Serializer(data_xml);
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Load JSON")]
    public void LoadJSON()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Load JSON");
        var data_json = SerialzedDataClass.LoadFromJSON();
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
    }

    [ContextMenu("Load ASSET")]
    public void LoadASSET()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Load ASSET");
        var data_asset = ScriptableObjectDataClass.LoadFromASSET();
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
    }

    [ContextMenu("Load XML (XMLDocument)")]
    public void LoadXML1()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Load XML (XMLDocument)");
        var data_xml1 = XmlSerializationDataClass.LoadFromXML_XMLDocument();
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
    }

    [ContextMenu("Load XML (Serializer)")]
    public void LoadXML2()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Load XML (Serializer)");
        var data_xml2 = XmlSerializationDataClass.LoadFromXML_Serializer();
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
    }
#endif
}
