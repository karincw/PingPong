using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Map
{
    [Space(4), Header("Portal Settings")]
    [SerializeField] private TornadoObject _portalPrefab;
    [SerializeField] private List<Vector2> _portalPositions;
    private List<TornadoObject> _portals = new();

    public override void SetUp()
    {
        foreach (var portalPos in _portalPositions)
        {
            var portal = Instantiate(_portalPrefab, transform);
            portal.transform.localPosition = portalPos;
            portal.Init();
            _portals.Add(portal);
        }
    }

    public override void Release()
    {
        for (int i = 0; i < _portals.Count; i++)
        {
            _portals[i].Release();
        }
    }
}