using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    private float height;
    private float currentHeight;
    private float towerRaiseSpeed = 1;
    private bool raisingTower = false;
    public void SetHeight(float height)
    {
        this.height = height;
        currentHeight = 0.1f;
        raisingTower = true; 
    }

    // Update is called once per frame
    void Update()
    {
        RaiseTheTower();
    }


    private void RaiseTheTower ()
    {
        if (!raisingTower)
            return;

        currentHeight += towerRaiseSpeed * Time.deltaTime;
        if (currentHeight > height)
        {
            raisingTower = false;
        }
        currentHeight = Mathf.Min(currentHeight, height);

        Vector3 scale = transform.localScale;
        Vector3 pos = transform.localPosition;

        scale.y = currentHeight;
        pos.y = currentHeight / 2;

        transform.localScale = scale;
        transform.localPosition = pos;
    }
}
