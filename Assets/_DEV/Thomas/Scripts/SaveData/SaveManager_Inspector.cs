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
        root.Add(LoadButton);
        root.Add(saveButton);

        return root;
    }
}