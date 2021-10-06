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

    public string eventstatus { get => _eventstatus; set => _eventstatus = value; }
    public bool status { get => _status; set => _status = value; }
    public int id { get => _id; set => _id = value; }

    public int CompareTo(Node other)
    {
        throw new NotImplementedException();
    }
    
}

public enum NodeEvent
{
    trap, cardGain
}