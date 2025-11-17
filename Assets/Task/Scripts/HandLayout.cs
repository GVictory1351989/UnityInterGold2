using UnityEngine;
using System.Collections;

public class HandLayout : MonoBehaviour
{
    public Transform handArea;
    public float maxArcAngle = 60f;  // maximum spread for many cards
    public float radius = 200f;
    [ContextMenu("Arrange Card")]
    public void ArrangeHand()
    {
        StartCoroutine(ArrangeHandCoroutine());
    }

    IEnumerator ArrangeHandCoroutine()
    {
        yield return null;
        int count = handArea.childCount;
        if (count == 0) yield break;
        float arc = Mathf.Min(maxArcAngle, count * 15f);
        float startAngle = -arc / 2f;
        for (int i = 0; i < count; i++)
        {
            Transform card = handArea.GetChild(i);
            float angle = (count == 1) ? 0f : startAngle + i * (arc / (count - 1));
            float rad = Mathf.Deg2Rad * angle;
            float x = Mathf.Sin(rad) * radius;
            float y = -Mathf.Cos(rad) * radius + radius; 
            card.localPosition = new Vector3(x, y, 0);
            card.localRotation = Quaternion.Euler(0, 0, -angle);
        }
    }
}
