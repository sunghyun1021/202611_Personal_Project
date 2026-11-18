using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Toggle _fullScreenToggle;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private TMP_InputField _soundText;
    
    // ---------- Event Method ----------

    private void OnEnable() => BindSoundUI();

    private void OnDisable() => UniBindSoundUI();

    // ----------------------------------

    private void BindSoundUI()
    {
        _soundText.onEndEdit.AddListener(SoundTextChanged);
        _soundSlider.onValueChanged.AddListener(SoundSliderChanged);
    }

    private void UniBindSoundUI()
    {
        _soundText.onEndEdit.RemoveListener(SoundTextChanged);
    }

    private void SoundTextChanged(string text)
    {
        if (text == "") return;
        
        float value = float.Parse(text);
        Debug.Log($"바뀜 : {value}");
        
        _soundSlider.value = (value/ 100);
    }

    private void SoundSliderChanged(float value)
    {
        int sound = (int)(value * 100);
        _soundText.text = sound.ToString();
    }
}
