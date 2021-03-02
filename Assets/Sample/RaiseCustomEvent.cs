using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaiseCustomEvent : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [SerializeField] private string eventName;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CustomEvent.Trigger(target, eventName);
        }
    }
}
