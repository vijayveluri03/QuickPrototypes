using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portal
{
    [RequireComponent(typeof(WarppableObject))]
    public class WarpableObjectMovement : MonoBehaviour
    {
        private WarppableObject obj;
        public Vector3 direction;
        public float speed = 5;

        public void Start()
        {
            obj = GetComponent<WarppableObject>();
        }

        public void Update()
        {
            if (obj == null) return;
            if (obj.AmIClone) return;

            transform.position += direction * speed * Time.deltaTime;
        }
    }
}