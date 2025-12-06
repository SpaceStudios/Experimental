using UnityEngine;

public abstract class Drivetrain : MonoBehaviour
{
    public abstract void setTrack(double width, double length);
    public abstract void setup();
    public abstract void driveJoystick(double x, double y, double rot);
}
