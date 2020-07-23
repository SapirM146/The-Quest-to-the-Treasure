using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrow : MonoBehaviour
{
    public GameObject grenadePrefab;
    public float throwForce = 20f;
    public int amountToThrow = 3;
    public float reloadTime = 60f;
    public Text grenadeAmountText;
    public Slider slider;
    

    float countdown;
    int currentAmountToThrow;

    private void Start()
    {
        currentAmountToThrow = amountToThrow;
        countdown = reloadTime;
        slider.maxValue = reloadTime;
        slider.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmountToThrow == 0)
        {
            if (!slider.gameObject.activeInHierarchy)
                slider.gameObject.SetActive(true);

            if (countdown > 0)
                countdown -= Time.deltaTime;
            else
            {
                currentAmountToThrow = amountToThrow;
                countdown = reloadTime;
                slider.gameObject.SetActive(false);
            }
            setBar(reloadTime - countdown);
        }

        if (currentAmountToThrow > 0 && Input.GetMouseButtonDown(1))
        {
            --currentAmountToThrow;
            ThrowGrenade();
        }

        grenadeAmountText.text = currentAmountToThrow.ToString();
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        rb.AddTorque(new Vector3(30, 20, 10));
    }

    void setBar(float amount)
    {
        slider.value = amount;
    }
}
