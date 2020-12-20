using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portal
{

    public static class Events
    {
        public static event System.Action<PortalInstance> PortalCreated;
        public static event System.Action<PortalInstance> PortalDestroyed;

        public static event System.Action<PortalInstance, WarppableObject> ObjectEnteredPortal;
        public static event System.Action<PortalInstance, WarppableObject, bool> ObjectExitedPortal;


        public static void RaisePortalCreatedEvent(PortalInstance portal ) { if ( PortalCreated != null ) PortalCreated.Invoke(portal); }
        public static void RaisePortalDestroyedEvent(PortalInstance portal) { if (PortalDestroyed != null) PortalDestroyed.Invoke(portal); }

        public static void RaiseOnObjectEnteredPortal(PortalInstance portal, WarppableObject obj) { if(ObjectEnteredPortal != null) ObjectEnteredPortal.Invoke(portal, obj); }
        public static void RaiseOnObjectExitedPortal(PortalInstance portal, WarppableObject obj, bool crossedThePortal) { if(ObjectExitedPortal != null) ObjectExitedPortal.Invoke(portal, obj, crossedThePortal); }
    }


    public class PortalSet : MonoBehaviour
    {
        [HideInInspector]
        public PortalInstance Green;
        [HideInInspector]
        public PortalInstance Red;

        private void Awake()
        {
            Events.PortalCreated += OnPortalCreatedHandler;
            Events.PortalDestroyed += OnPortalDestroyedHandler;
        }

        private void OnPortalCreatedHandler(PortalInstance instance)
        {
            Utils.Assert(instance != null);
            if (instance.Color == PortalInstance.eColor.Green && Green != null)
                Utils.Assert(false);
            if (instance.Color == PortalInstance.eColor.Red && Red != null)
                Utils.Assert(false);

            if (instance.Color == PortalInstance.eColor.Green)
                Green = instance;
            else
                Red = instance;

            // preventive check
            if (Green != null || Red != null)
                Utils.Assert(Green != Red);
        }
        private void OnPortalDestroyedHandler(PortalInstance instance)
        {
            Utils.Assert(instance != null);
            if (instance.Color == PortalInstance.eColor.Green && Green == null)
                Utils.Assert(false);
            if (instance.Color == PortalInstance.eColor.Red && Red == null)
                Utils.Assert(false);

            if (instance.Color == PortalInstance.eColor.Green)
                Green = null;
            else
                Red = null;

            // preventive check
            if (Green != null || Red != null)
                Utils.Assert(Green != Red);
        }

        public bool IsWormHoleOpen()
        {
            return Green != null && Red != null;
        }
        public PortalInstance GetOtherPortal ( PortalInstance instance )
        {
            if (!IsWormHoleOpen()) return null;
            if (instance == Green) return Red;
            if (instance == Red) return Green;
            return null;
        }

    }
}