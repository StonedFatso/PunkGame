using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanClass : MonoBehaviour
{
    [SerializeField]
    private int humanClass = 0;

    public int Class { get => humanClass; }
}
