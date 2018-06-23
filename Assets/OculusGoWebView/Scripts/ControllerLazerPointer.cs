using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ControllerLazerPointer : MonoBehaviour {
    public float maxLength = 10.0f;

    LineRenderer lineRenderer;
    Transform mainCamera;
    Vector3[] positions = new Vector3[2];

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        mainCamera = Camera.main.transform;
    }

    void Update() {
        var controller = OVRInput.GetActiveController();
        var position = OVRInput.GetLocalControllerPosition(controller);
        var rotation = OVRInput.GetLocalControllerRotation(controller);

        var headPosition = mainCamera.position;
        positions[0] = headPosition + position;
        positions[1] = rotation * Vector3.forward * maxLength + positions[0];
        lineRenderer.SetPositions(positions);
    }
}
