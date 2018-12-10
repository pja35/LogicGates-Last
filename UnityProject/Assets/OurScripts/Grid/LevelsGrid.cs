using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// Génère un ecran de chargement de niveau.
/// </summary>
public class LevelsGrid : MonoBehaviour
{
    /// <summary>
    /// ed. Nombre de colones du tableau de niveaux.
    /// </summary>
    public int nbCols = 2;
    /// <summary>
    /// ed. Nombre de lignes du tableau de niveaux.
    /// </summary>
    public int nbRows = 4;

    /// <summary>
    /// ed. Modifie l'espace disponible pour placer les boutons et autres elements
    /// </summary>
    public int paddingLeft = 150;
    /// <summary>
    /// ed. Modifie l'espace disponible pour placer les boutons et autres elements
    /// </summary>
    public int paddingRight = 150;
    /// <summary>
    /// ed. Modifie l'espace disponible pour placer les boutons et autres elements
    /// </summary>
    public int paddingBottom = 200;
    /// <summary>
    /// ed. Modifie l'espace disponible pour placer les boutons et autres elements
    /// </summary>
    public int paddingTop = 100;


    /// <summary>
    /// Change la couleur d'un bouton et son utilité selon si le niveau correspondant est débloqué.
    /// </summary>
    /// <param name="button">Le bouton.</param>
    /// <param name="numButon">Le numéro du niveau qui lui est associé.</param>
    /// <param name="nbUnlocked">Le nombre de niveaux débloqués.</param>
    private void SetUnlockedOrLockedState(GameObject button, int numButon, int nbUnlocked)
    {
        if (numButon > nbUnlocked)
        {
            button.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            ButtonUtil util = button.AddComponent<ButtonUtil>();
            button.GetComponent<Button>().onClick.AddListener(delegate { util.LoadScene("Niveau" + numButon); });
        }
    }

    /// <summary>
    /// Place un certain nombre de boutons dans un pannel.
    /// </summary>
    /// <param name="nbButton">Le nombre de boutons à placer</param>
    /// <param name="panel">Le panneau dans lequel placer les boutons</param>
    /// <param name="first">Le numéro du premier bouton</param>
    private void PlaceButtons(int nbButton, Transform panel, int first)
    {
        int nbUnlocked = ParametersLoader.GetUnlockedLevels();

        RectTransform parent = gameObject.GetComponentInParent<RectTransform>();
        //Taille du panneau supportant les boutons.
        Vector3 delta_Size = parent.sizeDelta / 2;
        Vector3 UpLeft = new Vector3(-delta_Size.x + paddingLeft, delta_Size.y - paddingTop, 0);

        //La distance entre chaque bouton en y:row et x:col
        float rowSteps = (parent.sizeDelta.y - paddingBottom - paddingTop) / (nbRows - 1);
        float colSteps = (parent.sizeDelta.x - paddingLeft - paddingRight) / (nbCols - 1);

        int placed = 0;

        for (int rowAct = 0; rowAct < nbRows; rowAct++)
        {
            for (int colAct = 0; colAct < nbCols; colAct++)
            {
                //Si on doit mettre moins que le nombre maximum de boutons dans le panel
                if (placed < nbButton)
                {
                    //On charge un préfab de bouton
                    GameObject button = (GameObject)Instantiate(Resources.Load("Button"));

                    //Placement du bouton
                    button.transform.SetParent(panel);
                    button.transform.localPosition = new Vector3(UpLeft.x + colSteps * colAct, UpLeft.y - rowSteps * rowAct, 0);
                    button.transform.localScale = new Vector3(1, 1, 1);

                    button.GetComponentInChildren<Text>().text = "Niveau\n" + (first + placed);

                    SetUnlockedOrLockedState(button, first + placed, nbUnlocked);

                    placed++;
                }
            }
        }

    }

    /// <summary>
    /// Place un bouton contextuel(retourn menu, panneau suivant, panneau précedent).
    /// </summary>
    /// <param name="panel">Le panneau dans lequel palcer le bouton</param>
    /// <param name="left">Placer à gauche ou à droite</param>
    /// <param name="buttonName">La ressource de bouton à charger</param>
    /// <param name="nextPanel">Si on charge un changeur de panneau, le nom du panneau qui sera chargé.</param>
    private void placeBottomButton(Transform panel, bool left, string buttonName, GameObject nextPanel)
    {
        //Pour placer le boutton en bas à gauche(droite) du panel qui le contient.
        RectTransform parent = gameObject.GetComponentInParent<RectTransform>();
        //La taille du panel
        Vector3 delta_Size = parent.sizeDelta / 2;

        GameObject button = (GameObject)Instantiate(Resources.Load(buttonName));
        button.transform.SetParent(panel);

        //Pour placer le bouton non pas par son milleu mais par un de ses coin
        Vector3 buttonDelta = button.GetComponent<RectTransform>().sizeDelta / 2;

        Vector3 buttonPos;
        if (left)
        {
            buttonPos = new Vector3(-delta_Size.x + buttonDelta.x, -delta_Size.y + buttonDelta.y, 0);
        }else
        {
            buttonPos = new Vector3(delta_Size.x - buttonDelta.x, -delta_Size.y + buttonDelta.y, 0);
        }

        if (nextPanel != null)
        {
            ButtonUtil util = button.AddComponent<ButtonUtil>();
            button.GetComponent<Button>().onClick.AddListener(delegate { util.ChangePanel(nextPanel); });
        }

        button.transform.localScale = new Vector3(1, 1, 1);
        button.transform.localPosition = buttonPos;
    }

    /// <summary>
    /// Au démarageon crée les panneaux et on y place les boutons.
    /// </summary>
    public void Start()
    {
        //On calcule le nombre de panneau à creer en fonction du nombre de niveau éxistants dans le jeu
        int nbPanels = Mathf.CeilToInt((float)ParametersLoader.GetNbLevels() / (nbCols * nbRows));
        int nbLevelsPanel = nbCols * nbRows;
        //Nombre de panneaux restants à placer
        int toplace = ParametersLoader.GetNbLevels();
        int totalPlaced = 0;

        GameObject[] panels = new GameObject[nbPanels];

        for (int panelAct = 0; panelAct < nbPanels; panelAct++)
        {
            panels[panelAct] = (GameObject)Instantiate(Resources.Load("Panel"));
        }

        for (int panelAct = 0; panelAct < nbPanels; panelAct++)
        {
            panels[panelAct].transform.SetParent(gameObject.transform);

            //Permet de faire que le panel fasse la même taille que son père.
            RectTransform panelTransform = panels[panelAct].GetComponent<RectTransform>();
            panelTransform.sizeDelta = new Vector3(0, 0, 0);
            panelTransform.localScale = new Vector3(1, 1, 1);

            //On place soit le nombre de butons normal soit le nombre de panneaux restants à placer
            PlaceButtons(Math.Min(toplace - totalPlaced, nbLevelsPanel), panels[panelAct].transform, totalPlaced);
            totalPlaced += nbLevelsPanel;

            //Rajouter les boutons du bas selon le type de panneau (premier, normal, dernier)
            //Premier panneau: button home + suivant si existe
            if (panelAct == 0)
            {
                placeBottomButton(panels[panelAct].transform, true, "ButtonMenu", null);
                if (nbPanels > 1)
                {
                    placeBottomButton(panels[panelAct].transform, false, "NextPanel", panels[panelAct + 1]);
                }
            }
            //Dernier panneau si plus de un panneau: bouton panneau précédent
            else if (panelAct == nbPanels - 1 && nbPanels != 1)
            {
                panels[panelAct].SetActive(false);
                placeBottomButton(panels[panelAct].transform, true, "PrevPanel", panels[panelAct - 1]);
            }
            //Panneau avec précédent et suivant.
            else
            {
                panels[panelAct].SetActive(false);
                placeBottomButton(panels[panelAct].transform, true, "PrevPanel", panels[panelAct - 1]);
                placeBottomButton(panels[panelAct].transform, false, "NextPanel", panels[panelAct + 1]);
            }
        }


    }
}
