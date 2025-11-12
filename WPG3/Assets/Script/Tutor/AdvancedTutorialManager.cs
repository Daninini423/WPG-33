using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // <-- Tambahkan ini untuk pindah scene

public class AdvanceTutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [TextArea(2, 5)] public string tutorialText; // teks tutorial
        public GameObject uiToShow;                  // UI yang ingin ditampilkan
        public GameObject uiToHide;                  // UI yang ingin disembunyikan
        public Sprite characterExpression;           // ekspresi karakter di step ini
        public bool hidePanel;                       // apakah panel disembunyikan?
        public AudioClip stepSound;                  //  suara one-shot di step ini
    }

    [Header("UI References")]
    public Canvas tutorialCanvas;
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    public Button nextButton;
    public Image characterImage;
    public AudioSource audioSource;                 //  audio source untuk play one-shot

    [Header("Scene Settings")]
    public string nextSceneName;                    // nama scene tujuan di akhir tutorial

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

        // tampilkan / sembunyikan panel
        tutorialPanel.SetActive(!step.hidePanel);

        // ubah teks
        if (!string.IsNullOrEmpty(step.tutorialText))
            tutorialText.text = step.tutorialText;

        // ubah ekspresi karakter
        if (characterImage != null && step.characterExpression != null)
            characterImage.sprite = step.characterExpression;

        // atur UI tambahan
        if (step.uiToShow != null)
            step.uiToShow.SetActive(true);
        if (step.uiToHide != null)
            step.uiToHide.SetActive(false);

        //  mainkan suara one-shot (jika ada)
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
            //  Pindah ke scene berikut
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // kalau tidak diisi, cukup tutup canvas
            tutorialCanvas.gameObject.SetActive(false);
        }
    }
}
