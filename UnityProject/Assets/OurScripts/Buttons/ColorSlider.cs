using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère les sliders permetant de changer la couleur des portes
/// </summary>
public class ColorSlider : MonoBehaviour
{
    /// <summary>
    /// Initialise l'état des slider selon les paramètres chargés
    /// </summary>
    void Start()
    {
        Color savedColor = ParametersLoader.GetColor();

        GameObject backGround = GameObject.Find("ColorDemo");
        RawImage backImage = backGround.GetComponent<RawImage>();
        backImage.color = savedColor;

        GameObject.Find("SliderR").gameObject.GetComponent<Slider>().value = savedColor.r;
        GameObject.Find("SliderG").gameObject.GetComponent<Slider>().value = savedColor.g;
        GameObject.Find("SliderB").gameObject.GetComponent<Slider>().value = savedColor.b;

        OnChange();

    }

    /// <summary>
    /// Modfie la zone de démonstation de la couleur et la couleur dans les paramètres
    /// </summary>
    public void OnChange()
    {
        GameObject backGround = GameObject.Find("ColorDemo");
        RawImage backImage = backGround.GetComponent<RawImage>();


        Slider sliderR = GameObject.Find("SliderR").gameObject.GetComponent<Slider>();
        Slider sliderG = GameObject.Find("SliderG").gameObject.GetComponent<Slider>();
        Slider sliderB = GameObject.Find("SliderB").gameObject.GetComponent<Slider>();

        backImage.color = new Color(sliderR.value, sliderG.value, sliderB.value);

        ParametersLoader.SetColor(backImage.color);

    }
}
