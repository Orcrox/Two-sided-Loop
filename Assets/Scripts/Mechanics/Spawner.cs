using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject mechanic;
    [SerializeField] private float magnitude;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (other.GetComponent<Player>().isUsingPower()) {
                GameObject newMechanic = Instantiate(mechanic, transform.position, transform.rotation);
                if (newMechanic.CompareTag("Ladder")) {
                    newMechanic.GetComponent<SpriteRenderer>().size = new Vector2(1, magnitude);
                }
                Destroy(gameObject);
            }
        }
    }
}
