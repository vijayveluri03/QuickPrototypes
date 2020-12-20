using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portal
{
    [RequireComponent(typeof(WarppableObject))]
    public class WarpableObjectRotation : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            obj = GetComponent<WarppableObject>();
        }

        // Update is called once per frame
        void Update()
        {
            if (obj == null) return;
            if (obj.AmIClone) return;

            transform.rotation *= Quaternion.AngleAxis(_anglePerSecond * Time.deltaTime, _axis);
        }

        private WarppableObject obj;
        [SerializeField] private Vector3 _axis;
        [SerializeField] private float _anglePerSecond;


    }
}