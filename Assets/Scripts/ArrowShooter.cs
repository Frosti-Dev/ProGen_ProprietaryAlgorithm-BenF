using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowShooter : MonoBehaviour
{
    public Arrow arrow;
    public Arrow chargedArrow;
    public Transform firePoint;

    public PlayerController playerController;

    public float fullChargeTime;
    public float chargedTime;
    private bool isCharging;

    [Header("Particle")]
    public ParticleSystem chargingParticles;

    public void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            chargingParticles.Play();
            playerController.moveSpeed/= 2;
            isCharging = true;
            chargedTime = Time.time;
            Debug.Log("Charging");
        }
        else
        {
            if (isCharging)
            {
                chargingParticles.Stop();
                playerController.moveSpeed*= 2;
                float chargeDuration = Time.time - chargedTime;
                FireShot(chargeDuration);
                isCharging = false;
                

            }
        }
        
    }

    private void FireShot(float duration)
    {
        if (duration>= fullChargeTime)
        {
            Arrow GO = Instantiate(chargedArrow, firePoint.position, firePoint.rotation);
            GO.Intialize(playerController.lastMoveInput);
        }

        else
        {
            Arrow GO = Instantiate(arrow, firePoint.position, firePoint.rotation);

            GO.Intialize(playerController.lastMoveInput);
        }
    }
}
