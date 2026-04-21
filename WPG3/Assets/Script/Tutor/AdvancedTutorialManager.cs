using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AdvanceTutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [TextArea(2, 5)] public string tutorialText;

        public GameObject uiToShow;
        public GameObject uiToHide;

        public GameObject uiToShow1;
        public GameObject uiToHide1;

        public GameObject canvasToShow;
        public GameObject canvasToHide;

        public Sprite characterExpression1;
        public Sprite characterExpression2;

        public bool hidePanel;

        public AudioClip stepSound;

        //  TAMBAHAN (INI YANG PENTING)
        public GameObject[] uiToFront; // UI yang mau dipaling depan
    }

    [Header("UI References")]
    public Canvas tutorialCanvas;
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    public Button nextButton;
    public Image characterImage1;
    public Image characterImage2;
    public AudioSource audioSource;

    [Header("Scene Settings")]
    public string nextSceneName;

    public TutorialStep[] steps;
    private int currentStep = 0;

    void Start()
    {
        Time.timeScale = 1f;

        // matikan semua UI awal
        foreach (var step in steps)
        {
            if (step.uiToShow != null)
                step.uiToShow.SetActive(false);

            if (step.uiToShow1 != null)
                step.uiToShow1.SetActive(false);
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

        // panel
        tutorialPanel.SetActive(!step.hidePanel);

        // text
        if (!string.IsNullOrEmpty(step.tutorialText))
            tutorialText.text = step.tutorialText;

        // character image
        if (characterImage1 != null && step.characterExpression1 != null)
            characterImage1.sprite = step.characterExpression1;

        if (characterImage2 != null && step.characterExpression2 != null)
            characterImage2.sprite = step.characterExpression2;

        // show/hide UI
        if (step.uiToShow != null)
            step.uiToShow.SetActive(true);

        if (step.uiToHide != null)
            step.uiToHide.SetActive(false);

        if (step.uiToShow1 != null)
            step.uiToShow1.SetActive(true);

        if (step.uiToHide1 != null)
            step.uiToHide1.SetActive(false);

        // canvas
        if (step.canvasToShow != null)
            step.canvasToShow.SetActive(true);

        if (step.canvasToHide != null)
            step.canvasToHide.SetActive(false);

        //  BAGIAN PALING PENTING (LAYER / ORDER)
        if (step.uiToFront != null)
        {
            foreach (var ui in step.uiToFront)
            {
                if (ui != null)
                    ui.transform.SetAsLastSibling(); // paling depan
            }
        }

        // sound
        if (audioSource != null && step.stepSound != null)
            audioSource.PlayOneShot(step.stepSound);
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
        Debug.Log("Tutorial selesai!");

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            tutorialCanvas.gameObject.SetActive(false);
        }
    }
}