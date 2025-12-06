using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using Unity.Mathematics;
public class SwerveDrive : Drivetrain
{
    public double TrackWidth = 24.0;
    public double TrackLength = 24.0;
    public double maxLinear = 5.0;
    public double maxRot = 10.0;
    public double wheelRadius = 2.0;
    public double wheelWidth = 1.0;

    // Velocities
    private double vX = 0.0;
    private double vY = 0.0;
    private double vRot = 0.0;
    
    [SerializeField] WheelCollider[] wheels;
    private GameObject[] wheelVisualizer;
    public Rigidbody mainRigidbody;
    public GameObject drivetrain;

    private Matrix<double> inverseKinematics;
    private Matrix<double> speeds = Matrix<double>.Build.Dense(3,1);

    public override void setTrack(double width, double length)
    {
        TrackWidth = width;
        TrackLength = length;
    }

    public override void setup()
    {
        print(wheels.Length);
        print("Test");
        inverseKinematics = Matrix<double>.Build.Dense(wheels.Length * 2, 3);
        wheelVisualizer = new GameObject[wheels.Length];
        for (int i=0; i<wheels.Length; i++)
        {   
            wheelVisualizer[i] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            inverseKinematics.SetRow(i*2+0,new double[] {1,0,-((i<2) ? UnitConversion.inchesToMeters(TrackWidth/2) : -UnitConversion.inchesToMeters(TrackWidth/2))});
            inverseKinematics.SetRow(i*2+1,new double[] {0,1,-((i%2==0) ? UnitConversion.inchesToMeters(TrackLength/2) : -UnitConversion.inchesToMeters(TrackLength/2))});
            wheels[i].center = new Vector3((float) ((i<2) ? UnitConversion.inchesToMeters(TrackWidth/2) : -UnitConversion.inchesToMeters(TrackWidth/2)),
                0.0f,
                (float) ((i%2==0) ? UnitConversion.inchesToMeters(TrackLength/2) : -UnitConversion.inchesToMeters(TrackLength/2)));
            wheels[i].radius = (float) UnitConversion.inchesToMeters(wheelRadius);
        }
    }

    private float compensateRotation(float input)
    {
        input = (float) math.floor((input*10)+0.5)/10;
        if (input >= 180f || input <= -180f)
        {
            return input - (math.floor(input/180f) * 180f);
        }
        return input;
    }

    public override void driveJoystick(double x, double y, double rot)
    {
        vX = maxLinear * x;
        vY = maxLinear * y;
        vRot = maxRot * rot;

        if (vX != 0 || vY != 0 || vRot != 0)
        {
            speeds.SetColumn(0,new double[] {vX,vY,vRot});
            Matrix<double> moduleStates = inverseKinematics.Multiply(speeds);
            for (int i=0; i<wheels.Length; i++)
            {
                double mX = moduleStates.At(i*2,0);
                double mY = moduleStates.At(i*2+1,0);
                double speed = math.sqrt((mX * mX) + (mY * mY));
                double mRot = math.degrees(math.atan2(mY,mX));
                float actualRotation = compensateRotation(wheels[i].steerAngle);
                if (mRot - actualRotation >= 180)
                {
                    speed *= -1;
                }
                mRot = compensateRotation((float) mRot - 90f);
                wheels[i].steerAngle = (float) mRot;
                wheels[i].rotationSpeed = (float) speed;
            }
        }
    }
}
