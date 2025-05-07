using UnityEngine;
using UnityEngine.UI;


namespace Assets.SimpleSpinner
{
    [RequireComponent(typeof(Image))]
    public class SimpleSpinner : MonoBehaviour
    {
        [Header("Rotation")]
        [Tooltip("Value in Hz (revolutions per second).")]
        public float RotationSpeed = 1f; // the higher the value, the faster it spins
        public AnimationCurve RotationAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [Header("Rainbow (Optional)")]
        public bool Rainbow = false;
        [Range(-10, 10)] public float RainbowSpeed = 0.25f;
        [Range(0, 1)] public float RainbowSaturation = 1f;
        public AnimationCurve RainbowAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);

        private Image image;
        private float period;
        private float startTime;
        private bool spinning = false;

        public float duration = 0.35f; // spin duration in seconds

        private void Awake() {
            image = GetComponent<Image>();
            gameObject.SetActive(false);
        }

        public void PlaySpin() {
            period = 0f;
            startTime = Time.time;
            spinning = true;
            transform.localEulerAngles = Vector3.zero;
            gameObject.SetActive(true);
        }

        private void Update() {
            if (!spinning) return;

            float elapsed = Time.time - startTime;

            if (elapsed >= duration) {
                spinning = false;
                gameObject.SetActive(false);
                transform.localEulerAngles = Vector3.zero;
                return;
            }

            float normalizedTime = (elapsed * RotationSpeed + period) % 1;
            transform.localEulerAngles = new Vector3(0, 0, -360 * RotationAnimationCurve.Evaluate(normalizedTime));

            if (Rainbow) {
                image.color = Color.HSVToRGB(RainbowAnimationCurve.Evaluate(normalizedTime), RainbowSaturation, 1);
            }
        }
    }
}