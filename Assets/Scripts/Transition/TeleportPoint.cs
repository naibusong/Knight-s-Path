using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour,IInteractable
{
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO gameSceneToGo;
    public Vector3 PositionToGo;
    public void TriggerAction()
    {
        Debug.Log("����");
        //��������
        loadEventSO.RaiseLoadRequestEvent(gameSceneToGo, PositionToGo, true);
    }

    
}
