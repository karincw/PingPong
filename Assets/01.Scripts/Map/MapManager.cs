using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Rough _rough;
    [SerializeField] private Tornado _tornado;
    [SerializeField] private Speed _speed;
    [SerializeField] private Portal _portal;
    [SerializeField] private Gravity _gravity;

    private Map _current;

    public void ChangeMap(Events evt)
    {
        switch (evt)
        {
            case Events.Normal:
                _current?.Release();
                _current = null;
                break;
            case Events.Gravity:
                _current?.Release();
                _current = _gravity;
                _current.SetUp();
                break;
            case Events.Portal:
                _current?.Release();
                _current = _portal;
                _current.SetUp(); 
                break;
            case Events.Speeeed:
                _current?.Release();
                _current = _speed;
                _current.SetUp();
                break;
            case Events.Rough:
                _current?.Release();
                _current = _rough;
                _current.SetUp();
                break;
            case Events.Tornado:
                _current?.Release();
                _current = _tornado;
                _current.SetUp();
                break;
        }
    }
}
