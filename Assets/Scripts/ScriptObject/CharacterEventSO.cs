using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject//继承SO后就不能作为组件了
{
    public UnityAction<Character> OnEventRaised;

    public void RaiseEvent(Character character)
    {
        OnEventRaised?.Invoke(character);
    }
}
