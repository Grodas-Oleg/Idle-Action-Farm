using _ActionFarm.Scripts.Utilities;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace _ActionFarm.Scripts.Components
{
    public class PlantGrowthController : MonoBehaviour

    {
        [SerializeField] private PlantDataHolder _plantDataHolder;
        [SerializeField] private MeshFilter _mesh;
        [SerializeField] private Collider[] _colliders;

        private int _currentPlantStage = 0;
        private bool _seqStatus;

        private int _maxPlantStage => _plantDataHolder.PlantDatas.Length - 1;
        private Sequence growthSequence;

        private void Start()
        {
            _mesh.mesh = _plantDataHolder.PlantDatas[_currentPlantStage].stagMesh;
            SetColliderStatus(false);
            CallGrowthSequence();
        }

        private void CallGrowthSequence()
        {
            if (_currentPlantStage == _maxPlantStage) return;

            growthSequence?.Kill();

            growthSequence = DOTween.Sequence()
                .AppendInterval(_plantDataHolder.PlantDatas[_currentPlantStage].growthTime)
                .AppendCallback(GrowthPlant);
        }

        private void GrowthPlant()
        {
            ++_currentPlantStage;
            SetColliderStatus(_currentPlantStage != 0);
            CallGrowthSequence();
            SetPlantMesh();
        }

        public void HarvestPlant(UnityAction action)
        {
            --_currentPlantStage;
            SetPlantMesh();
            SetColliderStatus(_currentPlantStage != 0);
            CallGrowthSequence();

            if (_currentPlantStage == 0)
                action?.Invoke();
        }

        private void SetPlantMesh() => _mesh.mesh = _plantDataHolder.PlantDatas[_currentPlantStage].stagMesh;

        private void SetColliderStatus(bool status)
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = status;
            }
        }
    }
}