using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portal
{
    public class PortalInstance : MonoBehaviour
    {
        public struct PositionAndRotation
        {
            public Vector3 lPosition;
            public Quaternion lRotation;
        }
        public enum eColor
        {
            Green,
            Red
        }

        public eColor Color { get { return _color; } }

        void Start()
        {
            Events.RaisePortalCreatedEvent(this);
            greenFrame.gameObject.SetActive(Color == eColor.Green);
            redFrame.gameObject.SetActive(Color == eColor.Red);
        }
        private void OnDestroy()
        {
            Events.RaisePortalDestroyedEvent(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter");
            if (other == null) return;
            WarppableObject warppableObject = other.GetComponent<WarppableObject>();
            if (warppableObject == null) return;

            if (_warppableObjectsInside.Contains(warppableObject)) return;

            if (!IsWarppableObjectInPositionZ(warppableObject)) return; // the object did not enter from forward 

            _warppableObjectsInside.Add(warppableObject);
            Events.RaiseOnObjectEnteredPortal(this, warppableObject);
        }
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("OnTriggerEnd");
            if (other == null) return;
            WarppableObject warppableObject = other.GetComponent<WarppableObject>();
            if (warppableObject == null) return;

            if (!_warppableObjectsInside.Contains(warppableObject)) return;
            
            bool exitedFromBack = !IsWarppableObjectInPositionZ(warppableObject);
            _warppableObjectsInside.Remove(warppableObject);

            Events.RaiseOnObjectExitedPortal(this, warppableObject, exitedFromBack);
        }

        public PositionAndRotation GetWarpableObjectLocalTransforms ( WarppableObject obj )
        {
            PositionAndRotation posAndRot = new PositionAndRotation();
            posAndRot.lPosition = _axis.InverseTransformPoint(obj.transform.position);   // Getting the position in the axis of the portal
            posAndRot.lRotation = Quaternion.Inverse(_axis.rotation) * obj.transform.rotation;   // finding local rotation - we are taking global rotaion and substracting it by Axis rotation.
                                                                                                // in rotations, substraction is inverse
            return posAndRot;
        }
        public bool IsWarppableObjectInPositionZ(WarppableObject obj)
        {
            return GetWarpableObjectLocalTransforms(obj).lPosition.z > 0;
        }

        private List<WarppableObject> _warppableObjectsInside = new List<WarppableObject>();

        [SerializeField]
        private PortalInstance.eColor _color;

        [SerializeField]
        private Transform greenFrame;
        [SerializeField]
        private Transform redFrame;

        [SerializeField]
        private Transform _axis;
    }
}