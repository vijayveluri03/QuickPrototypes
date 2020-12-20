using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portal
{
    public class WarppableObject : MonoBehaviour
    {

        public WarppableObject _myClone { get; private set; } = null;
        public bool AmIClone { get { return _amIClone; } }

        public void Awake()
        {
            playerCollider = GetComponent<Collider>();
        }

        public WarppableObject CloneObject ()
        {
            Utils.Assert(_myClone == null);

            GameObject clonedObj = GameObject.Instantiate(gameObject, Vector3.one * -999.0f, gameObject.transform.rotation, transform.parent);
            _myClone = clonedObj.GetComponent<WarppableObject>();
            _myClone._amIClone = true;
            _myClone.playerCollider.enabled = false;
            
            return _myClone;
        }
        public void SwapWithClone()
        {
            Utils.Assert(_myClone != null);
            Vector3 clonePosition = _myClone.transform.position;
            Quaternion cloneRot = _myClone.transform.rotation;

            _myClone.transform.position = transform.position;
            _myClone.transform.rotation = transform.rotation;

            transform.position = clonePosition;
            transform.rotation = cloneRot;
        }

        public void DestroyClone()
        {
            GameObject.Destroy(_myClone.gameObject);
            _myClone = null;
        }

        [SerializeField]
        private bool _amIClone = false;
        private Collider playerCollider = null;

    }
}