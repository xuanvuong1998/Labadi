using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project_Try1.Models {
    public class Question : IComparable<Question> {

        public int ID { get; set; }
        public int QuizID { get; set; }
        public int Type { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string AnsA { get; set; }
        public string AnsB { get; set; }
        public string AnsC { get; set; }
        public string AnsD { get; set; }

        public string Answer { get; set; }
        public int Time { get; set; }

        public int CompareTo(Question other) {
            return (ID - other.ID);
        }

    }


    public class QuestionDM {

        public List<Question> LoadQuestionList() {
            List<Question> list = null;
            try {
                using (var con = DBUtils.GetConnection()) {

                    con.Open();
                    using (var cmd = new SqlCommand("LoadAllQuestions", con)) {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                Question q = new Question {
                                    ID = (int)reader["id"],
                                    QuizID = (int)reader["quizID"],
                                    Content = (string)reader["queContent"],
                                    AnsA = (string)reader["answerA"],
                                    AnsB = (string)reader["answerB"],
                                    AnsC = (string)reader["answerC"],
                                    AnsD = (string)reader["answerD"],
                                    Answer = reader["answer"] == DBNull.Value ? "" : (string)reader["answer"],
                                    Image = reader["qimage"] == DBNull.Value ? "" : (string)reader["qimage"],
                                    Time = reader["qtime"] == DBNull.Value ? 0 : (int)reader["qtime"],
                                    Type = (int)reader["qtype"]
                                };

                                if (list == null) {
                                    list = new List<Question>();
                                }

                                list.Add(q);

                            }
                        }
                    }
                }
            } catch (Exception e) {
                throw e;
            }
            return list;
        }


        public List<Question> FindQuestionByQuizID(int id) {
            List<Question> list = null;
            try {
                using (var con = DBUtils.GetConnection()) {

                    con.Open();
                    using (var cmd = new SqlCommand("FindQuestionsOfAQuiz", con)) {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@quizID", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                Question q = new Question {
                                    ID = (int)reader["id"],
                                    Content = (string)reader["queContent"],
                                    AnsA = (string)reader["answerA"],
                                    AnsB = (string)reader["answerB"],
                                    AnsC = (string)reader["answerC"],
                                    AnsD = (string)reader["answerD"],
                                    Answer = reader["answer"] == DBNull.Value ? "" : (string)reader["answer"],
                                    Image = reader["qimage"] == DBNull.Value ? "" : (string)reader["qimage"],
                                    Time = reader["qtime"] == DBNull.Value ? 0 : (int)reader["qtime"],
                                    Type = (int)reader["qtype"]
                                };

                                if (list == null) {
                                    list = new List<Question>();
                                }

                                list.Add(q);

                            }
                        }
                    }
                }
            } catch (Exception e) {
                throw e;
            }
            return list;
        }

        public Question FindQuestionByID(int id) {
            try {
                using (var con = DBUtils.GetConnection()) {

                    con.Open();
                    using (var cmd = new SqlCommand("FindQuestionByID", con)) {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                Question q = new Question {
                                    ID = id,
                                    QuizID = (int)reader["quizID"],
                                    Content = (string)reader["queContent"],
                                    AnsA = (string)reader["answerA"],
                                    AnsB = (string)reader["answerB"],
                                    AnsC = (string)reader["answerC"],
                                    AnsD = (string)reader["answerD"],
                                    Answer = reader["answer"] == DBNull.Value ? "" : (string)reader["answer"],
                                    Image = reader["qimage"] == DBNull.Value ? "" : (string)reader["qimage"],
                                    Time = reader["qtime"] == DBNull.Value ? 0 : (int)reader["qtime"],
                                    Type = (int)reader["qtype"]
                                };

                                return q;

                            }
                        }
                    }
                }
            } catch (Exception e) {
                throw e;
            }
            return null;
        }

        public int GetMaxID() {

            try {
                using (var con = DBUtils.GetConnection()) {
                    con.Open();
                    using (var cmd = new SqlCommand("GetMaxQuestionID", con)) {
                        var reader = cmd.ExecuteReader();
                        if (reader.Read()) {
                            return (int)reader[0];
                        }
                        return -1;
                    }
                }
            } catch (Exception e) {

                throw e;
            }

        }

        public void AddQuestion(Question q) {
            try {
                using (var con = DBUtils.GetConnection()) {
                    con.Open();
                    using (var cmd = new SqlCommand("AddNewQuestion", con)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", q.ID);
                        cmd.Parameters.AddWithValue("@quizID", q.QuizID);
                        cmd.Parameters.AddWithValue("@answer", q.Answer);
                        cmd.Parameters.AddWithValue("@answerA", q.AnsA);
                        cmd.Parameters.AddWithValue("@answerB", q.AnsB);
                        cmd.Parameters.AddWithValue("@answerC", q.AnsC);
                        cmd.Parameters.AddWithValue("@answerD", q.AnsD);
                        cmd.Parameters.AddWithValue("@qtype", q.Type);
                        cmd.Parameters.AddWithValue("@queContent", q.Content);
                        cmd.Parameters.AddWithValue("@qTime", q.Time);
                        cmd.Parameters.AddWithValue("@qImage", q.Image);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception e) {

                throw e;
            }
        }

        public void UpdateQuestion(Question q) {
            try {
                using (var con = DBUtils.GetConnection()) {
                    con.Open();
                    using (var cmd = new SqlCommand("UpdateQuestion", con)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", q.ID);
                        cmd.Parameters.AddWithValue("@answer", q.Answer);
                        cmd.Parameters.AddWithValue("@answerA", q.AnsA);
                        cmd.Parameters.AddWithValue("@answerB", q.AnsB);
                        cmd.Parameters.AddWithValue("@answerC", q.AnsC);
                        cmd.Parameters.AddWithValue("@answerD", q.AnsD);
                        cmd.Parameters.AddWithValue("@queContent", q.Content);
                        cmd.Parameters.AddWithValue("@qTime", q.Time);
                        cmd.Parameters.AddWithValue("@qImage", q.Image);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception e) {
                throw e;
            }
        }

        public void DeleteQuestion(int id) {
            try {
                using (var con = DBUtils.GetConnection()) {
                    con.Open();
                    using (var cmd = new SqlCommand("DeleteQuestion", con)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception e) {

                throw e;
            }
        }

        public void DuplicateQuestion(Question q) {
            try {                
                q.ID = GetMaxID() + 1;
                AddQuestion(q);
            } catch (Exception e) {
                throw e;
            }
        }
    }
}