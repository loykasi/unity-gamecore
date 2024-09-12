using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private InteractionSystem _interactionSystem;
    [SerializeField] private Camera _camera;
    [SerializeField] private InteractableIndicator _indicatorPrefab;
    
    private InteractableIndicator _indicator;
    private InteractableBase _target;

    private void Awake()
    {
        _indicator = Instantiate(_indicatorPrefab, Vector3.zero, Quaternion.identity, transform);
        _indicator.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _interactionSystem.OnHasTarget += OnHasTarget;
        _interactionSystem.OnLostTarget += OnLostTarget;
    }

    private void OnDestroy()
    {
        _interactionSystem.OnHasTarget += OnHasTarget;
        _interactionSystem.OnLostTarget += OnLostTarget;
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 screenPosition = _camera.WorldToScreenPoint(_target.transform.position + _target.InteractableData.Offset);
            screenPosition.z = 0;
            if (screenPosition.x > 0 && screenPosition.x < Screen.width &&
                screenPosition.y > 0 && screenPosition.y < Screen.height)
            {
                if (_indicator.transform.position != screenPosition)
                {
                    _indicator.gameObject.SetActive(true);
                    _indicator.transform.position = screenPosition;
                }
            }
            else
            {
                _indicator.gameObject.SetActive(false);
            }
        }
    }

    private void OnHasTarget(InteractableBase interactable)
    {
        _target = interactable;
        _indicator.SetData(_target.InteractableData);
    }

    private void OnLostTarget()
    {
        _target = null;
        _indicator.gameObject.SetActive(false);
    }
}
