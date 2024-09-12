using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public bool IsActive = true;

    public System.Action<InteractableBase> OnHasTarget;
    public System.Action OnLostTarget;

    [SerializeField] private float _radius;
    [SerializeField] private float _angle;
    [SerializeField] private int _totalInteractable;
    [SerializeField] private LayerMask _interactableLayerMask;
    [SerializeField] private LayerMask _environmentLayerMask; 

    [Header("Advanced")]
    [SerializeField] private float _checkInterval;
    private float _checkTime;
 
    private Collider[] _colliders;
    private List<InteractableBase> _interactables = new();
    private InteractableBase _previousTargetInteractable;
    private InteractableBase _targetInteractable;

    private void Awake()
    {
        _colliders = new Collider[_totalInteractable];
    }

    private void OnEnable()
    {
        GlobalData.InteractionSystem = this;

        InputReader.OnInteractInput += OnInteract;
    }

    private void OnDisable()
    {
        GlobalData.InteractionSystem = null;

        InputReader.OnInteractInput -= OnInteract;
    }

    private void Update()
    {
        if (!IsActive) return;
        if (_checkTime > 0)
        {
            _checkTime -= Time.deltaTime;
        }
        else
        {
            _checkTime = _checkInterval;
            GetInteractable();
            UpdateTargetInteractable();
        }
    }

    private void GetInteractable()
    {
        int numCollider = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _interactableLayerMask);
        _interactables.Clear();
        for (int i = 0; i < numCollider; i++)
        {
            var collider = _colliders[i];
            if (IsPointInView(collider.transform.position))
            {
                if (collider.TryGetComponent(out InteractableBase interactable))
                {
                    if (interactable.CanInteract)
                        _interactables.Add(interactable);
                }
            }
        }
    }

    private void UpdateTargetInteractable()
    {
        if (_interactables.Count > 0)
        {
            InteractableBase nearest = _interactables[0];
            float minSq = (nearest.transform.position - transform.position).sqrMagnitude;
            float nearestDot = Vector3.Dot((nearest.transform.position - transform.position).normalized, transform.forward);
            for (int i = 1; i < _interactables.Count; i++)
            {
                float dot = Vector3.Dot((_interactables[i].transform.position - transform.position).normalized, transform.forward);
                if (dot > nearestDot)
                {
                    nearest = _interactables[i];
                    nearestDot = dot;
                }
            }

            if (_targetInteractable == null || nearest != _previousTargetInteractable)
            {
                _targetInteractable = nearest;
                OnHasTarget?.Invoke(_targetInteractable);
            }
            _targetInteractable = nearest;
            _previousTargetInteractable = _targetInteractable;
        }
        else
        {
            if (_targetInteractable != null)
            {
                _targetInteractable = null;
                OnLostTarget?.Invoke();
            }
        }
    }

    public bool IsPointInView(Vector3 point)
    {
        Vector3 direction = point - transform.position;
        
        if (direction.sqrMagnitude > _radius * _radius)
        {
            return false;
        }
    
        if (Vector3.Angle(transform.forward, direction) > _angle)
        {
            return false;
        }

        if (Physics.Linecast(transform.position, point, _environmentLayerMask))
        {
            return false;
        }

        return true;
    }

    private void OnInteract()
    {
        if (_targetInteractable != null)
        {
            _targetInteractable.Interact();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(_angle, transform.up) * transform.forward * _radius);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(- _angle, transform.up) * transform.forward * _radius);

        Gizmos.color = Color.green;
        if (_interactables != null && _interactables.Count > 0)
            foreach (var item in _interactables)
            {
                Gizmos.DrawSphere(item.transform.position, 0.1f);
            }

        Gizmos.color = Color.red;
        if (_targetInteractable != null)
        {
            Gizmos.DrawSphere(_targetInteractable.transform.position, 0.2f);
        }
    }
}