using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SLBFE.Models
{
    /// <summary>
    /// Database Related Functions
    /// </summary>
    public static class DataStore
    {
        /// <summary>
        /// Databse filename
        /// </summary>
        public static string DatabaseName = "SLBFE.db";

        /// <summary>
        /// Path of the database
        /// </summary>
        public static string DatabasePath = Path.Combine("SLBFE_Data", DatabaseName);

        /// <summary>
        /// Physical log of the API requests and actions
        /// </summary>
        public static string LogFilePath = Path.Combine("SLBFE_Data", "api_log.txt");

        /// <summary>
        /// Logs the input into debug and to atext file
        /// </summary>
        /// <param name="val"></param>
        public static void Log(string val)
        {
            var logString = $"[{DateTime.Now.ToString("G")}] >> {val}";
            // log the request to the file
            File.AppendAllText(LogFilePath, logString + "\n");

            Debug.WriteLine(logString);
            // [5/2/2022 6:53:21 PM] >> text to log 
        }

        /// <summary>
        /// This initializes the database
        /// </summary>
        public static void InitializeDatabase()
        {
            Directory.CreateDirectory("SLBFE_Data");

            if (!File.Exists(DatabasePath))
                File.Create(DatabasePath);

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    string dbScript = "CREATE TABLE IF NOT EXISTS " +
                    "Citizen (" +
                        "NationalID TEXT, " +
                        "FirstName TEXT, " +
                        "LastName TEXT, " +
                        "Email TEXT, " +
                        "PhoneNumber TEXT, " +
                        "BirthDate TEXT, " +
                        "Password TEXT, " +
                        "AddressL1 TEXT, " +
                        "AddressL2 TEXT, " +
                        "City TEXT, " +
                        "ZipCode TEXT, " +
                        "MapLocation TEXT, " +
                        "CurrentProfession TEXT, " +
                        "Affiliation TEXT, " +
                        "Qualifications TEXT, " +
                        "FilePathCV TEXT, " +
                        "FilePathQualifications TEXT ); " +
                    "CREATE TABLE IF NOT EXISTS " +
                    "Officer (" +
                        "NationalID TEXT, " +
                        "FirstName TEXT, " +
                        "LastName TEXT, " +
                        "Email TEXT, " +
                        "PhoneNumber TEXT, " +
                        "BirthDate TEXT, " +
                        "Password TEXT, " +
                        "AddressL1 TEXT, " +
                        "AddressL2 TEXT, " +
                        "City TEXT, " +
                        "ZipCode TEXT, " +
                        "EmployeeID TEXT, " +
                        "FilePathEmolyeeIDPhoto TEXT ); ";

                    SQLiteCommand intiCommand  = new SQLiteCommand(dbScript, con);
                    intiCommand.ExecuteNonQuery();

                    con.Close();

                    Log("Database Initialized");


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    // Log the error to the API log
                    Log("Database Initialization Failed");
                    Log(ex.ToString());
                }
            }
        }
    

        /// <summary>
        /// Get all Citizens from the database
        /// </summary>
        /// <returns>Retuns all the users as a list of Citizens</returns>
        public static List<Citizen> GetCitizens()
        {
            var citizenList = new List<Citizen>();

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand selectCommand = new SQLiteCommand();
                    selectCommand.CommandText = "SELECT * FROM Citizen";
                    selectCommand.Connection = con;

                    var reader = selectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var locationString = reader.GetString(11);
                        var locationData = locationString.Split(',');

                        var qualificationString = reader.GetString(14);
                        var qualificationData = qualificationString.Split(',');

                        var qualificationFilesString = reader.GetString(16);
                        var qualificationFilesData = qualificationFilesString.Split(',');

                        citizenList.Add(
                            new Citizen()
                            {
                                NationalId = reader.GetString(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                BirthDate = DateTime.Parse(reader.GetString(5)),
                                Password = reader.GetString(6),
                                AddressL1 = reader.GetString(7),
                                AddressL2 = reader.GetString(8),
                                City = reader.GetString(9),
                                ZipCode = reader.GetString(10),
                                MapLocation = new Location(float.Parse(locationData[0]), float.Parse(locationData[1])),
                                CurrentProfession = reader.GetString(12),
                                Affiliation = reader.GetString(13),
                                Qualifications = qualificationData.ToList(),
                                FilePathCV = reader.GetString(15),
                                FilePathQualifications = qualificationFilesData.ToList()
                            }); ;
                    }

                    return citizenList;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    // Log the error to the API log
                    Log("Citizen Get Request Failed (Database)");
                    Log(ex.ToString());
                }
            }

            // Log the error to the API log
            Log("Uncaught Error on Citizen Get Request (Database)");

            return null;
        }

        /// <summary>
        /// Registers a new Cizien to the system
        /// </summary>
        /// <param name="citizen">Citizens details as an object</param>
        /// <returns>Number of rows affected or -1 if there is an error with the data -2 if there is an error on the database</returns>
        public static int RegisterCitizen(Citizen citizen)
        {
            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand insertCommand = new SQLiteCommand();
                    insertCommand.CommandText = "INSERT INTO " +
                        "Citizen " +
                            "VALUES(" +
                                "@nationalID, " +
                                "@firstName, " +
                                "@lastName, " +
                                "@email, " +
                                "@phoneNumber, " +
                                "@birthDate, " +
                                "@password, " +
                                "@addressL1, " +
                                "@addressL2, " +
                                "@city, " +
                                "@zipCode, " +
                                "@mapLocation, " +
                                "@currentProfession, " +
                                "@affiliation, " +
                                "@qualifications, " +
                                "@filePathCV, " +
                                "@filePathQualifications)";
                    insertCommand.Connection = con;

                    var mapDataBuilder = new StringBuilder();
                    mapDataBuilder.Append(citizen.MapLocation.Latitude.ToString()+",");
                    mapDataBuilder.Append(citizen.MapLocation.Longitude.ToString());

                    var qualificationBuilder = new StringBuilder();
                    if (citizen.Qualifications.Count > 1)
                    {
                        foreach(var qualification in citizen.Qualifications)
                        {
                            qualificationBuilder.Append(qualification + ",");
                        }
                        qualificationBuilder = qualificationBuilder.Remove(qualificationBuilder.Length-1, 1);
                    }
                    else if (citizen.Qualifications.Count == 1)
                    {
                        qualificationBuilder.Append(citizen.Qualifications.ToArray()[0]);
                    }
                    else
                    {
                        qualificationBuilder.Append("");
                    }

                    var qualificationFileBuilder = new StringBuilder();
                    if (citizen.Qualifications.Count > 1)
                    {
                        foreach (var qualification in citizen.Qualifications)
                        {
                            qualificationFileBuilder.Append(qualification + ",");
                        }
                        qualificationFileBuilder = qualificationFileBuilder.Remove(qualificationFileBuilder.Length - 1, 1);
                    }
                    else if (citizen.Qualifications.Count == 1)
                    {
                        qualificationFileBuilder.Append(citizen.Qualifications.ToArray()[0]);
                    }
                    else
                    {
                        qualificationFileBuilder.Append("");
                    }

                    insertCommand.Parameters.AddWithValue("@nationalID", citizen.NationalId);
                    insertCommand.Parameters.AddWithValue("@firstName", citizen.FirstName);
                    insertCommand.Parameters.AddWithValue("@lastName", citizen.LastName);
                    insertCommand.Parameters.AddWithValue("@email", citizen.Email);
                    insertCommand.Parameters.AddWithValue("@phoneNumber", citizen.PhoneNumber);
                    insertCommand.Parameters.AddWithValue("@birthDate", citizen.BirthDate.ToString("G"));
                    insertCommand.Parameters.AddWithValue("@password", citizen.Password);
                    insertCommand.Parameters.AddWithValue("@addressL1", citizen.AddressL1);
                    insertCommand.Parameters.AddWithValue("@addressL2", citizen.AddressL2);
                    insertCommand.Parameters.AddWithValue("@city", citizen.City);
                    insertCommand.Parameters.AddWithValue("@zipCode", citizen.ZipCode);
                    insertCommand.Parameters.AddWithValue("@mapLocation", mapDataBuilder.ToString());
                    insertCommand.Parameters.AddWithValue("@currentProfession", citizen.CurrentProfession);
                    insertCommand.Parameters.AddWithValue("@affiliation", citizen.Affiliation);
                    insertCommand.Parameters.AddWithValue("@qualifications", qualificationBuilder.ToString());
                    insertCommand.Parameters.AddWithValue("@filePathCV", citizen.FilePathCV);
                    insertCommand.Parameters.AddWithValue("@filePathQualifications", qualificationFileBuilder.ToString());

                    var affRows = insertCommand.ExecuteNonQuery();

                    if(affRows > 0)  // if it is an successfull registration
                    {
                        Log($"Successfully Registred User {citizen.Email}");
                    }

                    return affRows>0 ? affRows : -1;  // if affected rows is larger than 0 return the affected rows number else return -1 in indicate it is an error

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    // Log the error to the API log
                    Log("Citizen Registration Failed (Database)");
                    Log(ex.ToString());
                }
            }

            // Log the error to the API log
            Log("Uncaught Error on Citizen Registration (Database)");
            return -2;
        }

        /// <summary>
        /// Deletes the citizen profile from the database
        /// </summary>
        /// <param name="nationalID">National ID of the User</param>
        /// <param name="requestedBy">Employee ID of the Officer that os requested this</param>
        /// <returns></returns>
        public static int DeleteCitizen(string nationalID, string requestedBy)
        {
            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand deleteCommand = new SQLiteCommand();
                    deleteCommand.CommandText = "DELETE FROM Citizen WHERE NationalID=@nationalID";
                    deleteCommand.Connection = con;
                    deleteCommand.Parameters.AddWithValue("@nationalID", nationalID);



                    var affRows = deleteCommand.ExecuteNonQuery();

                    if (affRows > 0)  // if it is an successfull deletion
                    {
                        Log($"Successfully Deleted User {nationalID} for the Request by {requestedBy}");
                    }

                    return affRows > 0 ? affRows : -1;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    // Log the error to the API log
                    Log("Citizen deletion Failed (Database)");
                    Log(ex.ToString());

                }
            }

            // Log the error to the API log
            Log("Uncaught Error on Citizen deletion (Database)");
            return -2;
        }
    }
}