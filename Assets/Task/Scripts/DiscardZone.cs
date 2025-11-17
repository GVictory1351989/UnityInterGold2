using UnityEngine;

public class DiscardZone : MonoBehaviour
{
    public static DiscardZone Instance;

    private RectTransform rect;

    void Awake()
    {
        Instance = this;
        rect = GetComponent<RectTransform>();
    }

    public bool IsInside(RectTransform cardRect)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            rect,
            Input.mousePosition,
            null
        );
    }

    public void DiscardCard(GameObject card)
    {
        Debug.Log("Card discarded: " + card.name);

        // Optionally destroy
        Destroy(card);

        // Or move to discard pile object
        // card.transform.SetParent(discardParent);
    }
}
