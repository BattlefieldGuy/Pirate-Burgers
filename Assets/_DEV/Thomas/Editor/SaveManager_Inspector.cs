using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

[CustomEditor(typeof(SaveManager))]
public class SaveManager_Inspector : Editor
{
    public VisualTreeAsset m_inspectorXML;
    public override VisualElement CreateInspectorGUI()
    {
        // Build a root element and include the default inspector (IMGUI) so existing fields still show
        var root = new VisualElement();
        root.Add(new IMGUIContainer(() => DrawDefaultInspector()));

        #region Save button
        // Create a UIElements button and wire it to the SaveIngredients method on the inspected SaveManager
        var saveButton = new UnityEngine.UIElements.Button(() =>
        {
            if (target is SaveManager saveManager)
            {
                saveManager.SaveIngredients();
            }
            else
            {
                Debug.LogError("Target is not a SaveManager.");
            }
        })
        {
            text = "save all ingredients"
        };
        #endregion
        #region Load button
        var LoadButton = new UnityEngine.UIElements.Button(() =>
        {
            if (target is SaveManager saveManager)
            {
                saveManager.LoadIngredients();
            }
            else
            {
                Debug.LogError("Target is not a SaveManager.");
            }
        })
        {
            text = "Load all ingredients"
        };
        #endregion
        
        #region SavetoJSON button
        var SaveToJSONButton = new UnityEngine.UIElements.Button(() =>
        {
            if (target is SaveManager saveManager)
            {
                saveManager.ConvertToJSON();
            }
            else
            {
                Debug.LogError("Target is not a SaveManager.");
            }
        })
        {
            text = "Save to JSON"
        };
        #endregion

        #region  LoadfromJSON button with target string input
        var FilenameField = new TextField("Filename:");
        /*
                 FilenameField.style.marginTop = 10;
        FilenameField.style.marginBottom = 5;
         */

        
        var LoadFromJSONButton = new UnityEngine.UIElements.Button(() =>
        {
            if (target is SaveManager saveManager)
            {
                string filename = FilenameField.value;
                saveManager.loadFromJSON(filename);
            }
            else
            {
                Debug.LogError("Target is not a SaveManager.");
            }
        })
        {
            text = "Load from JSON"
        };

        #endregion
        
        root.Add(LoadButton);
        root.Add(saveButton);
        
        //add seperation line with name 'JSON Operations'
        var separator = new VisualElement();
        separator.style.height = 2;
        separator.style.backgroundColor = Color.gray;
        separator.style.marginTop = 10;
        separator.style.marginBottom = 10;
        root.Add(separator);
        var jsonLabel = new Label("JSON Operations");
        jsonLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
        root.Add(jsonLabel);
        
        root.Add(SaveToJSONButton);
        root.Add(FilenameField);
        root.Add(LoadFromJSONButton);
        

        return root;
    }
}