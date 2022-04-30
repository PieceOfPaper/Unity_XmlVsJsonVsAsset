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
        if (System.IO.File.Exists(System.IO.Path.Combine(Application.dataPath, "Resources/Data.json"))) System.IO.File.Delete(System.IO.Path.Combine(Application.dataPath, "Resources/Data.json"));

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
        if (System.IO.File.Exists(System.IO.Path.Combine(Application.dataPath, "Resources/Data.asset"))) System.IO.File.Delete(System.IO.Path.Combine(Application.dataPath, "Resources/Data.asset"));

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
        if (System.IO.File.Exists(System.IO.Path.Combine(Application.dataPath, "Resources/Data1.xml"))) System.IO.File.Delete(System.IO.Path.Combine(Application.dataPath, "Resources/Data1.xml"));

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
        if (System.IO.File.Exists(System.IO.Path.Combine(Application.dataPath, "Resources/Data2.xml"))) System.IO.File.Delete(System.IO.Path.Combine(Application.dataPath, "Resources/Data2.xml"));

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
        var data = SerialzedDataClass.LoadFromJSON();
        UnityEngine.Profiling.Profiler.EndSample();
        if (data == null) Debug.LogError("Load Fail");

        UnityEditor.EditorApplication.isPaused = true;
    }

    [ContextMenu("Load ASSET")]
    public void LoadASSET()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Load ASSET");
        var data = ScriptableObjectDataClass.LoadFromASSET();
        UnityEngine.Profiling.Profiler.EndSample();
        if (data == null) Debug.LogError("Load Fail");

        UnityEditor.EditorApplication.isPaused = true;
    }

    [ContextMenu("Load XML (XMLDocument)")]
    public void LoadXML1()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Load XML (XMLDocument)");
        var data = XmlSerializationDataClass.LoadFromXML_XMLDocument();
        UnityEngine.Profiling.Profiler.EndSample();
        if (data == null) Debug.LogError("Load Fail");

        UnityEditor.EditorApplication.isPaused = true;
    }

    [ContextMenu("Load XML (Serializer)")]
    public void LoadXML2()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Load XML (Serializer)");
        var data = XmlSerializationDataClass.LoadFromXML_Serializer();
        UnityEngine.Profiling.Profiler.EndSample();
        if (data == null) Debug.LogError("Load Fail");

        UnityEditor.EditorApplication.isPaused = true;
    }

    [ContextMenu("Build AssetBundle JSON")]
    public void BuildAssetBundleJSON()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Build AssetBundle JSON");
        SerialzedDataClass.JSONToAssetBundle();
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Build AssetBundle ASSET")]
    public void BuildAssetBundleASSET()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Build AssetBundle ASSET");
        ScriptableObjectDataClass.ASSETToAssetBundle();
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Build AssetBundle XML (XMLDocument)")]
    public void BuildAssetBundleXML1()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Build AssetBundle XML (XMLDocument)");
        XmlSerializationDataClass.XMLToAssetBundle_XMLDocument();
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Build AssetBundle XML (Serializer)")]
    public void BuildAssetBundleXML2()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Build AssetBundle XML (Serializer)");
        XmlSerializationDataClass.XMLToAssetBundle_Serializer();
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Load AssetBundle JSON")]
    public void LoadAssetBundleJSON()
    {
        var assetBundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(Application.dataPath, "Resources/assetbundle_json"));

        UnityEngine.Profiling.Profiler.BeginSample("Load AssetBundle JSON");
        var data = SerialzedDataClass.LoadFromJSONAssetBundle(assetBundle);
        UnityEngine.Profiling.Profiler.EndSample();
        if (data == null) Debug.LogError("Load Fail");

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Load AssetBundle ASSET")]
    public void LoadAssetBundleASSET()
    {
        var assetBundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(Application.dataPath, "Resources/assetbundle_asset"));

        UnityEngine.Profiling.Profiler.BeginSample("Load AssetBundle ASSET");
        var data = ScriptableObjectDataClass.LoadFromASSETAssetBundle(assetBundle);
        UnityEngine.Profiling.Profiler.EndSample();
        if (data == null) Debug.LogError("Load Fail");

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Load AssetBundle XML (XMLDocument)")]
    public void LoadAssetBundleXML1()
    {
        var assetBundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(Application.dataPath, "Resources/assetbundle_xml1"));

        UnityEngine.Profiling.Profiler.BeginSample("Load AssetBundle XML (XMLDocument)");
        var data = XmlSerializationDataClass.LoadFromXMLAssetBundle_XMLDocument(assetBundle);
        UnityEngine.Profiling.Profiler.EndSample();
        if (data == null) Debug.LogError("Load Fail");

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }

    [ContextMenu("Load AssetBundle XML (XMLDocument)")]
    public void LoadAssetBundleXML2()
    {
        var assetBundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(Application.dataPath, "Resources/assetbundle_xml2"));

        UnityEngine.Profiling.Profiler.BeginSample("Load AssetBundle XML (Serializer)");
        var data = XmlSerializationDataClass.LoadFromXMLAssetBundle_Serializer(assetBundle);
        UnityEngine.Profiling.Profiler.EndSample();
        if (data == null) Debug.LogError("Load Fail");

        UnityEditor.EditorApplication.isPaused = true;
        UnityEditor.AssetDatabase.Refresh();
    }
#endif
}
