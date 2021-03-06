using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningBarrelBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        PlayerBehaviour player = collider.GetComponent<PlayerBehaviour>();
        if (player != null) {
            player.masksToThrow = GameController.instance.parameters.MAX_MASKS[OptionsController.maskCapacity];    // TODO: Parameter file
            AudioController.instance.PlaySFX("inhale");
        }
    }
}
