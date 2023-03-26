using System;
using TMPro;
using UnityEngine;

namespace LoopRace.Scripts.Player
{
    public class GarageButton : MonoBehaviour
    {
        [SerializeField] private Garage _garage;
        [SerializeField] private GarageInfoBlock _infoBlock;
        public Garage Garage => _garage;
        public GarageInfoBlock InfoBlock => _infoBlock;
        
        public void SetCamera(Camera camera)
        {
            var pos = camera.transform.position;
            _infoBlock.transform.rotation = Quaternion.LookRotation(pos);
        }

        private void FixedUpdate()
        {
            _infoBlock.Text.text = _garage.ReleasedCarsCount + "/" + _garage.CarCount;
        }
    }
}