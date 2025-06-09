using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float parallaxFactor = 0.5f;
    [SerializeField] private float textureUnitSizeX;

    private Vector3 lastCameraPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        lastCameraPosition = cameraTransform.position;

        if (textureUnitSizeX == 0f)
        {
            // Calculate texture unit size based on the sprite's pixels per unit and scale
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit * transform.localScale.x;
        }
    }

    void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0);
        lastCameraPosition = cameraTransform.position;

        // Reposition background to loop
        float cameraOffset = cameraTransform.position.x - transform.position.x;
        if (Mathf.Abs(cameraOffset) >= textureUnitSizeX)
        {
            float offset = cameraOffset > 0 ? textureUnitSizeX : -textureUnitSizeX;
            transform.position += new Vector3(offset, 0, 0);
        }
    }
}
