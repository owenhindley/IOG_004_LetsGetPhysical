using UnityEngine;

public class AlphabetizeChildren : MonoBehaviour
{
    [InspectorButton("OrderAlphabetically")]
    public bool _orderAlphabetically;

    public void OrderAlphabetically()
    {
        for (int j = 0; j < transform.childCount; j++)
        {
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                if (string.Compare(transform.GetChild(i).name, transform.GetChild(i + 1).name) > 0)
                {
                    transform.GetChild(i + 1).SetSiblingIndex(i);
                }
            }

        }
    }
}
