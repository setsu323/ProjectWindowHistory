using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ProjectWindowHistory
{
    /// <summary>
    /// ProjectWindowとHistoryのペアを保持しておくScriptableSingleton
    /// アセットとして保存はしてないので、Unityエディタ再起動時には履歴情報は消える
    /// </summary>
    public class ProjectWindowHistoryHolder : ScriptableSingleton<ProjectWindowHistoryHolder>
    {
        [SerializeField] private List<ProjectWindowHistorySaveData> _saveDataList = new();

        public ProjectWindowHistory GetHistory(EditorWindow targetWindow)
        {
            return _saveDataList.FirstOrDefault(data => data.WindowEntityId == targetWindow.GetEntityId())?.History;
        }

        public void Add(EditorWindow targetWindow, ProjectWindowHistory history)
        {
            var saveData = new ProjectWindowHistorySaveData(targetWindow, history);
            _saveDataList.Add(saveData);
        }
    }

    [Serializable]
    public class ProjectWindowHistorySaveData
    {
        [SerializeField] private EntityId _windowEntityId;
        [SerializeField] private ProjectWindowHistory _history;

        public EntityId WindowEntityId => _windowEntityId;
        public ProjectWindowHistory History => _history;

        public ProjectWindowHistorySaveData(EditorWindow window, ProjectWindowHistory history)
        {
            _windowEntityId = window.GetEntityId();
            _history = history;
        }
    }
}