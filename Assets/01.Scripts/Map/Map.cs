using DG.Tweening;
using UnityEngine;

public abstract class Map : MonoBehaviour
{
    [Header("Map Settings")]
    [SerializeField] protected float _changingTime = 1;

    public abstract void SetUp();
    public abstract void Release();
}
