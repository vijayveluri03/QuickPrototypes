using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private const int CIRCLE_ANGLE = 360;
    [Header("Minute marker")]
    [SerializeField] private Transform minuteMarkerParent;
    [SerializeField] private GameObject minuteMarkerObj;
    [SerializeField] private Vector3 minuteMarkerDistanceFromParent;
    [SerializeField] private int totalMinuteMarkersCount;

    [Header("Hands")]
    [SerializeField] private GameObject hourHandGO;
    [SerializeField] private GameObject minuteHandGO;
    [SerializeField] private GameObject secondHandGO;
    private float hoursAngle, minuteAngle, secondsAngle;

    // Start is called before the first frame update
    void Start()
    {
        CreateAndArrangeMinuteMarkers();
    }

    // Update is called once per frame
    void Update()
    {
		GetAnglesForHands(out hoursAngle, out minuteAngle, out secondsAngle);

        hourHandGO.transform.localRotation = Quaternion.AngleAxis(hoursAngle, Vector3.forward);
        minuteHandGO.transform.localRotation = Quaternion.AngleAxis(minuteAngle, Vector3.forward);
        secondHandGO.transform.localRotation = Quaternion.AngleAxis(secondsAngle, Vector3.forward);
    }

    private void CreateAndArrangeMinuteMarkers()
    {
        if (totalMinuteMarkersCount > 0)
        {
            float angleBetweenMinuteMarkets = (float)(CIRCLE_ANGLE / totalMinuteMarkersCount);
            for (int i = 0; i < totalMinuteMarkersCount; i++)
            {
                GameObject minuteMarkerGO = GameObject.Instantiate(minuteMarkerObj, minuteMarkerParent) as GameObject;
                minuteMarkerGO.transform.localPosition = minuteMarkerDistanceFromParent;
                minuteMarkerGO.transform.localRotation = Quaternion.identity;

                float rotationAngle = GetAngleInClockwiseDirection(angleBetweenMinuteMarkets * i);
                minuteMarkerGO.transform.localPosition = RotateVectorAroundZ(minuteMarkerGO.transform.localPosition, rotationAngle);

                minuteMarkerGO.transform.localRotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
            }
        }
    }

    private void GetAnglesForHands ( out float angleForHoursHand, out float angleForMinutesHand, out float angleForSecondsHand )
    {
        System.DateTime time = GetTime();

        angleForSecondsHand = (float) ( time.Second * CIRCLE_ANGLE / 60.0 );
        angleForMinutesHand = (float) ( time.Minute * CIRCLE_ANGLE / 60.0 );
		angleForHoursHand = (float)((time.Hour > 12 ? 24 - time.Hour : time.Hour) * CIRCLE_ANGLE / 12.0);

		angleForSecondsHand = GetAngleInClockwiseDirection(angleForSecondsHand);
        angleForMinutesHand = GetAngleInClockwiseDirection(angleForMinutesHand);
        angleForHoursHand = GetAngleInClockwiseDirection(angleForHoursHand);
    }
    private System.DateTime GetTime () 
    {
        return System.DateTime.Now;
    }

#region Utility
    private float GetAngleInClockwiseDirection(float zAngle)
    {
        return -zAngle; // In LHS , Z is opposite to us, so it turns in counter clockwise direction. 
    }
    private Vector3 RotateVectorAroundZ(Vector3 vector, float angle_deg)
    {
        // we are getting column Major matrix here 
        float[,] rotMat = GetRotationMatrix(angle_deg);

        // transformedVec = Vector * Column Major Matrix 
        Vector3 transformedVec = new Vector3
        (
            vector.x * rotMat[0, 0] + vector.y * rotMat[1, 0] + vector.z * rotMat[2, 0],
            vector.x * rotMat[0, 1] + vector.y * rotMat[1, 1] + vector.z * rotMat[2, 1],
            vector.x * rotMat[0, 2] + vector.y * rotMat[1, 2] + vector.z * rotMat[2, 2]
        );
        return transformedVec;
    }
    private float[,] GetRotationMatrix(float rotationAngleInZ_deg)
    {
        float c = Mathf.Cos(Mathf.Deg2Rad * rotationAngleInZ_deg);
        float s = Mathf.Sin(Mathf.Deg2Rad * rotationAngleInZ_deg);

        // This is a rotation matrix around Z and its in a column Major matrix format. 
        float[,] rotMat = {
                            { c , s, 0 },
                            { -s, c, 0 },
                            { 0, 0, 1 }
                        };
        return rotMat;
    }
#endregion
}
