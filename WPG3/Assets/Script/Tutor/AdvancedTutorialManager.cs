using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdvanceTutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [TextArea(2, 5)] public string tutorialText; // teks yang ditampilkan
        public GameObject uiToShow;                  // UI yang ingin ditampilkan
        public GameObject uiToHide;                  // UI yang ingin disembunyikan
        public bool hideTutorialPanel;               // apakah panel disembunyikan di step ini
    }

    public GameObject tutorialPanel;        // Panel untuk teks tutorial
    public TextMeshProUGUI tutorialText;    // Komponen teks
    public Button nextButton;               // Tombol Next (selalu aktif)
    public TutorialStep[] steps;            // Semua langkah tutorial

    private int currentStep = 0;

    void Start()
    {
        Time.timeScale = 1f;

        // Sembunyikan semua UI di awal
        foreach (var step in steps)
        {
            if (step.uiToShow != null)
                step.uiToShow.SetActive(false);
        }

        // Tampilkan step pertama
        ShowStep(0);

        // Pastikan tombol next selalu aktif
        nextButton.gameObject.SetActive(true);

        // Tambahkan event untuk tombol next
        nextButton.onClick.AddListener(NextStep);
    }

    void Update()
    {
        // Tombol spasi juga bisa lanjut
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }
    }

    void ShowStep(int index)
    {
        if (index >= steps.Length)
        {
            EndTutorial();
            return;
        }

        var step = steps[index];

        // Atur panel tutorial
        tutorialPanel.SetActive(!step.hideTutorialPanel);

        // Update teks hanya kalau panel aktif
        if (!step.hideTutorialPanel && !string.IsNullOrEmpty(step.tutorialText))
        {
            tutorialText.text = step.tutorialText;
        }

        // Atur UI yang muncul / hilang
        if (step.uiToShow != null)
            step.uiToShow.SetActive(true);

        if (step.uiToHide != null)
            step.uiToHide.SetActive(false);
    }

    void NextStep()
    {
        currentStep++;

        if (currentStep < steps.Length)
        {
            ShowStep(currentStep);
        }
        else
        {
            EndTutorial();
        }
    }

    void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        nextButton.gameObject.SetActive(false); // Tombol baru hilang kalau tutorial selesai
        Debug.Log("Tutorial selesai!");
    }
}
