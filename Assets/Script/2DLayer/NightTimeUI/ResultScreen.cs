using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DarkLordGame
{
    public class ResultScreen : MonoBehaviour
    {
        public TextMeshProUGUI potionName;
        public Button closeButton;
        public ResultUIGroup effective;
        public ResultUIGroup taste;
        public ResultUIGroup ability;
        public CommentData commentData;
        public GameObject totalScoreRoot;
        public TextMeshProUGUI totalScore;

        public float delayForEachElement = 0.2f;
        public float delayForEachParameter = 0.5f;

        [System.Serializable]
        public class ResultUIGroup
        {
            public GameObject root;
            public TextMeshProUGUI scoreText;
            public TextMeshProUGUI commenterName;
            public TextMeshProUGUI commentText;

            public void SetData(string score, string commenter, string comment)
            {
                scoreText.SetText(score);
                commenterName.SetText(commenter);
                commentText.SetText(comment);

                scoreText.ForceMeshUpdate();
                commenterName.ForceMeshUpdate();
                commentText.ForceMeshUpdate();
            }

            public IEnumerator ShowEnumerator(float delayForEach)
            {
                root.SetActive(true);
                yield return new WaitForSeconds(delayForEach);
                scoreText.gameObject.SetActive(true);
                yield return new WaitForSeconds(delayForEach);
                commenterName.gameObject.SetActive(true);
                yield return new WaitForSeconds(delayForEach);
                commentText.gameObject.SetActive(true);
            }
            public void Hide()
            {
                root.SetActive(false);
                scoreText.gameObject.SetActive(false);
                commenterName.gameObject.SetActive(false);
                commentText.gameObject.SetActive(false);
            }
        }

        public Communicator onClosed = new Communicator();

        public void Setup()
        {
            closeButton.onClick.AddListener(OnClickedClose);
        }

        public IEnumerator ShowResultEnumerator(InventoryItemData inventoryItemData)
        {
            closeButton.gameObject.SetActive(false);
            totalScoreRoot.SetActive(false);
            totalScore.gameObject.SetActive(false);
            potionName.SetText(inventoryItemData.itemName);
            potionName.ForceMeshUpdate();
            totalScore.SetText(inventoryItemData.totalScore.ToString("n0"));
            totalScore.gameObject.SetActive(false);
            effective.Hide();
            taste.Hide();
            ability.Hide();


            List<string> commenters = commentData.GetCommenters(3);
            string effectiveComment = commentData.GetEffectComment((int)inventoryItemData.effectiveScore);
            effective.SetData(inventoryItemData.effectiveScore.ToString("n0"), commenters[0], effectiveComment);

            string tasteComment = commentData.GetEffectComment((int)inventoryItemData.tasteScore);
            taste.SetData(inventoryItemData.tasteScore.ToString("n0"), commenters[1], tasteComment);

            string universualComment = commentData.GetEffectComment((int)inventoryItemData.ability);
            ability.SetData(inventoryItemData.ability.ToString("n0"), commenters[2], universualComment);
            yield return null;

            yield return effective.ShowEnumerator(delayForEachElement);
            yield return new WaitForSeconds(delayForEachParameter);

            yield return taste.ShowEnumerator(delayForEachElement);
            yield return new WaitForSeconds(delayForEachParameter);

            yield return ability.ShowEnumerator(delayForEachElement);
            yield return new WaitForSeconds(delayForEachParameter);

            totalScoreRoot.SetActive(true);
            new WaitForSeconds(delayForEachElement);
            totalScore.gameObject.SetActive(true);

            yield return new WaitForSeconds(delayForEachParameter);
            closeButton.gameObject.SetActive(true);
        }

        private void OnClickedClose()
        {
            gameObject.SetActive(false);
            onClosed.Invoke();
        }
    }
}