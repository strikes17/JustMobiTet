using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts;
using UnityEditor;
using UnityEngine;

public class ConnectorFinderWindow : EditorWindow
{
    private string m_StringFinder;

    [MenuItem("Tools/Speed Time")]
    public static void SpeedTime()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 1f) ? 4f :
            Mathf.Approximately(Time.timeScale, 4f) ? 16f : 1f;
    }

    [MenuItem("Examples/My Editor Window")]
    public static void ShowExample()
    {
        ConnectorFinderWindow wnd = GetWindow<ConnectorFinderWindow>();
        wnd.titleContent = new GUIContent("Connector Finder");
    }

    private void OnGUI()
    {
        m_StringFinder = GUILayout.TextField(m_StringFinder);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Connectors"))
        {
            List<GameObject> toSelect = new();
            var entities = FindObjectsByType<AbstractEntity>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var abstractEntity in entities)
            {
                var connectors = abstractEntity.Connectors;
                if (connectors.Any(x => x == null))
                {
                    toSelect.Add(abstractEntity.gameObject);
                }
                else
                if (connectors.FirstOrDefault(x =>
                        x.GetType().ToString().ToLower().Contains(m_StringFinder.ToLower())) != null)
                {
                    toSelect.Add(abstractEntity.gameObject);
                }
            }

            Selection.objects = toSelect.ToArray();
        }

        if (GUILayout.Button("Modules"))
        {
            List<GameObject> toSelect = new();
            var entities = FindObjectsByType<AbstractEntity>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var abstractEntity in entities)
            {
                var modules = abstractEntity.BehaviourModules;
                if (modules.Any(x => x == null))
                {
                    toSelect.Add(abstractEntity.gameObject);
                }
                else
                if (modules.FirstOrDefault(x => x.GetType().ToString().ToLower().Contains(m_StringFinder.ToLower())) !=
                    null)
                {
                    toSelect.Add(abstractEntity.gameObject);
                }
            }

            Selection.objects = toSelect.ToArray();
        }

        GUILayout.EndHorizontal();
    }
}