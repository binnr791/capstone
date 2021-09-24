using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;

public class Node : MonoBehaviour, IComparable<Node>
{

    [Header("Data")]
    [SerializeField] private int _id;
    [SerializeField] private bool _status;
    [SerializeField] private string _eventstatus;

}
