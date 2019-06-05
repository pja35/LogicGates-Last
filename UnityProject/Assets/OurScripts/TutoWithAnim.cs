using UnityEngine;
/// <summary>
/// Classe permettant d'afficher ou masquer l'animation de tuto dans la scene. 
/// Elle lance l'animation des que la fenetre est fermée ou immediatement si les tuto sont desactivés.
/// Elle la masque si le joueur commence à placer une porte.
/// </summary>
public class TutoWithAnim : MonoBehaviour
{

    /// <summary>
    /// Le pop up de tutoriel.
    /// </summary>
    public GameObject popup;

    /// <summary>
    /// Animation de tutoriel.
    /// </summary>
    public GameObject tutoanim;

    /// <summary>Le chemin menant a l'emplacement du fichier audio (par exemple Sounds/test)</summary> 
    public string soundName;

    /// <summary>
    /// Active le secret du tutoriel avec une chance sur dix
    /// </summary>
    private bool secret = false;

    /// <summary>
    /// On affiche la popup seulement si la variable si la variable tuto est activée dans les
    /// parametres utilisateur. On change l'apparence de l'animation de tuto une fois sur dix
    ///  </summary>
    void Start()
    {
        int random = Random.Range(0, 100);

        if (random == 0)
        {
            tutoanim.GetComponent<Renderer>().material = Resources.Load("GodHandMat", typeof(Material)) as Material;
            secret = true;
        }

        if (ParametersLoader.GetTuto())
        { 
            tutoanim.SetActive(false);
        }
        else
        {
            tutoanim.SetActive(true);
        }
    }

    void Update()
    {
        GameObject toolbox = GameObject.Find("Toolbox");
        Transform gate = null;
       
        //on verifie si l'utilisateur a placé une porte
        if (toolbox != null)
        {
            gate = toolbox.transform.Find("Inst 0(Clone)");
        }

        //Pas d'animation si popup active ou porte placée
        if ((popup && popup.activeSelf) || gate != null)
        {
            tutoanim.SetActive(false);
        }

        //on attend la fermeture de la popup pour lancer l'animation
        else if (!tutoanim.activeSelf)
        {
            tutoanim.SetActive(true);
            if (secret)
            {
                PlaySecretTheme();
            }
        }

        //on a trouvé le secret et pas de tuto activé on joue le theme directement 
        if (!ParametersLoader.GetTuto() && secret)
        {
            PlaySecretTheme();
        }

    }

    private void PlaySecretTheme()
    {
        SoundUtil.PlaySound("Sounds/magic");
        SoundUtil.PlayOnlySound(soundName);
        secret = false;
    }


}
