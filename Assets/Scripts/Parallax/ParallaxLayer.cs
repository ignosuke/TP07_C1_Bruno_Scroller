using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private ParallaxLayerSo layerData;
    [SerializeField] private Transform cameraTransform;

    private Transform[] pieces;
    private float followFactor;
    private float pieceWidth;
    private float verticalOffset;
    private float lastCameraX;

    private void Awake()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        followFactor = layerData.GetFollowFactor();

        CachePieces();

        lastCameraX = cameraTransform.position.x;
        verticalOffset = transform.position.y - cameraTransform.position.y;
    }

    private void CachePieces()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        pieces = new Transform[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
            pieces[i] = renderers[i].transform;

        pieceWidth = renderers[0].bounds.size.x;
    }

    private void LateUpdate()
    {
        float cameraX = cameraTransform.position.x;
        float delta = cameraX - lastCameraX;
        lastCameraX = cameraX;

        Move(delta);
        Wrap(cameraX);
    }

    private void Move(float delta)
    {
        float y = cameraTransform.position.y + verticalOffset;

        foreach (Transform piece in pieces)
        {
            Vector3 position = piece.position;

            position.x += delta * followFactor;
            position.y = y;

            piece.position = position;
        }
    }

    // La pieza que quedo fuera de cuadro salta al otro lado
    private void Wrap(float cameraX)
    {
        float cycle = pieceWidth * pieces.Length;

        foreach (Transform piece in pieces)
        {
            float distance = piece.position.x - cameraX;

            if (Mathf.Abs(distance) < pieceWidth) continue;

            Vector3 position = piece.position;
            position.x -= Mathf.Sign(distance) * cycle;
            piece.position = position;
        }
    }
}