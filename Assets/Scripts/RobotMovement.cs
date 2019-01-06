﻿using UnityEngine;
using UnityEngine.Events;

public class RobotMovement : ConfigurableComponent {

    /// <summary>
    /// Wrapper class for RobotMovement settings
    /// </summary>
    [System.Serializable]
    public class Settings : ComponentSettings {
        public float rotationSpeed;
        public float movementSpeed;

        public bool isJoystickEnabled;
        public bool isReverseEnabled;
        public bool isForwardEnabled;
        public bool isRightEnabled;
        public bool isLeftEnabled;

        public Settings(
            float rotationSpeed,
            float movementSpeed,
            bool isJoystickEnabled,
            bool isReverseEnabled,
            bool isForwardEnabled,
            bool isRightEnabled,
            bool isLeftEnabled
            ) {

            this.rotationSpeed = rotationSpeed;
            this.movementSpeed = movementSpeed;

            this.isForwardEnabled = isForwardEnabled;
            this.isJoystickEnabled = isJoystickEnabled;
            this.isLeftEnabled = isLeftEnabled;
            this.isRightEnabled = isRightEnabled;
            this.isReverseEnabled = isReverseEnabled;
        }
    }

    private Rigidbody rigidBody;

    //default values
    public float rotationSpeed;
    public float movementSpeed;
    public float requiredViewingAngle;

    public bool isJoystickEnabled;
    public bool isReverseEnabled;
    public bool isForwardEnabled;
    public bool isRightEnabled;
    public bool isLeftEnabled;

    
    protected override void Awake() {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        float vertical;
        float horizontal;

        // using joy stick
        if (isJoystickEnabled) {
            vertical = JoystickController.vertical;
            horizontal = JoystickController.horizontal;
        }
        else {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }

        if (ShouldRotate(horizontal)) {
            Quaternion rotateBy = Quaternion.Euler(0, horizontal * rotationSpeed * Time.deltaTime, 0);
            Debug.Log(rotateBy);
            rigidBody.MoveRotation(transform.rotation * rotateBy);
        }

        if (ShouldMove(vertical)) {
            Vector3 moveBy = transform.forward * vertical * movementSpeed * Time.deltaTime;
            Debug.Log(moveBy);
            rigidBody.MovePosition(transform.position + moveBy);
        }
    }

    /// <summary>
    /// Checks if object should rotate either left, right or both.
    /// </summary>
    /// <returns>True if should rotate, false if not</returns>
    private bool ShouldRotate(float horizontal) {
        return (horizontal < 0 && isLeftEnabled) ||
            (horizontal > 0 && isRightEnabled);
    }

    private bool ShouldMove(float vertical) {
        return (vertical < 0 && isReverseEnabled) ||
            (vertical > 0 && isForwardEnabled);
    }

    public override ComponentSettings GetCurrentSettings() {
        Settings settings = new Settings(rotationSpeed, movementSpeed, 
            isJoystickEnabled, isReverseEnabled, isForwardEnabled, 
            isRightEnabled, isLeftEnabled);

        return settings;
    }

    protected override void ApplySettings(ComponentSettings settings) {
        Settings applySettings = (Settings)settings;

        isJoystickEnabled = applySettings.isJoystickEnabled;
        isForwardEnabled = applySettings.isForwardEnabled;
        isReverseEnabled = applySettings.isReverseEnabled;
        isLeftEnabled = applySettings.isLeftEnabled;
        isRightEnabled = applySettings.isRightEnabled;

        movementSpeed = applySettings.movementSpeed;
        rotationSpeed = applySettings.rotationSpeed;
    }

    public override ComponentSettings GetDefaultSettings() {
        return new Settings(5f, 5f, true, true, true, true, true);
    }

    public override string GetSettingsID() {
        return typeof(Settings).FullName;
    }
}