using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SampleScene : MonoBehaviour
{

    public Button testToastButton;

    public Button testAsyncToastButton;
    // Start is called before the first frame update
    
    private readonly SampleKit sample = new SampleKit();
    void Start()
    {
        sample.Initialize();
        testToastButton.onClick.AddListener(sample.CallTest);
        testAsyncToastButton.onClick.AddListener(sample.AsyncCallTest);
    }
}