using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 0.5f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 0.5f;

    [Header("Destroy")]
    [SerializeField] AudioClip destroyClip;
    [SerializeField][Range(0f, 1f)] float destroyVolume = 0.5f;

    static AudioPlayer instance;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayDestroyClip()
    {
        PlayClip(destroyClip, destroyVolume);
    }

    private void PlayClip(AudioClip audioClip, float volume)
    {
        if (audioClip == null)
        {
            return;
        }

        AudioSource.PlayClipAtPoint(audioClip,
                                    Camera.main.transform.position,
                                    volume);
    }
}
