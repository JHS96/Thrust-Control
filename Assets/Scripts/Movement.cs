using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float mainThrust = 100f;
  [SerializeField] float rotationThrust = 5f;
  [SerializeField] AudioClip mainEngine;
  [SerializeField] ParticleSystem mainEngineParticles;
  [SerializeField] ParticleSystem leftFrontThrustParticles;
  [SerializeField] ParticleSystem leftRearThrustParticles;
  [SerializeField] ParticleSystem rightFrontThrustParticles;
  [SerializeField] ParticleSystem rightRearThrustParticles;

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
      StartThrusting();
    }
    else
    {
      StopThrusting();
    }
  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      RotateLeft();
    }
    else if (Input.GetKey(KeyCode.D))
    {
      RotateRight();
    }
    else
    {
      StopRotating();
    }
  }

  void StartThrusting()
  {
    rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    if (!audioSource.isPlaying)
    {
      audioSource.PlayOneShot(mainEngine);
    }
    if (!mainEngineParticles.isPlaying)
    {
      mainEngineParticles.Play();
    }
  }

  void StopThrusting()
  {
    mainEngineParticles.Stop();
    audioSource.Stop();
  }

  void RotateLeft()
  {
    ApplyRotation(rotationThrust);
    if (!rightFrontThrustParticles.isPlaying && !rightRearThrustParticles.isPlaying)
    {
      rightFrontThrustParticles.Play();
      rightRearThrustParticles.Play();
    }
  }

  void RotateRight()
  {
    ApplyRotation(-rotationThrust);
    if (!leftFrontThrustParticles.isPlaying && !leftRearThrustParticles.isPlaying)
    {
      leftFrontThrustParticles.Play();
      leftRearThrustParticles.Play();
    }
  }

  void StopRotating()
  {
    rightFrontThrustParticles.Stop();
    rightRearThrustParticles.Stop();
    leftFrontThrustParticles.Stop();
    leftRearThrustParticles.Stop();
  }

  void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; // Freeze physics system rotation to enable manual rotation.
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false; // Unfreeze rotation so physics system rotation can take over.
  }
}
