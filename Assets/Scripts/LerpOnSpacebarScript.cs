﻿using UnityEngine;
using System.Collections;

public class LerpOnSpacebarScript : MonoBehaviour
{
    /// <summary>
    /// The time taken to move from the start to finish positions
    /// </summary>
    public float timeTakenDuringLerp = 1f;

    /// <summary>
    /// How far the object should move when 'space' is pressed
    /// </summary>
    public float distanceToMove = 10;

    //Whether we are currently interpolating or not
    private bool _isLerping;

    //The start and finish positions for the interpolation
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    //The Time.time value when we started the interpolation
    private float _timeStartedLerping;

    /// <summary>
    /// Called to begin the linear interpolation
    /// </summary>
    void StartLerping()
    {
        _isLerping = true;
        _timeStartedLerping = Time.time;

        //We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
        _startPosition = transform.position;
        _endPosition = transform.position + Vector3.forward * distanceToMove;
    }

    void Update()
    {
        //When the user hits the spacebar, we start lerping
        if (Input.GetKey(KeyCode.Space))
        {
            StartLerping();
        }
    }

    //We do the actual interpolation in FixedUpdate(), since we're dealing with a rigidbody
    void FixedUpdate()
    {
        if (_isLerping)
        {
            //We want percentage = 0.0 when Time.time = _timeStartedLerping
            //and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
            //In other words, we want to know what percentage of "timeTakenDuringLerp" the value
            //"Time.time - _timeStartedLerping" is.
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            //Perform the actual lerping.  Notice that the first two parameters will always be the same
            //throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
            //to start another lerp)
            transform.position = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);

            //When we've completed the lerp, we set _isLerping to false
            if (percentageComplete >= 1.0f)
            {
                _isLerping = false;
            }
        }
    }
}