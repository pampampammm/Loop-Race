using System;
using UnityEngine;

namespace LoopRace.Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private MainUIView _mainUIView;
        private bool _lockScreen = true;
        
        public event Action<Garage> GarageClicked;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_lockScreen)
                {
                    _lockScreen = false;
                    _mainUIView.DisableTapScreen();
                    return;
                }
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        if(hit.collider.TryGetComponent<GarageButton>(out var component))
                        {
                            GarageClicked?.Invoke(component.Garage);
                        }
                    }
                }
            }
            
            
            if (Input.touchCount > 0)
            {
                if (_lockScreen)
                {
                    _lockScreen = false;
                    _mainUIView.DisableTapScreen();
                    return;
                }
                
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        if(hit.collider.TryGetComponent<GarageButton>(out var component))
                        {
                            GarageClicked?.Invoke(component.Garage);
                        }
                    }
                }
            }
        }
    }
}