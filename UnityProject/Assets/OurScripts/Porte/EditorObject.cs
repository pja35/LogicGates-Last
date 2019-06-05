using System.Collections;
using UnityEngine;

/// <summary>
/// Un objet placé par lé développeur dans Unity qui sera transformé en porte au moment du jeu
/// </summary>
public class EditorObject : MonoBehaviour, IDevObjInit
{

    /// <summary>
    /// List des objets auxquels se connecter
    /// </summary>
	public GameObject[] listToConnecte;
    /// <summary>
    /// ed. Pour chaque objet auquel se connecter le numéro d'input auquel se connecter
    /// </summary>
    public int[] connectionsIO;
    /// <summary>
    /// ed. Le comportement de la future porte
    /// </summary>
    public Comportement comp;
    /// <summary>
    /// ed. Le nombre d'IO de la future porte
    /// </summary>
	public int nb_outputs, nb_inputs;
    /// <summary>
    /// ed. Le symbole de la porte
    /// </summary>
	public string symbol = "";

    /// <summary>
    /// Configure la porte 
    /// </summary>
    public void Instantiate()
    {
        Gate gt = gameObject.AddComponent<Gate>();
        gt.CreateGateIO(nb_inputs, nb_outputs, comp);
        gt.OnFix();

        DrawUtil.AddText(gameObject, symbol);
    }

    /// <summary>
    /// Place l'objet sur la grille
    /// </summary>
    public void PlaceOnGrid()
    {
        GridUtil.TakeNearestAnchor(gameObject);
    }

    /// <summary>
    /// Tente de connecter l'objet
    /// </summary>
    public void MakeInitialConnections()
    {
        Gate gt = gameObject.GetComponent<Gate>();
        for (int i = 0; i < listToConnecte.Length; i++)
        {
            GameObject toCo = listToConnecte[i];
            int inputToCo = 0;
            if (i < connectionsIO.Length) { inputToCo = connectionsIO[i]; }
            gt.TryToConnect(i, toCo, inputToCo);
        }
    }

    /// <summary>
    /// Inutilisé pour ce type d'objets
    /// </summary>
    public void TagGameObject()
    {

    }

    
    /// <summary>
    /// Lorsqu'on clique sur l'objet on joue le son d'une decharge et on fait vibrer le 
    /// telephone si les vibrations sont activées. 
    /// On augmente egalement la portée des particules pendant une seconde.
    /// </summary>
    void OnMouseDown()
    {
        ParticleSystem.MainModule m = GetComponentInChildren<ParticleSystem>().main;
        m.startLifetime = 1;
        SoundUtil.PlaySound("Sounds/light");
        if (ParametersLoader.GetSnooze())
            Handheld.Vibrate();
        StartCoroutine(waiter());
    }

    /// <summary>
    /// Fonction qui attend 1 seconde avant de remettre l'animation à son etat initial 
    /// </summary>
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(1);
        ParticleSystem.MainModule m = GetComponentInChildren<ParticleSystem>().main;
        m.startLifetime = 0.5F;

    }
}
