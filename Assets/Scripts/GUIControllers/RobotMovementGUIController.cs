﻿using UnityEngine;
using SREYELINKLib;
using UnityEngine.UI;

public class RobotMovementGUIController : DataGUIController {
    //drag and drop respective GameObjects with corresponding components in Unity Editor.
    public RobotMovement robotMovement;

    public DescriptiveSlider movementSpeedSlider;
    public DescriptiveSlider rotationSpeedSlider;

    public Toggle isJoystickEnabledToggle;
    public Toggle isForwardEnabledToggle;
    public Toggle isReverseEnabledToggle;
    public Toggle isLeftEnabledToggle;
    public Toggle isRightEnabledToggle;

    private void Awake() {
        rotationSpeedSlider.onValueChanged.AddListener(OnRotationSpeedChanged);
        movementSpeedSlider.onValueChanged.AddListener(OnMovementSpeedChanged);

    }

    public override void UpdateSettingsGUI() {
        movementSpeedSlider.value = robotMovement.movementSpeed;
        rotationSpeedSlider.value = robotMovement.rotationSpeed;

        isJoystickEnabledToggle.isOn = robotMovement.isJoystickEnabled;
        isForwardEnabledToggle.isOn = robotMovement.isForwardEnabled;
        isReverseEnabledToggle.isOn = robotMovement.isReverseEnabled;
        isLeftEnabledToggle.isOn = robotMovement.isLeftEnabled;
        isRightEnabledToggle.isOn = robotMovement.isRightEnabled;
    }

    //listeners for UI GameObjects, drag and drop in respective Child UI Components
    public void OnRotationSpeedChanged(float value) {
        robotMovement.rotationSpeed = value;
    }

    public void OnMovementSpeedChanged(float value) {
        robotMovement.movementSpeed = value;
    }

    public void OnJoystickEnableToggled(bool value) {
        robotMovement.isJoystickEnabled = value;
    }

    public void OnForwardEnableToggled(bool value) {
        robotMovement.isForwardEnabled = value;
    }

    public void OnReverseEnableToggled(bool value) {
        robotMovement.isReverseEnabled = value;
    }

    public void OnLeftEnableToggled(bool value) {
        robotMovement.isLeftEnabled = value;
    }

    public void OnRightEnableToggled(bool value) {
        robotMovement.isRightEnabled = value;
    }
    //End of Listeners for UI Objects


}
