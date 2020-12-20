using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portal
{
    [RequireComponent(typeof(WarppableObject))]
    public class WarpableObjectBasicMovement : MonoBehaviour
    {
        private WarppableObject obj;
        public float speed = 5;

        public void Start()
        {
            obj = GetComponent<WarppableObject>();
        }

        public void Update()
        {
            if (obj == null) return;
            if (obj.AmIClone) return;

            Vector3 direction = Vector3.zero;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                direction = transform.forward;
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                direction = -transform.forward;
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                direction = -transform.right;
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                direction = transform.right;
            else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.PageUp))
                direction = transform.up;
            else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.PageDown))
                direction = -transform.up;


            transform.position += direction * speed * Time.deltaTime;
        }
    }
}