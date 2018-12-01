using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "CommentData", menuName = "DarkLordGame/CommentData")]
    public class CommentData : ScriptableObject
    {
        [System.Serializable]
        public class CommentsForScore
        {
            public int score = 0;
            public List<string> comments = new List<string>();
        }

        public List<string> commenters = new List<string>();
        public List<CommentsForScore> effectComments = new List<CommentsForScore>();
        public List<CommentsForScore> tasteComments = new List<CommentsForScore>();
        public List<CommentsForScore> storabilityComments = new List<CommentsForScore>();

        public List<string> GetCommenters(int numbers)
        {
            List<string> targetList = new List<string>();
            targetList.AddRange(commenters);
            List<string> resultList = new List<string>();
            for (int i = 0; i < numbers; i++)
            {
                int index = Random.Range(0, targetList.Count);
                resultList.Add(targetList[index]);
                targetList.RemoveAt(index);
            }
            return resultList;
        }

        public string GetEffectComment(int score)
        {
            return GetComment(effectComments, score);
        }

        public string GetTasteComments(int score)
        {
            return GetComment(tasteComments, score);
        }

        public string GetStorabilityComments(int score)
        {
            return GetComment(storabilityComments, score);
        }

        private string GetComment(List<CommentsForScore> commentsForScores, int score)
        {
            string comment = "Meh...";
            int count = commentsForScores.Count;
            for (int i = 0; i < count; i++)
            {
                if (commentsForScores[i].score == score)
                {
                    int rand = Random.Range(0, commentsForScores[i].comments.Count);
                    return commentsForScores[i].comments[rand];
                }
            }
            return comment;
        }
    }
}