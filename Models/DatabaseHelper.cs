using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TaskManagementApp.Models;

namespace TaskManagementApp.Data
{
    public class DatabaseHelper
    {
        
        private string connectionString = ConfigurationManager.ConnectionStrings["TaskManagementDb"].ConnectionString;

        public List<TaskItem> GetAllTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TaskItems", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new TaskItem
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        IsCompleted = (bool)reader["IsCompleted"]
                    });
                }
            }
            return tasks;
        }

        public TaskItem GetTaskById(int id)
        {
            TaskItem task = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TaskItems WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    task = new TaskItem
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        IsCompleted = (bool)reader["IsCompleted"]
                    };
                }
            }
            return task;
        }

        public void AddTask(TaskItem task)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO TaskItems (Title, Description, IsCompleted) VALUES (@Title, @Description, @IsCompleted)", connection);
                command.Parameters.AddWithValue("@Title", task.Title);
                command.Parameters.AddWithValue("@Description", task.Description);
                command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateTask(TaskItem task)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE TaskItems SET Title = @Title, Description = @Description, IsCompleted = @IsCompleted WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", task.Id);
                command.Parameters.AddWithValue("@Title", task.Title);
                command.Parameters.AddWithValue("@Description", task.Description);
                command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTask(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM TaskItems WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}