using UnityEngine;
using UnityEngine.UI;

public class SetupCarAttributes : MonoBehaviour //this is used to setup the car information
{
    
    public float MaxShield;
    public float MaxAcceleration;
    public float TopSpeed;

   
    public SetupCarAttributes(float S, float A,float T)
    {
        MaxShield = S;
        MaxAcceleration = A;
        TopSpeed = T;
    }
}
