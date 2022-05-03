﻿using System;
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
        /// Database File Name
        /// </summary>
        public static string DatabaseName = "SLBFE.db";

        /// <summary>
        /// Path of the database
        /// </summary>
        public static string DatabasePath = Path.Combine("SLBFE_Data", DatabaseName);


        /// <summary>
        /// Physical log to the API requests and actions
        /// </summary>
        public static string LogFilePath = Path.Combine("SLBFE_Data", "api_log.txt");

        /// <summary>
        /// Logs the input into debug and to text file
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
        /// This initialize the database
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
                        "StateProvince TEXT, " +
                        "City TEXT, " +
                        "ZipCode TEXT, " +
                        "EmployeeID TEXT, " +
                        "FilePathEmployeeIDPhoto TEXT );" +
                    "CREATE TABLE IF NOT EXISTS " +
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
                        "StateProvince TEXT, " +
                        "City TEXT, " +
                        "ZipCode TEXT, " +
                        "MapLocation TEXT, " +
                        "CurrentProfession TEXT, " +
                        "Affiliation TEXT, " +
                        "Qualifications TEXT, " +
                        "FilePathCV TEXT, " +
                        "FilePathQualifications TEXT );" +
                    "CREATE TABLE IF NOT EXISTS " +
                    "Company (" +
                        "BRNumber TEXT, " +
                        "FilePathBR TEXT, " +
                        "BusinessName TEXT, " +
                        "BusinessCategory TEXT, " +
                        "Email TEXT, " +
                        "PhoneNumber TEXT, " +
                        "BirthDate TEXT, " +
                        "Password TEXT, " +
                        "AddressL1 TEXT, " +
                        "AddressL2 TEXT, " +
                        "StateProvince TEXT, " +
                        "City TEXT, " +
                        "ZipCode TEXT );";


                    SQLiteCommand initCommand = new SQLiteCommand(dbScript, con);
                    initCommand.ExecuteNonQuery();


                    con.Close();

                    Log("Database Intilized!");


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);


                    Log("Database Intilized Failed!");
                    Log(ex.ToString());
                }
            }
        }


        /// <summary>
        /// Get all Bureau Officers from the database
        /// </summary>
        /// <returns> Return all users as a list of Bureau Officers</returns>
        public static List<Bureau> GetBureaus()
        {
            var bureauList = new List<Bureau>();

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand selectCommand = new SQLiteCommand();
                    selectCommand.CommandText = "SELECT * FROM Officer";
                    selectCommand.Connection = con;

                    var reader = selectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        bureauList.Add(
                            new Bureau()
                            {
                                NationalID = reader.GetString(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                BirthDate = DateTime.Parse(reader.GetString(5)),
                                Password = reader.GetString(6),
                                AddressL1 = reader.GetString(7),
                                AddressL2 = reader.GetString(8),
                                StateProvince = reader.GetString(9),
                                City = reader.GetString(10),
                                ZipCode = reader.GetString(11),
                                EmployeeID = reader.GetString(12),
                                FilePathEmployeeIDPhoto = reader.GetString(13),
                            });
                    }


                    return bureauList;


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    Log("Bureau Get Request Failed! (Database)");
                    Log(ex.ToString());
                }

                Log("Uncaught Error on Fetching Officers!");
                return null;
            }
        }

        /// <summary>
        /// Registers a new Bureau Officer to the System
        /// </summary>
        /// <param name="officer">Office details as an object</param>
        /// <returns>Number of rows affected or -1 if there an error with the data -2 if there is an error on the database</returns>
        public static int RegisterOfficer(Bureau officer)
        {

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand insertCommand = new SQLiteCommand();
                    insertCommand.CommandText = "INSERT INTO" +
                        " Officer " +
                        "VALUES(" +
                        "@nationalID," +
                        "@firstName," +
                        "@lastName," +
                        "@email," +
                        "@phoneNumber, " +
                        "@birthDate, " +
                        "@password, " +
                        "@addressL1, " +
                        "@addressL2, " +
                        "@stateProvince, " +
                        "@city, " +
                        "@zipCode, " +
                        "@employeeID, " +
                        "@filePathEmployeeIDPhoto)";
                    insertCommand.Connection = con;

                    insertCommand.Parameters.AddWithValue("@nationalID", officer.NationalID);
                    insertCommand.Parameters.AddWithValue("@firstName", officer.FirstName);
                    insertCommand.Parameters.AddWithValue("@lastName", officer.LastName);
                    insertCommand.Parameters.AddWithValue("@email", officer.Email);
                    insertCommand.Parameters.AddWithValue("@phoneNumber", officer.PhoneNumber);
                    insertCommand.Parameters.AddWithValue("@birthDate", officer.BirthDate.ToString("G"));
                    insertCommand.Parameters.AddWithValue("@password", officer.Password);
                    insertCommand.Parameters.AddWithValue("@addressL1", officer.AddressL1);
                    insertCommand.Parameters.AddWithValue("@addressL2", officer.AddressL2);
                    insertCommand.Parameters.AddWithValue("@stateProvince", officer.StateProvince);
                    insertCommand.Parameters.AddWithValue("@city", officer.City);
                    insertCommand.Parameters.AddWithValue("@zipCode", officer.ZipCode);
                    insertCommand.Parameters.AddWithValue("@employeeID", officer.EmployeeID);
                    insertCommand.Parameters.AddWithValue("@filePathEmployeeIDPhoto", officer.FilePathEmployeeIDPhoto);

                    var affRows = insertCommand.ExecuteNonQuery();

                    if (affRows > 0)
                    {
                        Log($"Successfully Registered User {officer.Email}");
                    }


                    return affRows > 0 ? affRows : -1; // if affected rows is larger than 0 return the affected rows number else return -1 in indicate it is an error 

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    Log("Bureau Officer Registration Failed! (Database)");
                    Log(ex.ToString());
                }
            }

            Log("Uncaught Error on Officer Registration!");
            return -2;
        }


        /// <summary>
        /// Deletes the bureau officer profile from the database
        /// </summary>
        /// <param name="nationalID">National ID of the User</param>
        /// <param name="requestedBy">Employee ID of the Officer that os requested this</param>
        /// <returns></returns>
        public static int DeleteOfficer(string nationalID, string requestedBy)
        {


            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand deleteCommand = new SQLiteCommand();
                    deleteCommand.CommandText = "DELETE FROM Officer WHERE NationalID = @nationalID";
                    deleteCommand.Connection = con;
                    deleteCommand.Parameters.AddWithValue("@nationalId", nationalID);



                    var affRows = deleteCommand.ExecuteNonQuery();


                    if (affRows > 0)
                    {
                        Log($"Bureau Officer Successfully Deleted! {nationalID} for the requested by {requestedBy}");
                    }

                    return affRows > 0 ? affRows : -1;


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    Log("Bureau Officer Deletion Failed! (Database)");
                    Log(ex.ToString());
                }


                Log("Uncaught Error on Office Deletion!");
                return -2;
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
                        var locationString = reader.GetString(12);
                        var locationData = locationString.Split(',');

                        var qualificationString = reader.GetString(15);
                        var qualificationData = qualificationString.Split(',');

                        var qualificationFilesString = reader.GetString(17);
                        var qualificationFilesData = qualificationFilesString.Split(',');

                        citizenList.Add(
                            new Citizen()
                            {
                                NationalID = reader.GetString(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                BirthDate = DateTime.Parse(reader.GetString(5)),
                                Password = reader.GetString(6),
                                AddressL1 = reader.GetString(7),
                                AddressL2 = reader.GetString(8),
                                StateProvince = reader.GetString(9),
                                City = reader.GetString(10),
                                ZipCode = reader.GetString(11),
                                MapLocation = new Location(float.Parse(locationData[0]), float.Parse(locationData[1])),
                                CurrentProfession = reader.GetString(13),
                                Affiliation = reader.GetString(14),
                                Qualifications = qualificationData.ToList(),
                                FilePathCV = reader.GetString(16),
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
                                "@stateProvince, " +
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
                    mapDataBuilder.Append(citizen.MapLocation.Latitude.ToString() + ",");
                    mapDataBuilder.Append(citizen.MapLocation.Longitude.ToString());

                    var qualificationBuilder = new StringBuilder();
                    if (citizen.Qualifications.Count > 1)
                    {
                        foreach (var qualification in citizen.Qualifications)
                        {
                            qualificationBuilder.Append(qualification + ",");
                        }
                        qualificationBuilder = qualificationBuilder.Remove(qualificationBuilder.Length - 1, 1);
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
                        foreach (var qualification in citizen.FilePathQualifications)
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

                    insertCommand.Parameters.AddWithValue("@nationalID", citizen.NationalID);
                    insertCommand.Parameters.AddWithValue("@firstName", citizen.FirstName);
                    insertCommand.Parameters.AddWithValue("@lastName", citizen.LastName);
                    insertCommand.Parameters.AddWithValue("@email", citizen.Email);
                    insertCommand.Parameters.AddWithValue("@phoneNumber", citizen.PhoneNumber);
                    insertCommand.Parameters.AddWithValue("@birthDate", citizen.BirthDate.ToString("G"));
                    insertCommand.Parameters.AddWithValue("@password", citizen.Password);
                    insertCommand.Parameters.AddWithValue("@addressL1", citizen.AddressL1);
                    insertCommand.Parameters.AddWithValue("@addressL2", citizen.AddressL2);
                    insertCommand.Parameters.AddWithValue("@stateProvince", citizen.StateProvince);
                    insertCommand.Parameters.AddWithValue("@city", citizen.City);
                    insertCommand.Parameters.AddWithValue("@zipCode", citizen.ZipCode);
                    insertCommand.Parameters.AddWithValue("@mapLocation", mapDataBuilder.ToString());
                    insertCommand.Parameters.AddWithValue("@currentProfession", citizen.CurrentProfession);
                    insertCommand.Parameters.AddWithValue("@affiliation", citizen.Affiliation);
                    insertCommand.Parameters.AddWithValue("@qualifications", qualificationBuilder.ToString());
                    insertCommand.Parameters.AddWithValue("@filePathCV", citizen.FilePathCV);
                    insertCommand.Parameters.AddWithValue("@filePathQualifications", qualificationFileBuilder.ToString());

                    var affRows = insertCommand.ExecuteNonQuery();

                    if (affRows > 0)  // if it is an successfull registration
                    {
                        Log($"Successfully Registred User {citizen.Email}");
                    }

                    return affRows > 0 ? affRows : -1;  // if affected rows is larger than 0 return the affected rows number else return -1 in indicate it is an error

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

        /// <summary>
        /// Update the officer in the system
        /// </summary>
        /// <param name="employeeID"> Officer details as an object</param> 
        /// <param name="officer">Number of rows affected or -1 if there is an error with the data -2 if there is an error on the database </param>
        /// <returns></returns>
        public static int UpdateOfficer(string employeeID, Bureau officer)
        {

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand updateCommand = new SQLiteCommand();
                    updateCommand.CommandText = "UPDATE " +
                        " Officer " +
                        " SET " +
                            "NationalID=@nationalID," +
                            "FirstName=@firstName," +
                            "LastName=@lastName," +
                            "Email=@email," +
                            "PhoneNumber=@phoneNumber, " +
                            "BirthDate=@birthDate, " +
                            "Password=@password, " +
                            "AddressL1=@addressL1, " +
                            "AddressL2=@addressL2, " +
                            "StateProvince=@stateProvince, " +
                            "City=@city, " +
                            "ZipCode=@zipCode, " +
                            "EmployeeID=@employeeID, " +
                            "FilePathEmployeeIDPhoto=@filePathEmployeeIDPhoto " +
                         "WHERE " +
                            "EmployeeID=@employeeID";
                    updateCommand.Connection = con;

                    updateCommand.Parameters.AddWithValue("@nationalID", officer.NationalID);
                    updateCommand.Parameters.AddWithValue("@firstName", officer.FirstName);
                    updateCommand.Parameters.AddWithValue("@lastName", officer.LastName);
                    updateCommand.Parameters.AddWithValue("@email", officer.Email);
                    updateCommand.Parameters.AddWithValue("@phoneNumber", officer.PhoneNumber);
                    updateCommand.Parameters.AddWithValue("@birthDate", officer.BirthDate.ToString("G"));
                    updateCommand.Parameters.AddWithValue("@password", officer.Password);
                    updateCommand.Parameters.AddWithValue("@addressL1", officer.AddressL1);
                    updateCommand.Parameters.AddWithValue("@addressL2", officer.AddressL2);
                    updateCommand.Parameters.AddWithValue("@stateProvince", officer.StateProvince);
                    updateCommand.Parameters.AddWithValue("@city", officer.City);
                    updateCommand.Parameters.AddWithValue("@zipCode", officer.ZipCode);
                    updateCommand.Parameters.AddWithValue("@employeeID", officer.EmployeeID);
                    updateCommand.Parameters.AddWithValue("@filePathEmployeeIDPhoto", officer.FilePathEmployeeIDPhoto);

                    var affRows = updateCommand.ExecuteNonQuery();

                    if (affRows > 0)
                    {
                        Log($"Successfully Updated the User {officer.Email}");
                    }


                    return affRows > 0 ? affRows : -1; // if affected rows is larger than 0 return the affected rows number else return -1 in indicate it is an error 

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    Log("Bureau Officer Updated Failed! (Database)");
                    Log(ex.ToString());
                }
            }

            Log("Uncaught Error on Office Update!");
            return -2;
        }

    }
}