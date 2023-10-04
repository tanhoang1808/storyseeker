using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper instance;
    public static CoroutineHelper Instance { get => instance;}

    private void Awake()
    {
        CoroutineHelper.instance = this;
    }

}
