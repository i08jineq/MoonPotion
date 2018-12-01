using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DarkLordGame
{
    public class UnlockIngredientScreen : MonoBehaviour
    {
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemDescription;
        public TextMeshProUGUI price;
        public Button closeButton;

        public Communicator onClosed = new Communicator();
        public void Setup()
        {
            closeButton.onClick.AddListener(OnClosed);
        }

        public void Open(IngredientData ingredient)
        {
            gameObject.SetActive(true);
            itemName.SetText(ingredient.ingredientName);
            itemDescription.SetText(ingredient.description);
            price.SetText(ingredient.price.ToString());


            itemName.ForceMeshUpdate();
            itemDescription.ForceMeshUpdate();
            price.ForceMeshUpdate();
        }

        private void OnClosed()
        {
            gameObject.SetActive(false);
            onClosed.Invoke();
        }
    }
}