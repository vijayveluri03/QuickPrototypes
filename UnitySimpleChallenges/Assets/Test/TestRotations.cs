using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotations : MonoBehaviour
{
    public Transform parent;
    public Transform child;
    public Transform grandChild;

    // Start is called before the first frame update
    void Start()
    {

        parent.rotation = Quaternion.AngleAxis(45, parent.up);
        Debug.Log("parent right " + parent.right + " child.right " + child.right);

        //child.localRotation = Quaternion.AngleAxis(45, child.right);
        //child.rotation = Quaternion.AngleAxis(45, child.right) * child.rotation;

        Debug.Log("parent right " + parent.right + " child.right " + child.right);

    }

    // Update is called once per frame
    void Update()
    {
        
        if ( Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log($"local rotation of child : {child.transform.localRotation.eulerAngles} " +
                        $" local of Parent : {parent.transform.localRotation.eulerAngles} " +

                        $" Global of child : {child.transform.rotation.eulerAngles} "
                ); ;

            Debug.Log( $" solution  = {(parent.transform.localRotation * child.transform.localRotation).eulerAngles}");
            child.rotation = (child.transform.localRotation * parent.transform.localRotation);
            //Debug.Log($"potential solution B = {(child.transform.localRotation * parent.transform.localRotation).eulerAngles}");
        }

        if(Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.LogError(child.right);
            child.localRotation = Quaternion.AngleAxis(45 * Time.deltaTime * 10, child.right) * child.localRotation;
            //child.rotation = child.rotation * Quaternion.AngleAxis(45 * Time.deltaTime, Vector3.right);
        }
        else if ( Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogError(child.right);
            child.rotation = child.rotation * Quaternion.AngleAxis(45, child.right);
            Debug.LogError(child.right);
        }

    }
}
