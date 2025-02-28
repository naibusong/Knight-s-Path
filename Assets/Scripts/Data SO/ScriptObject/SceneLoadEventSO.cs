using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]

public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;

    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="locationToLoad">Ҫ���صĳɾ�</param>
    /// <param name="posToGo">Ҫȥ������</param>
    /// <param name="fadeScene">�Ƿ���Ҫ���볡��</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScene)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGo, fadeScene);
    }
}
