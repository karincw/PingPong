using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentMover : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody;
    protected bool _enable;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public abstract string GetKeyInfo();
    public abstract float GetPowerPercent();
    public abstract void HandleUpButton();
    public abstract void HandleDownButton();
    public abstract void HandleEnterButton();
    public abstract void Init();
    public abstract void Release();
}
