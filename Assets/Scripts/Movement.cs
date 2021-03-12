using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float mainThrust = 100f;
  [SerializeField] float rotationThrust = 5f;
  [SerializeField] AudioClip mainEngine;

  Rigidbody rb;
  AudioSource audioSource;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    ProcessThrust();
    ProcessRotation();
  }

  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
      if (!audioSource.isPlaying)
      {
        audioSource.PlayOneShot(mainEngine);
      }
    }
    else
    {
      audioSource.Stop();
    }
  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      ApplyRotation(rotationThrust);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      ApplyRotation(-rotationThrust);
    }
  }

  void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; // Freeze physics system rotation to enable manual rotation.
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false; // Unfreeze rotation so physics system rotation can take over.
  }
}
