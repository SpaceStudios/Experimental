using UnityEngine;
using UnityEngine.InputSystem;

public class RobotController : MonoBehaviour
{
    public PlayerInput input;
    public Drivetrain drivetrain;
    public double TrackWidth = 5.0;
    public double TrackLength = 5.0;

    private double DriveX;
    private double DriveY;
    private double DriveRot;

    private InputAction driveX;
    private InputAction driveY;
    private InputAction driveRot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        driveX = InputSystem.actions.FindAction("DriveX");
        driveY = InputSystem.actions.FindAction("DriveY");
        driveRot = InputSystem.actions.FindAction("DriveZ");

        drivetrain.setTrack(TrackWidth,TrackLength);
        drivetrain.setup();
    }

    void onDrive()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xin = driveX.ReadValue<float>();
        float yin = driveY.ReadValue<float>();
        float rin = driveRot.ReadValue<float>();
        drivetrain.driveJoystick(xin,yin,rin);
    }
}
