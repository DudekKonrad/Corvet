using System;
using UnityEngine;

namespace Application.Contexts.ProjectContext.Configs
{
    public enum ExpType
    {
        Medium = 0
    }
    [Serializable]
    public class ExpConfig
    { 
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _value;
        [SerializeField] private float _followSpeed;

        public GameObject Prefab => _prefab;
        public int Value => _value;
        public float FollowSpeed => _followSpeed;
    }
}