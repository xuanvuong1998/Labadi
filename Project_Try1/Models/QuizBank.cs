using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project_Try1.Models {
    public class Quiz {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Creator { get; set; }

        public List<Question> QuestionList { get; set; }

        public void AddQuestionToQuiz(Question aQue) {
            QuestionList.Add(aQue);
        }

        public void RemoveQuestionFromQuiz(Question aQue) {
            QuestionList.Remove(aQue);
        }

        public void UpdateQuestionInQuiz(Question aQue) {
            Question q = QuestionList.SingleOrDefault(item => item.ID == aQue.ID);
            q = aQue;
        }
    }

    public class QuizBank {

        public List<Quiz> LoadTopQuizs(int top) {
            List<Quiz> list = null;
            try {
                using (var con = DBUtils.GetConnection()) {
                    string sql = "SELECT * FROM Quiz";

                    con.Open();
                    using (var cmd = new SqlCommand(sql, con)) {
                        //cmd.Parameters.AddWithValue("@top", top);

                        using (var reader = cmd.ExecuteReader()) {

                            if (reader.HasRows) {

                                QuestionDM questionDM = new QuestionDM();

                                while (reader.Read()) {

                                    int quizID = (int)reader["id"];
                                    Quiz q = new Quiz {
                                        Creator = (string)reader["creator"],
                                        ID = quizID,
                                        Title = (string)reader["title"],
                                        Image = reader["image"] == DBNull.Value ? "" : (string)reader["image"],
                                        QuestionList = questionDM.FindQuestionByQuizID(quizID)
                                    };

                                    if (list == null) {
                                        list = new List<Quiz>();
                                    }

                                    list.Add(q);
                                }
                            }
                        }
                    }

                }
            } catch (Exception e) {

                throw e;
            }
            return list;
        }

        public List<Quiz> GetAllQuizesOfCreator(string creatorID) {
            List<Quiz> list = null;

            try {
                using (var con = DBUtils.GetConnection()) {
                    con.Open();
                    using (var cmd = new SqlCommand("GetAllQuizesOfCreator", con)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@creator", creatorID);
                        using (var reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {

                                QuestionDM questionDM = new QuestionDM();

                                while (reader.Read()) {

                                    int quizID = (int)reader["id"];
                                    Quiz q = new Quiz {
                                        Creator = (string)reader["creator"],
                                        ID = quizID,
                                        Title = (string)reader["title"],
                                        Image = reader["image"] == DBNull.Value ? "" : (string)reader["image"],
                                        QuestionList = questionDM.FindQuestionByQuizID(quizID)
                                    };

                                    if (list == null) {
                                        list = new List<Quiz>();
                                    }

                                    list.Add(q);
                                }
                            }
                        }
                    }
                }

            } catch (Exception e) {

                throw e;
            }
            return list;
        }


        public int GetMaxID() {

            try {
                using (var con = DBUtils.GetConnection()) {
                    con.Open();                    
                    using (var cmd = new SqlCommand("GetMaxQuizID", con)) {
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

        public Quiz FindQuizByID(int id) {
            try {                
                using (var con = DBUtils.GetConnection()) {
                    con.Open();
                    using (var cmd = new SqlCommand("FindQuizByID", con)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows) {

                            QuestionDM questionDM = new QuestionDM();

                            if (reader.Read()) {

                                int quizID = (int)reader["id"];
                                Quiz q = new Quiz {
                                    Creator = (string)reader["creator"],
                                    ID = quizID,
                                    Title = (string)reader["title"],
                                    Image = reader["image"] == DBNull.Value ? "" : (string)reader["image"],
                                    QuestionList = questionDM.FindQuestionByQuizID(quizID)
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

        public void AddNewQuiz(Quiz q) {
            try {
                using (var con = DBUtils.GetConnection()) {
                    con.Open();
                    using (var cmd = new SqlCommand("AddNewQuiz", con)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", q.ID);
                        cmd.Parameters.AddWithValue("@creator", q.Creator);                        
                        cmd.Parameters.AddWithValue("@title", q.Title);
                        cmd.Parameters.AddWithValue("@image", q.Image);
                        cmd.ExecuteNonQuery();
                    }
                }

                QuestionDM dM = new QuestionDM();
                foreach(var item in q.QuestionList) {
                    dM.AddQuestion(item);
                }
            } catch (Exception e) {

                throw e;
            }
        }

        public void UpdateDescription(Quiz q) {
            try {
                using (var con = DBUtils.GetConnection()) {
                    con.Open();
                    using (var cmd = new SqlCommand("UpdateDescription", con)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", q.ID);
                        cmd.Parameters.AddWithValue("@title", q.Title);
                        cmd.Parameters.AddWithValue("@image", q.Image);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception e) {

                throw e;
            }
        }
    }
}