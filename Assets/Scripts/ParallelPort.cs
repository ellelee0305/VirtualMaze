﻿using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class ParallelPort : ConfigurableComponent {
    private bool parallelflip = false;
    public int portHexAddress;

    [DllImport("InpOutx64.dll", EntryPoint = "Out32")]
    private static extern void Out32(int address, int value);

    [Serializable]
    public class Settings : ComponentSettings {
        public int portHexAddress;
        public Settings(int portHexAddress) {
            this.portHexAddress = portHexAddress;
        }
    }

    public static void TryOut32(int address, int value) {
        try {
            Out32(address, value);
        }
        catch (System.DllNotFoundException e) {
            Debug.LogException(e);
        }
    }

    public void WriteTrigger(int value) {
        if (portHexAddress == -1) {
            Out32(portHexAddress, value);
        }
    }

    public void SimpleTest() {
        if (parallelflip) {
            TryOut32(portHexAddress, 255);
        }
        else {
            TryOut32(portHexAddress, 0);
        }

        parallelflip = !parallelflip;
    }

    public override String GetSettingsID() {
        return typeof(Settings).FullName;
    }

    public override ComponentSettings GetDefaultSettings() {
        return new Settings(0);
    }

    public override ComponentSettings GetCurrentSettings() {
        return new Settings(portHexAddress);
    }

    protected override void ApplySettings(ComponentSettings loadedSettings) {
        Settings s = (Settings)loadedSettings;

        portHexAddress = s.portHexAddress;
    }
}