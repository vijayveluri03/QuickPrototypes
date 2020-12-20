using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoggyLandscape
{
    

    public class SpawnLandscape : MonoBehaviour
    {
        [Header("Skyscrapper props")]
        public FloatRange towerHeight;
        public ColorRange towerColorRangeBasedOnHeight;
        public Transform towerReference;    // should contain a meshrender and a simple material with "_Color" property 

        [Header("Landscape")]
        public Vector2Int dimentions;
        public Transform centerAnchor;
        public Vector2Int blockDimentions;
        public float landscapeDensityFactor;

        void Start()
        {
            if (towerReference == null) Utils.ThrowException();
            if ( !towerReference.GetComponent<TowerBehaviour>()) Utils.ThrowException();
            
            BuildLandscape();
        }

        void BuildLandscape()
        {
            for (int x = -dimentions.x; x < dimentions.x; x++)
            {
                for (int y = -dimentions.y; y < dimentions.y; y++)
                {
                    // empty space for roads 
                    if (x % blockDimentions.x == 0 || y % blockDimentions.y == 0)
                        continue;

                    if (UnityEngine.Random.Range((float)0.0f, (float)1.0) < landscapeDensityFactor)
                    {
                        float height = towerHeight.GetRandom();
                        float heightFactor = towerHeight.InvLerp(height);
                        Color color = towerColorRangeBasedOnHeight.Lerp(heightFactor);
                        CreateATower(new Vector2Int(x, y), height, color);
                    }
                }
            }
        }
        void CreateATower(Vector2Int position, float height, Color color)
        {
            // Simple instantiation for now 
            GameObject tower = GameObject.Instantiate(towerReference).gameObject;
            TowerBehaviour towerBehaviour = tower.GetComponent<TowerBehaviour>();

            tower.transform.parent = centerAnchor;
            tower.transform.position = new Vector3(position.x, 0, position.y);
            towerBehaviour.SetHeight(height);

            tower.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        void Update()
        {

        }
    }
}