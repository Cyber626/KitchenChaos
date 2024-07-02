using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    private Player player;

    private float footstepTimer, footstepTimerMax = .1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.IsWalking())
        {

            footstepTimer -= Time.deltaTime;
            if (footstepTimer < 0)
            {
                footstepTimer = footstepTimerMax;

                SoundManager.Instance.PlayFootstepsSound(player.transform.position);
            }
        }
    }

}
