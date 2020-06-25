using TMPro;
using UnityEngine;

public class KeycardScript : MonoBehaviour
{

    public GameObject PickupText;
    public KeyCardManager keyCard;

    public TextMeshProUGUI objectiveTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupText.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                keyCard.YellowKeyCard = true;
                objectiveTxt.text = "Escape through the Green Door";
                Destroy(this.gameObject);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        PickupText.SetActive(false);
    }

}
