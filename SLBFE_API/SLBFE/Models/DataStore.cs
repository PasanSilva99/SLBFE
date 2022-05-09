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
            // this will create the file if not exists to save the data
            Directory.CreateDirectory("SLBFE_Data");

            if (!File.Exists(DatabasePath))  // check wether the database file exissts
                File.Create(DatabasePath);  // if it is not there, Create it. 

            // As for the above lines, the database will be autometically created within the forst run. 
            // If it fails at any point, Please Leave a message in discord. I will look into it. 
            // put that message on #project-discussion not in the #resources\
            // in the Tecxick Study group Server/

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
                        "FilePathQualifications TEXT, " +
                        "FilePathBirthCertificate TEXT, " +
                        "FilePathPassport TEXT );" +
                    "CREATE TABLE IF NOT EXISTS " +
                    "Company (" +
                        "BRNumber TEXT, " +
                        "FilePathBR TEXT, " +
                        "BusinessName TEXT, " +
                        "BusinessCategory TEXT, " +
                        "Email TEXT, " +
                        "PhoneNumber TEXT, " +
                        "Password TEXT, " +
                        "AddressL1 TEXT, " +
                        "AddressL2 TEXT, " +
                        "StateProvince TEXT, " +
                        "City TEXT, " +
                        "ZipCode TEXT );" +
                    "CREATE TABLE IF NOT EXISTS " +
                    "UserValidation (" +
                        "NationalID TEXT, " +  // Dational ID of the user that needs to be validated
                        "isApproved TEXT, " +  // Id this user approved?
                        "Changes TEXT, " +  // Is there any changes that needs to be done?
                        "EmoployeeID TEXT );" +  // ID of the employee that reviwed this user account
                    "CREATE TABLE IF NOT EXISTS " +
                    "Feedback (" +
                        "ID TEXT, " +  // Auto generated ID for this Feedbaclk
                        "Email TEXT, " +  // Email of the user that posted this
                        "Usename TEXT, " +  // User name of the user that posted this
                        "isComplaint INTEGER, " +  // This ithis a complaint?
                        "CompanyID TEXT, " +  // Company ID which is related to this feedback
                        "CompanyName TEXT, " +  // Company Name which is related to this feedback
                        "SentDate TEXT, " +  // The date that pugblished this feedback
                        "Content TEXT );"+  // Content of the feedback
                    "CREATE TABLE IF NOT EXISTS " +
                    "FeedbackReply (" +
                        "FeedbackID TEXT, " +  // Which Feedback that this reply belongs to
                        "Email TEXT, " +  // Email of the user that posted this
                        "Usename TEXT, " +  // User name of the user that posted this
                        "SentDate TEXT, " +  // Date thst is this replay added
                        "Content TEXT );"+  // Content of the reply
                    "CREATE TABLE IF NOT EXISTS " +
                    "Interview (" +
                        "InterviewID TEXT, " +  // Auto generated ID of teh Interview
                        "CompanyID TEXT, " +  // ID of the company that posted this interview
                        "CompanyName TEXT, " +  // Name of the company that posted this interview
                        "UserEmail TEXT, " +  // EMail of the user that should receive this 
                        "SentDate TEXT, " +  // The date that sent this interview
                        "State TEXT, " +  // Is accepted or not 
                        "Content TEXT );";  // Description and any content that is related to this interview post


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

        #region Bureau Functions

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
        /// Updates the validation request of the citizen
        /// </summary>
        /// <param name="validationData"></param>
        /// <returns></returns>
        public static int ValidateCitizen(UserValidation validationData)
        {
            Log($"Validation Request Update For {validationData.NationalID}");

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                con.Open();
                string insertQuarry = "" +
                    "UPDATE " +
                        "UserValidation " +
                    "SET " +
                        "isApproved=@isapproved, " +
                        "@Changes=changes, " +
                        "EmoployeeID=@employeeID " +
                    "WHERE " +
                        "NationalID=@nationalID;";

                SQLiteCommand insertCommand = new SQLiteCommand(insertQuarry, con);
                insertCommand.Parameters.AddWithValue("@nationalID", validationData.NationalID);
                insertCommand.Parameters.AddWithValue("@isapproved", validationData.IsApproved ? 1 : 0);
                insertCommand.Parameters.AddWithValue("@changes", validationData.Changes);
                insertCommand.Parameters.AddWithValue("@employeeID", validationData.EmployeeID);

                var affRows = insertCommand.ExecuteNonQuery();

                if (affRows > 0)
                {
                    Log($"Validation Request Update For {validationData.NationalID} Saved Successfully");
                    return 1;
                }
            }


            // The is an un identified Error
            Log("Unrecognized Error in the user validation data");
            return 0;
        }

        /// <summary>
        /// Updates the validation request of the citizen
        /// </summary>
        /// <param name="nationalID"></param>
        /// <returns></returns>
        private static int ValidateCitizen(string nationalID)
        {
            Log($"Validation Request Update For {nationalID}");

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                con.Open();
                string insertQuarry = "" +
                    "UPDATE " +
                        "UserValidation " +
                    "SET " +
                        "isApproved=@isapproved, " +
                        "@Changes=changes, " +
                        "EmoployeeID=@employeeID " +
                    "WHERE " +
                        "NationalID=@nationalID;";

                SQLiteCommand insertCommand = new SQLiteCommand(insertQuarry, con);
                insertCommand.Parameters.AddWithValue("@nationalID", nationalID);
                insertCommand.Parameters.AddWithValue("@isapproved", 0);
                insertCommand.Parameters.AddWithValue("@changes", "");
                insertCommand.Parameters.AddWithValue("@employeeID", "");

                var affRows = insertCommand.ExecuteNonQuery();

                if (affRows > 0)
                {
                    Log($"Validation Request Update For {nationalID} Saved Successfully");
                    return 1;
                }
            }


            // The is an un identified Error
            Log("Unrecognized Error in the user validation data");
            return 0;
        }

        private static int DeleteValidationRequest(string nationalID, string employeeID)
        {
            Log($"Validate Request Data Deletion Requested for account {nationalID} by {employeeID}");

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                con.Open();
                string insertQuarry = "" +
                    "DELETE FROM " +
                        "UserValidation " +
                    "WHERE " +
                        "NationalID=@nationalID;";

                SQLiteCommand insertCommand = new SQLiteCommand(insertQuarry, con);
                insertCommand.Parameters.AddWithValue("@nationalID", nationalID);

                var affRows = insertCommand.ExecuteNonQuery();

                if (affRows > 0)
                {
                    Log($"Successfully Deleted Validation Data for {nationalID} as requested by {employeeID}");
                    return 1;
                }
            }


            // The is an un identified Error
            Log("Unrecognized Error in the user validation data");
            return 0;
        }

        #endregion

        #region Citizen Functions

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
                                FilePathQualifications = qualificationFilesData.ToList(),
                                FilePathBirthCertificate = reader.GetString(17),
                                FilePathPassport = reader.GetString(18),
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
            Log("Uncaught Error on Citizen Get Request");

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
                                "@filePathQualifications, " +
                                "@FilePathBirthCertificate, " +
                                "@FilePathPassport)";
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
                    insertCommand.Parameters.AddWithValue("@FilePathBirthCertificate", citizen.FilePathBirthCertificate);
                    insertCommand.Parameters.AddWithValue("@FilePathPassport", citizen.FilePathPassport);

                    var affRows = insertCommand.ExecuteNonQuery();

                    if (affRows > 0)  // if it is an successfull registration
                    {
                        Log($"Successfully Registred User {citizen.Email}");

                        NewValidationRequest(citizen.NationalID);

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
            Log("Uncaught Error on Citizen Registration");
            return -2;
        }

        /// <summary>
        /// Updates a Cizien in the system
        /// </summary>
        /// <param name="citizen">Citizens details as an object</param>
        /// <param name="nationalID">National ID number of the Citizen that needs to be updated</param>
        /// <returns>Number of rows affected or -1 if there is an error with the data -2 if there is an error on the database</returns>
        public static int UpdateCitizen(string nationalID, Citizen citizen)
        {
            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand updateCommand = new SQLiteCommand();
                    updateCommand.CommandText = "UPDATE " +
                        "Citizen " +
                            "SET " +
                                "FirstName=@firstName, " +
                                "LastName=@lastName, " +
                                "Email=@email, " +
                                "PhoneNumber=@phoneNumber, " +
                                "BirthDate=@birthDate, " +
                                "Password=@password, " +
                                "AddressL1=@addressL1, " +
                                "AddressL2=@addressL2, " +
                                "StateProvince=@stateProvince, " +
                                "City=@city, " +
                                "ZipCode=@zipCode, " +
                                "MapLocation=@mapLocation, " +
                                "CurrentProfession=@currentProfession, " +
                                "Affiliation=@affiliation, " +
                                "Qualifications=@qualifications, " +
                                "FilePathCV=@filePathCV, " +
                                "FilePathQualifications=@filePathQualifications " +
                                "FilePathBirthCertificate=@filePathBirthCertificate " +
                                "FilePathPassport=@filePathPassport " +
                             "WHERE " +
                                "NationalID=@nationalID ";
                    updateCommand.Connection = con;

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

                    updateCommand.Parameters.AddWithValue("@nationalID", nationalID);
                    updateCommand.Parameters.AddWithValue("@firstName", citizen.FirstName);
                    updateCommand.Parameters.AddWithValue("@lastName", citizen.LastName);
                    updateCommand.Parameters.AddWithValue("@email", citizen.Email);
                    updateCommand.Parameters.AddWithValue("@phoneNumber", citizen.PhoneNumber);
                    updateCommand.Parameters.AddWithValue("@birthDate", citizen.BirthDate.ToString("G"));
                    updateCommand.Parameters.AddWithValue("@password", citizen.Password);
                    updateCommand.Parameters.AddWithValue("@addressL1", citizen.AddressL1);
                    updateCommand.Parameters.AddWithValue("@addressL2", citizen.AddressL2);
                    updateCommand.Parameters.AddWithValue("@stateProvince", citizen.StateProvince);
                    updateCommand.Parameters.AddWithValue("@city", citizen.City);
                    updateCommand.Parameters.AddWithValue("@zipCode", citizen.ZipCode);
                    updateCommand.Parameters.AddWithValue("@mapLocation", mapDataBuilder.ToString());
                    updateCommand.Parameters.AddWithValue("@currentProfession", citizen.CurrentProfession);
                    updateCommand.Parameters.AddWithValue("@affiliation", citizen.Affiliation);
                    updateCommand.Parameters.AddWithValue("@qualifications", qualificationBuilder.ToString());
                    updateCommand.Parameters.AddWithValue("@filePathCV", citizen.FilePathCV);
                    updateCommand.Parameters.AddWithValue("@filePathQualifications", qualificationFileBuilder.ToString());
                    updateCommand.Parameters.AddWithValue("@FilePathBirthCertificate", citizen.FilePathBirthCertificate);
                    updateCommand.Parameters.AddWithValue("@FilePathPassport", citizen.FilePathPassport);

                    var affRows = updateCommand.ExecuteNonQuery();

                    if (affRows > 0)  // if it is an successfull registration
                    {
                        Log($"Successfully Updated User {citizen.Email}");
                        ValidateCitizen(citizen.NationalID);
                    }

                    return affRows > 0 ? affRows : -1;  // if affected rows is larger than 0 return the affected rows number else return -1 in indicate it is an error

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    // Log the error to the API log
                    Log("Citizen Updated Failed (Database)");
                    Log(ex.ToString());
                }
            }

            // Log the error to the API log
            Log("Uncaught Error on Citizen Updated");
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
                        DeleteValidationRequest(nationalID, requestedBy);
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
            Log("Uncaught Error on Citizen deletion");
            return -2;
        }

        private static bool NewValidationRequest(string nationalID)
        {
            Log($"New Validation Request For {nationalID}");

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                con.Open();
                string insertQuarry = "INSERT INTO UserValidation VALUES(@nationalID, @isapproved, @changes, @employeeID)";
                SQLiteCommand insertCommand = new SQLiteCommand(insertQuarry, con);
                insertCommand.Parameters.AddWithValue("@nationalID", nationalID);
                insertCommand.Parameters.AddWithValue("@isapproved", 0);
                insertCommand.Parameters.AddWithValue("@changes", "");
                insertCommand.Parameters.AddWithValue("@employeeID", "");

                var affRows = insertCommand.ExecuteNonQuery();

                if (affRows > 0)
                {
                    Log($"New Validation Request For {nationalID} Saved Successfully");
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region Commpany DB Functions

        #region Commpany Registers
        /// <summary>
        /// Registers a new Commpanyr to the System
        /// </summary>
        /// <param name="Commpany">Commpany details as an object</param>
        /// <returns>Number of rows affected or -1 if there an error with the data -2 if there is an error on the database</returns>
        public static int RegisterCommpany(Commpany commpany)
        {

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand insertCommand = new SQLiteCommand();
                    insertCommand.CommandText = "INSERT INTO " +
                     "Company " +
                        "VALUES(" +
                        "@brNumber," +
                        "@filePathBR," +
                        "@businessName," +
                        "@businessCategory," +
                        "@email," +
                        "@phoneNumber, " +
                        "@password, " +
                        "@addressL1, " +
                        "@addressL2, " +
                        "@stateProvince, " +
                        "@city, " +
                        "@zipCode );";
                    insertCommand.Connection = con;

                    insertCommand.Parameters.AddWithValue("@brNumber", commpany.BRNumber);
                    insertCommand.Parameters.AddWithValue("@filePathBR", commpany.FilePathBR);
                    insertCommand.Parameters.AddWithValue("@businessName", commpany.BusinessName);
                    insertCommand.Parameters.AddWithValue("@businessCategory", commpany.BusinessCategory);
                    insertCommand.Parameters.AddWithValue("@email", commpany.Email);
                    insertCommand.Parameters.AddWithValue("@phoneNumber", commpany.PhoneNumber);
                    insertCommand.Parameters.AddWithValue("@password", commpany.Password);
                    insertCommand.Parameters.AddWithValue("@addressL1", commpany.AddressL1);
                    insertCommand.Parameters.AddWithValue("@addressL2", commpany.AddressL2);
                    insertCommand.Parameters.AddWithValue("@stateProvince", commpany.StateProvince);
                    insertCommand.Parameters.AddWithValue("@city", commpany.City);
                    insertCommand.Parameters.AddWithValue("@zipCode", commpany.ZipCode);

                    var affRows = insertCommand.ExecuteNonQuery();

                    if (affRows > 0)
                    {
                        Log($"Successfully Registered Commpany {commpany.Email}");
                    }


                    return affRows > 0 ? affRows : -1; // if affected rows is larger than 0 return the affected rows number else return -1 in indicate it is an error 

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    Log("Commpany Registration Failed! (Database)");
                    Log(ex.ToString());
                }
            }

            Log("Uncaught Error on Commpany Registration!");
            return -2;
        }
        #endregion

        #region Commpany Data Retrieve
        /// <summary>
        /// Get all Commpanies from the database
        /// </summary>
        /// <returns> Return all Commpanies as a list of Commpanies</returns>
        public static List<Commpany> GetCommpany()
        {
            var commpanyList = new List<Commpany>();

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand selectCommand = new SQLiteCommand();
                    selectCommand.CommandText = "SELECT * FROM Company";
                    selectCommand.Connection = con;

                    var reader = selectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        commpanyList.Add(
                             new Commpany()
                             {
                                 BRNumber = reader.GetString(0),
                                 FilePathBR = reader.GetString(1),
                                 BusinessName = reader.GetString(2),
                                 BusinessCategory = reader.GetString(3),
                                 Email = reader.GetString(4),
                                 PhoneNumber = reader.GetString(5),
                                 Password = reader.GetString(6),
                                 AddressL1 = reader.GetString(7),
                                 AddressL2 = reader.GetString(8),
                                 StateProvince = reader.GetString(9),
                                 City = reader.GetString(10),
                                 ZipCode = reader.GetString(11)
                             });
                    }


                    return commpanyList;


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    Log("Commpany Get Request Failed! (Database)");
                    Log(ex.ToString());
                }

                Log("Uncaught Error on Fetching commpanies!");
                return null;
            }
        }
        #endregion

        #region Company Detail Update
        public static int UpdateCompany(string BRNumber, Commpany company)
        {
            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand updateCommand = new SQLiteCommand();
                    updateCommand.CommandText = "UPDATE " +
                        "Company " +
                            "SET " +
                                "FilePathBR=@filePathBR, " +
                                "BusinessName=@businessName, " +
                                "BusinessCategory=@businessCategory, " +
                                "Email=@email, " +
                                "PhoneNumber=@phoneNumber, " +
                                "Password=@password, " +
                                "AddressL1=@addressL1, " +
                                "AddressL2=@addressL2, " +
                                "StateProvince=@stateProvince, " +
                                "City=@city, " +
                                "ZipCode=@zipCode " +

                             "WHERE " +
                                "BRNumber=@brNumber";
                    updateCommand.Connection = con;



                    updateCommand.Parameters.AddWithValue("@filePathBR", company.FilePathBR);
                    updateCommand.Parameters.AddWithValue("@businessName", company.BusinessName);
                    updateCommand.Parameters.AddWithValue("@brNumber", company.BRNumber);
                    updateCommand.Parameters.AddWithValue("@businessCategory", company.BusinessCategory);
                    updateCommand.Parameters.AddWithValue("@email", company.Email);
                    updateCommand.Parameters.AddWithValue("@phoneNumber", company.PhoneNumber);
                    updateCommand.Parameters.AddWithValue("@password", company.Password);
                    updateCommand.Parameters.AddWithValue("@addressL1", company.AddressL1);
                    updateCommand.Parameters.AddWithValue("@addressL2", company.AddressL2);
                    updateCommand.Parameters.AddWithValue("@stateProvince", company.StateProvince);
                    updateCommand.Parameters.AddWithValue("@city", company.City);
                    updateCommand.Parameters.AddWithValue("@zipCode", company.ZipCode);


                    var affRows = updateCommand.ExecuteNonQuery();

                    if (affRows > 0)  // if it is an successfull registration
                    {
                        Log($"Successfully updated company {company.Email}");
                    }

                    return affRows > 0 ? affRows : -1;  // if affected rows is larger than 0 return the affected rows number else return -1 in indicate it is an error

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    // Log the error to the API log
                    Log("company Updated Failed (Database)");
                    Log(ex.ToString());
                }
            }

            // Log the error to the API log
            Log("Uncaught Error on company Updated");
            return -2;
        }
        #endregion

        #region Company Data Delete
        public static int DeleteCompany(string BRNumber, string requestedBy)
        {

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand deleteCommand = new SQLiteCommand();
                    deleteCommand.CommandText = "DELETE FROM Company WHERE BRNumber=@brNumber";
                    deleteCommand.Connection = con;
                    deleteCommand.Parameters.AddWithValue("@brNumber", BRNumber);



                    var affRows = deleteCommand.ExecuteNonQuery();

                    if (affRows > 0)  // if it is an successfull deletion
                    {
                        Log($"Successfully Deleted company {BRNumber} for the Request by {requestedBy}");
                    }

                    return affRows > 0 ? affRows : -1;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    // Log the error to the API log
                    Log("Company deletion Failed (Database)");
                    Log(ex.ToString());

                }
            }

            // Log the error to the API log
            Log("Uncaught Error on company deletion");
            return -2;
        }
        #endregion

        #endregion


    }
}