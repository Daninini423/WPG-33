using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdvanceTutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [TextArea(2, 5)] public string tutorialText;   // teks tutorial
        public GameObject uiToShow;                    // UI yang ingin ditampilkan
        public GameObject uiToHide;                    // UI yang ingin disembunyikan
        public Sprite characterExpression;             // ekspresi karakter di step ini
        public bool hidePanel;                         // apakah panel disembunyikan?
        public AudioClip stepSound;                    //  efek suara untuk step ini
    }

    [Header("UI References")]
    public Canvas tutorialCanvas;
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    public Button nextButton;
    public Image characterImage; // referensi ke image karakter

    [Header("Audio Settings")]
    public AudioSource audioSource; //  tempat mainkan efek suara

    public TutorialStep[] steps;
    private int currentStep = 0;

    void Start()
    {
        Time.timeScale = 1f;

        foreach (var step in steps)
        {
            if (step.uiToShow != null)
                step.uiToShow.SetActive(false);
        }

        ShowStep(0);
        nextButton.onClick.AddListener(NextStep);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextStep();
    }

    void ShowStep(int index)
    {
        if (index >= steps.Length)
        {
            EndTutorial();
            return;
        }

        var step = steps[index];

        // tampilkan/hide panel
        tutorialPanel.SetActive(!step.hidePanel);

        // ubah teks
        if (!string.IsNullOrEmpty(step.tutorialText))
            tutorialText.text = step.tutorialText;

        // ubah ekspresi karakter
        if (characterImage != null && step.characterExpression != null)
            characterImage.sprite = step.characterExpression;

        // mainkan efek suara step ini 
        if (audioSource != null && step.stepSound != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f); // biar terasa hidup
            audioSource.PlayOneShot(step.stepSound);
        }

        // atur UI tambahan
        if (step.uiToShow != null)
            step.uiToShow.SetActive(true);
        if (step.uiToHide != null)
            step.uiToHide.SetActive(false);
    }

    void NextStep()
    {
        currentStep++;
        if (currentStep < steps.Length)
            ShowStep(currentStep);
        else
            EndTutorial();
    }

    void EndTutorial()
    {
        tutorialCanvas.gameObject.SetActive(false);
        Debug.Log("Tutorial selesai!");
    }
}
