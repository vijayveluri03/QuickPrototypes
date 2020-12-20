using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portal
{
    public class Wormhole : MonoBehaviour
    {
        [SerializeField]
        private PortalSet portalSet;
        private void Awake()
        {
            Events.ObjectEnteredPortal += OnObjectEnterWarp;
            Events.ObjectExitedPortal += OnObjectExitedWarp;

            Utils.Assert(portalSet != null);
        }

        private void OnObjectEnterWarp( PortalInstance portal, WarppableObject warppableObj )
        {
            Debug.Log("OnObjectEnterWarp 1");
            Utils.Assert(portal != null && warppableObj != null);
            if (!portalSet.IsWormHoleOpen()) return;
            if (GetWormHoleSet(warppableObj) != null) return;

            warppableObj.CloneObject();
            warpableObjectSets.Add(new WarpableObjectSet(portal, portalSet.GetOtherPortal(portal), warppableObj) );
        }
        private void OnObjectExitedWarp(PortalInstance portal, WarppableObject warppableObj, bool crossedTheWormHole)
        {
            Utils.Assert(portal != null && warppableObj != null);
            if (!portalSet.IsWormHoleOpen()) return;

            WarpableObjectSet warpableObjectSet = GetWormHoleSet(warppableObj);
            if ( warpableObjectSet == null) { Debug.LogWarning("Check this!"); return; }

            if (crossedTheWormHole)
                warpableObjectSet._warpObject.SwapWithClone();
            warpableObjectSet._warpObject.DestroyClone();

            warpableObjectSets.Remove(warpableObjectSet);
        }

        private void Update()
        {
            foreach (var set in warpableObjectSets)
            {
                PortalInstance.PositionAndRotation localPosition = set._portalA.GetWarpableObjectLocalTransforms(set._warpObject);
                localPosition.lPosition.z = -1 * localPosition.lPosition.z;   // Because we want the object to come out of other portal, not go inside.
                                                                            // Both the portals have same forward direction. 
                localPosition.lRotation = Quaternion.AngleAxis(180, set._portalA.transform.up) * localPosition.lRotation; // the direction of the player should be reverse wrt the axis. if the players forward
                                                                                                // is towards portal A, his direction should be away from Portal B. 
                set._warpObject._myClone.transform.position = set._portalB.transform.TransformPoint( localPosition.lPosition);
                set._warpObject._myClone.transform.rotation = set._portalB.transform.rotation * localPosition.lRotation;
            }
        }

        private WarpableObjectSet GetWormHoleSet( WarppableObject obj)
        {
            foreach( var set in warpableObjectSets)
            {
                if (set._warpObject == obj) return set;
            }
            return null;
        }

        private List<WarpableObjectSet> warpableObjectSets = new List<WarpableObjectSet>();
    }

    class WarpableObjectSet
    {
        public WarpableObjectSet( PortalInstance portalA, PortalInstance portalB, WarppableObject warpableObj )
        {
            _portalA = portalA;
            _portalB = portalB;
            _warpObject = warpableObj;
        }
        public PortalInstance _portalA { get; private set; } = null;
        public PortalInstance _portalB { get; private set; } = null;
        public WarppableObject _warpObject { get; private set; } = null;
    }
}
