using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Map
{
    [Space(4), Header("Portal Settings")]
    [SerializeField] private PortalObject _portalPrefab;
    [SerializeField] private List<Vector2> _portalPositions;
    private List<PortalObject> _portals = new();

    public override void SetUp()
    {
        PortalObject fp = null;
        foreach (var portalPos in _portalPositions)
        {
            var portal = Instantiate(_portalPrefab, transform);
            portal.transform.localPosition = portalPos;
            if (fp == null) fp = portal;
            else
            {
                fp._warpTarget = portal;
                portal._warpTarget = fp;
            }
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