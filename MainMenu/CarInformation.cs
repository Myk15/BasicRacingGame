using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarInformation : MonoBehaviour
    //Sets up all the information which player can change
{
    private bool Android; // so game runs on android
    
    public Text ShieldGreen;
    public Text AccelerationGreen;
    public Text SpeedGreen;

    public Slider ShieldSlider;
    public Text ShieldSliderValue;
    public Slider AccelerationSlider;

    private float AccDefault;
    private float ShieldDefault;
    private float MaxSpeedDefault;

    private float AccDifference;
    private float ShieldDifference;
    private float MaxSpeedDifference;

    public Text AccelerationSliderValue;
    public Slider MaxSpeedSlider;
    public Text MaxSpeedSliderValue;

    public Text RemainingAttributesText;

    

    public float MaxAttributePoints;
    private float RemainingAttributePoints;

    public GameObject CarStats; //used to get default stats of the car
    private SetupCarAttributes SetupCar;
    public GameObject ErrorObject; //used to enable and disable text
    public Text ErrorMessage;


    private void Awake()
    {
#if UNITY_ANDROID
        Android = true;
#endif
        SetupDefaults();
    }

    public void SetupDefaults()
    {
        RemainingAttributePoints = MaxAttributePoints; //how many points we can use

        SetupCar = CarStats.GetComponent<SetupCarAttributes>(); 

        ShieldSlider.GetComponent<Slider>().maxValue = SetupCar.MaxShield;
        AccelerationSlider.GetComponent<Slider>().maxValue = SetupCar.MaxAcceleration;
        MaxSpeedSlider.GetComponent<Slider>().maxValue = SetupCar.TopSpeed;

        ShieldDefault = ShieldSlider.GetComponent<Slider>().minValue;
        AccDefault = AccelerationSlider.GetComponent<Slider>().minValue;
        MaxSpeedDefault = MaxSpeedSlider.GetComponent<Slider>().minValue;

        ShieldGreen.text = SetupCar.MaxShield.ToString();
        AccelerationGreen.text = SetupCar.MaxAcceleration.ToString();
        SpeedGreen.text = SetupCar.TopSpeed.ToString();

        
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        SetSliderInformation();
        CheckRemaingAttributePoints();

        
    }
    public void SetSliderInformation()
    {
        ShieldSliderValue.text = ShieldSlider.GetComponent<Slider>().value.ToString();
        AccelerationSliderValue.text = AccelerationSlider.GetComponent<Slider>().value.ToString();
        MaxSpeedSliderValue.text = MaxSpeedSlider.GetComponent<Slider>().value.ToString();
        ShieldDifference = ShieldDefault - ShieldSlider.GetComponent<Slider>().value;
        MaxSpeedDifference = MaxSpeedDefault - MaxSpeedSlider.GetComponent<Slider>().value;
        AccDifference = AccDefault - AccelerationSlider.GetComponent<Slider>().value;
    }

    public void CheckRemaingAttributePoints() //checks to see if the bars have changed, if they have update bars
    {
        float temp = RemainingAttributePoints;
        float temp2 = MaxAttributePoints + ShieldDifference;
        temp2 += MaxSpeedDifference;
        temp2 += AccDifference;

        if(temp != temp2)
        {
            RemainingAttributePoints = temp2;
        }
        RemainingAttributesText.text = RemainingAttributePoints.ToString();
    }

    public void ShowErrorMessage(string message)
    {
        ErrorObject.SetActive(true);
        ErrorMessage.text = message;

    }

    public void StartRace()
    {
        if (!Android)
        {
            if (RemainingAttributePoints == 0)
            {
                SetupPlayerAttributes();

            }
            else if (RemainingAttributePoints > 0)
            {
                ShowErrorMessage("Please use all 200 points");
            }
            else if (RemainingAttributePoints < 0)
            {
                ShowErrorMessage("You have used too many points");
            }

        }
        else
        {
            SceneManager.LoadScene("RaceMap");
        }
            

    }

    public void SetupPlayerAttributes() //get the slider values and send to be written to the json file
    {
        float TopSpeed = MaxSpeedSlider.GetComponent<Slider>().value;
        float MaxShield = ShieldSlider.GetComponent<Slider>().value;
        float MaxAcceleration = AccelerationSlider.GetComponent<Slider>().value;
       

        ReadAndWriteCarAttributes.Write(MaxShield,TopSpeed,MaxAcceleration);

        SceneManager.LoadScene("RaceMap");
    }
}
