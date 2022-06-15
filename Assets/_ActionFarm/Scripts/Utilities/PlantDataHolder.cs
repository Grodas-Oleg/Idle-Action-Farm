using System;
using UnityEngine;

namespace _ActionFarm.Scripts.Utilities
{
    [CreateAssetMenu(fileName = "PlantData", menuName = "Holders/PlantDataHolder")]
    public class PlantDataHolder : ScriptableObject
    {
        [SerializeField] private PlantData[] plantDatas;
        [SerializeField] private GameObject _plantBlock;
        public PlantData[] PlantDatas => plantDatas;
    }

    [Serializable]
    public class PlantData
    {
        public Mesh stagMesh;
        public float growthTime;
    }
}