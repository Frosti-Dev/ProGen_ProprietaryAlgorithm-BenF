using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;

    Vector2 movementDirection;

    public void Intialize(Vector2 direction)
    {
        Debug.Log(direction);

        movementDirection = direction;


        transform.up = direction;
        
    }

    IEnumerator waitToDisable()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }

    private void OnEnable()
    {
        StartCoroutine(waitToDisable());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        //Debug.Log($"X: {playerController.lastMoveInputX}, Y {playerController.lastMoveInputY}");
         gameObject.transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damageableComponent))
        {
            gameObject.SetActive(false);
            damageableComponent.TakeDamage(1);
        }

    }
}
