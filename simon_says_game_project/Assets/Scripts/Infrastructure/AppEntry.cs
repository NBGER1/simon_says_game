using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;

public class AppEntry : MonoBehaviour
{
    void Start()
    {
        GameplayServices.Initialize();
    }
}