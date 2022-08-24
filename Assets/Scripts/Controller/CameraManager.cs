using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject actionCameraObject;
    void Start()
    {
        BaseAction.OnAnyActionStarted += OnAnyActionStarted;
        BaseAction.OnAnyActionComplete += OnAnyActionCompleted;
        HideActionCamera();
    }
    void ShowActionCamera()
    {
        actionCameraObject.SetActive(true);
    }
    void HideActionCamera()
    {
        actionCameraObject.SetActive(false);
    }
    void OnAnyActionStarted(BaseAction thisAction)
    {
        switch (thisAction)
        {
            case ShootAction shootAction:
                Unit shooterUnit = shootAction.GetUnit();
                Unit targetUnit = shootAction.GetTarget();
                Vector3 cameraPositionHeight = Vector3.up * 1.7f;
                Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;
                float ShoulderOffset = 0.5f;
                Vector3 shoulderOffset = Quaternion.Euler(0f, 90f, 0f) * shootDir * ShoulderOffset;
                Vector3 actionCameraPosition =
                shooterUnit.GetWorldPosition() + cameraPositionHeight + shoulderOffset + (shootDir * -1);
                actionCameraObject.transform.position = actionCameraPosition;
                actionCameraObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraPositionHeight);
                ShowActionCamera();
                break;
        }
    }
    void OnAnyActionCompleted(BaseAction thisAction)
    {
        switch (thisAction)
        {
            case ShootAction shootAction:
                HideActionCamera();
                break;
        }
    }
}
