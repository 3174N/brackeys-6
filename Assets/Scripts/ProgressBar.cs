using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/LinearProgressBar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/LinearProgressBar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    public float Minimum;
    public float Maximum;
    public float Current;
    public Image Mask;
    public Image Fill;
    public Color FillColor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = Current - Minimum;
        float maximumOffset = Maximum - Minimum;
        float fillAmount = currentOffset / maximumOffset;
        Mask.fillAmount = fillAmount;

        Fill.color = FillColor;
    }
}