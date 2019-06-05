using UnityEngine;

/// <summary>
/// Contrôle la musique du jeu
/// </summary>
public class Theme : MonoBehaviour
{
    /// <summary>Le chemin menant a l'emplacement du fichier audio (par exemple Sounds/test)</summary> 
    public string soundName;
    /// <summary>
    /// Change le theme audio actuel en celui choisit pour cette scene 
    /// </summary>
    void Start()
    {
        SoundUtil.PlayOnlySound(soundName);
    }

}
