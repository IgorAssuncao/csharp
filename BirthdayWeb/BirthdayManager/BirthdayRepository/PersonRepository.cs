using System;
using System.Linq;
using Model;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Repository
{
    public class PersonRepository : IPersonRepository
    {
        private string Connectionstring;

        public PersonRepository(IConfiguration configuration)
        {
            this.Connectionstring = configuration.GetConnectionString("BirthdayDB");
        }

        public List<Person> GetAllPeople()
        {
            using(var connection = new SqlConnection(Connectionstring))
            {
                List<Person> people = new List<Person>();

                connection.Open();
                try
                {
                    SqlCommand sqlCommand = connection.CreateCommand();

                    sqlCommand.CommandText = "SELECT * FROM PERSON ORDER BY DAY(birthday) - DAY(CAST(GETDATE() AS DATE)), MONTH(birthday) - MONTH(CAST(GETDATE() AS DATE))";

                    // ExecuteReader() returns data from DB but it's an Iterable.
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while(sqlDataReader.Read())
                    {
                        people.Add(new Person(
                            Convert.ToInt32(sqlDataReader["id"].ToString()),
                            sqlDataReader["name"].ToString(),
                            sqlDataReader["lastname"].ToString(),
                            DateTime.Parse(sqlDataReader["birthday"].ToString())
                        ));
                    }

                    return people;
                }
                catch
                {
                    return new List<Person>();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Person GetPersonById(int Id)
        {
            using(var connection = new SqlConnection(Connectionstring))
            {
                List<Person> people = new List<Person>();

                connection.Open();
                try
                {
                    SqlCommand sqlCommand = connection.CreateCommand();

                    sqlCommand.CommandText = "SELECT * FROM PERSON WHERE id = @p1";
                    sqlCommand.Parameters.AddWithValue("p1", Id);

                    // ExecuteReader() returns data from DB but it's an Iterable.
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while(sqlDataReader.Read())
                    {
                        people.Add(new Person(
                            Convert.ToInt32(sqlDataReader["id"].ToString()),
                            sqlDataReader["name"].ToString(),
                            sqlDataReader["lastname"].ToString(),
                            DateTime.Parse(sqlDataReader["birthday"].ToString())
                        ));
                    }

                    return people.First();
                }
                catch
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Person> SearchByNameOrLastname(string text)
        {
            using(var connection = new SqlConnection(Connectionstring))
            {
                List<Person> people = new List<Person>();

                connection.Open();
                try
                {
                    SqlCommand sqlCommand = connection.CreateCommand();

                    sqlCommand.CommandText = "SELECT * FROM PERSON WHERE name LIKE @p1 OR lastname LIKE @p1";
                    sqlCommand.Parameters.AddWithValue("p1", "%" + text + "%");

                    // ExecuteReader() returns data from DB but it's an Iterable.
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while(sqlDataReader.Read())
                    {
                        people.Add(new Person(
                            Convert.ToInt32(sqlDataReader["id"].ToString()),
                            sqlDataReader["name"].ToString(),
                            sqlDataReader["lastname"].ToString(),
                            DateTime.Parse(sqlDataReader["birthday"].ToString())
                        ));
                    }

                    return people;
                }
                catch
                {
                    return new List<Person>();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void AddPerson(Person person)
        {
            using(var connection = new SqlConnection(Connectionstring))
            {
                connection.Open();
                try
                {
                    SqlCommand sqlCommand = connection.CreateCommand();

                    sqlCommand.CommandText = "INSERT INTO PERSON(name, lastname, birthday) VALUES (@p1, @p2, @p3)";
                    sqlCommand.Parameters.AddWithValue("p1", person.Name);
                    sqlCommand.Parameters.AddWithValue("p2", person.Lastname);
                    sqlCommand.Parameters.AddWithValue("p3", person.Birthday);

                    // Execute query but does not return anything;
                    sqlCommand.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdatePerson(Person person)
        {
            using(var connection = new SqlConnection(Connectionstring))
            {
                connection.Open();
                try
                {
                    SqlCommand sqlCommand = connection.CreateCommand();

                    sqlCommand.CommandText = @"UPDATE PERSON
                                                SET name = @p2,
                                                lastname = @p3,
                                                birthday = @p4
                                                WHERE id = @p1";

                    sqlCommand.Parameters.AddWithValue("p1", person.Id);
                    sqlCommand.Parameters.AddWithValue("p2", person.Name);
                    sqlCommand.Parameters.AddWithValue("p3", person.Lastname);
                    sqlCommand.Parameters.AddWithValue("p4", person.Birthday);

                    // Execute query but does not return anything;
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeletePerson(Person person)
        {
            using(var connection = new SqlConnection(Connectionstring))
            {
                connection.Open();
                try
                {
                    SqlCommand sqlCommand = connection.CreateCommand();

                    sqlCommand.CommandText = "DELETE FROM PERSON WHERE id = @p1";
                    sqlCommand.Parameters.AddWithValue("p1", person.Id);

                    // Execute query but does not return anything;
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
