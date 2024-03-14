
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using NCB_DPC_FRONT.Models;
using NCB_DPC_FRONT.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NCB_DPC_FRONT.DAL
{
    public class DBDataAccess
    {
        public class SlotInfo
        {
            public int Allocated { get; set; }
            public int Allowed { get; set; }
        }

        public class CoursePrice
        {
            public float Price { get; set; }
        }
        //private System.Net.NetworkCredential spCredentials;
        //private string sharepointURL;
        //private string spUserName;
        //private string spPassword;
        //private string spDomain;

        //private SqlConnection conn;


        //private System.Data.SqlClient.SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString);




        //spUserName = GlobalVar.SPUserName;
        //spPassword = GlobalVar.SPPassword;
        //spDomain = GlobalVar.SPDomain;

        //spCredentials = new System.Net.NetworkCredential(spUserName, spPassword, spDomain);

        public DBDataAccess()
            {
                //conn = new SqlConnection(GlobalVar.ConnectionString);
            }

        public bool UpdateApplicationPaymentStatus(string appId)
        {
            bool insertionSuccessful = false;

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdateDPCApplicationPaymentStatus", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppId", appId);
                        command.Parameters.AddWithValue("@PaymentStatus", "Approved");
                        //command.Parameters.AddWithValue("@applicationStatus", "Completed");
                        int rowsAffected = command.ExecuteNonQuery();
                        insertionSuccessful = rowsAffected > 0;
                    }
                });
            }

            return insertionSuccessful;
        }


        public bool InsertIntoDPCSBMPayment(string appId, string successIndicator, string date, string status)
        {
            bool insertionSuccessful = false;

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("sp_Insert_DPC_Success_Payment", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@appId", appId);
                        command.Parameters.AddWithValue("@successIndicator", successIndicator);
                        command.Parameters.AddWithValue("@date", date);
                        command.Parameters.AddWithValue("@status", status);
                        int rowsAffected = command.ExecuteNonQuery();
                        insertionSuccessful = rowsAffected > 0;
                    }
                });
            }

            return insertionSuccessful;
        }

        public bool UpdateScratchCard(float key, string value)
        {
            bool insertionSuccessful = false;
            
            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdateDPCScratchcard", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SerialNum", key);
                        command.Parameters.AddWithValue("@Pin", value);
                        int rowsAffected = command.ExecuteNonQuery();
                        insertionSuccessful = rowsAffected > 0;
                    }
                });
            }

            return insertionSuccessful;
        }

        public Dictionary<float, string> GetAllScratchcardNumbers()
        {
            Dictionary<float, string> scratchCardDict = new Dictionary<float, string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        conn.Open();

                        string query = "SELECT SerialNumber, PIN FROM DPC_ScratchCard WHERE Status = @status;";

                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@status", "New");
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            float key = float.Parse(reader["SerialNumber"].ToString());
                            string value = reader["PIN"].ToString();

                            scratchCardDict.Add(key, value);
                        }

                        reader.Close();
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred during the operation: " + ex.Message);
            }

            return scratchCardDict;
        }



        public string CheckIfCardIsUsed(string serialNo)
        {
            string status = "";
            // Assuming you have a SQL query to fetch the data
            string query = "select * from DPC_ScratchCard where SerialNumber = @serialNo";

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@serialNo", serialNo);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            status = reader["Status"].ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }

            return status;
            // Set Attachments
        }

        public bool UpdateBatchSlotUsingStoredProcedure(string batchRef)
        {
            bool insertionSuccessful = false;

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("sp_DeductDPCSlot", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Batch_Reference", batchRef);
                        int rowsAffected = command.ExecuteNonQuery();
                        insertionSuccessful = rowsAffected > 0;
                    }
                });
            }

            return insertionSuccessful;
        }



        //public CourseDetailsModel GetCourseDetails(string batchNumber)
        //{
        //    CourseDetailsModel courseDetails = new CourseDetailsModel();
        //    int count = 0;

        //    SPSecurity.RunWithElevatedPrivileges(delegate ()
        //    {
        //        conn.Open();

        //        var selectQuery = "SELECT * FROM TABLENAME WHERE BATCH = @batchNumber";
        //        SqlCommand cmd = new SqlCommand(selectQuery, conn);
        //        cmd.Parameters.AddWithValue("@batchNumber", batchNumber);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        count = dt.Rows.Count;

        //        //Insert values into courseDetails
        //        var variable1 = dt.Rows[0][0].ToString();
        //        var variable2 = dt.Rows[0][1].ToString();

        //        conn.Close();
        //    });

        //    return courseDetails;
        //}




        public bool InsertDataUsingStoredProcedure(
            string batch, string trainingCentre, string days, string time, string duration, string batchDetails, string startDate, string endDate, string isCitizen, string nationality,
            string nid, string passport, string title, string surname, string otherNames, string maidenName, string dob, string age, string gender, string postCode,
            string address1, string address2, string address3, string telHome, string telOffice, string telMobile, string email, string occupation, string jobTitle, string sector,
            string grade, string other, string employerName, string applicationDate, string signature, string paymentMethod, string serialNumber, string pin, string appID, 
            string category, string ApplicantSignature, string successIndicator)
        {
            bool insertionSuccessful = false;

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("sp_InsertDPCApplication", conn))
                        {
                            conn.Open();

                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Clear();

                            command.Parameters.AddWithValue("@Batch", batch);
                            command.Parameters.AddWithValue("@TrainingCentre", trainingCentre);
                            command.Parameters.AddWithValue("@NumberOfDays", days);
                            command.Parameters.AddWithValue("@CourseTime", time);
                            command.Parameters.AddWithValue("@Duration", duration);
                            command.Parameters.AddWithValue("@BatchDetails", batchDetails);
                            command.Parameters.AddWithValue("@StartDate", startDate);
                            command.Parameters.AddWithValue("@EndDate", endDate);
                            command.Parameters.AddWithValue("@IsCitizen", isCitizen);
                            command.Parameters.AddWithValue("@Nationality", nationality);
                            command.Parameters.AddWithValue("@NID", nid);
                            command.Parameters.AddWithValue("@Passport", passport);
                            command.Parameters.AddWithValue("@Title", title);
                            command.Parameters.AddWithValue("@Surname", surname);
                            command.Parameters.AddWithValue("@OtherNames", otherNames);
                            command.Parameters.AddWithValue("@MaidenName", maidenName);
                            command.Parameters.AddWithValue("@DateOfBirth", dob);
                            command.Parameters.AddWithValue("@Age", age);
                            command.Parameters.AddWithValue("@Gender", gender);
                            command.Parameters.AddWithValue("@AddressPostCode", postCode);
                            command.Parameters.AddWithValue("@AddressLine1", address1);
                            command.Parameters.AddWithValue("@AddressLine2", address2);
                            command.Parameters.AddWithValue("@AddressLine3", address3);
                            command.Parameters.AddWithValue("@TelephoneHome", telHome);
                            command.Parameters.AddWithValue("@TelephoneOffice", telOffice);
                            command.Parameters.AddWithValue("@TelephoneMobile", telMobile);
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@Occupation", occupation);
                            command.Parameters.AddWithValue("@JobTitle", jobTitle);
                            command.Parameters.AddWithValue("@Sector", sector);
                            command.Parameters.AddWithValue("@Grade", grade);
                            command.Parameters.AddWithValue("@Other", other);
                            command.Parameters.AddWithValue("@EmployerName", employerName);
                            command.Parameters.AddWithValue("@ApplicationDate", applicationDate);
                            command.Parameters.AddWithValue("@Signature", signature);
                            command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                            command.Parameters.AddWithValue("@SerialNumber", serialNumber);
                            command.Parameters.AddWithValue("@PIN", pin);
                            command.Parameters.AddWithValue("@ApplicationID", appID);
                            command.Parameters.AddWithValue("@PaymentStatus", "Pending");
                            command.Parameters.AddWithValue("@Category", "Applicant");
                            command.Parameters.AddWithValue("@ApplicationStatus", "Not Completed");
                            command.Parameters.AddWithValue("@ApplicantSignature", ApplicantSignature);
                            command.Parameters.AddWithValue("@SuccessIndicator", successIndicator);



                            int rowsAffected = command.ExecuteNonQuery();

                            insertionSuccessful = rowsAffected > 0;
                            // Check if the insertion was successful
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                });
            }

            return insertionSuccessful;
        }


        public string GetBatchNumber(string batchRef)
        {
            string batch_Number = "";
            // Assuming you have a SQL query to fetch the data
            string query = "SELECT BatchNumber FROM DPC_Batch_Training WHERE Batch_Reference = @batchRef";

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@batchRef", batchRef);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            batch_Number = reader["BatchNumber"].ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }

            return batch_Number;
            // Set Attachments
        }

        public float GetCoursePrice()
        {
            CoursePrice price = new CoursePrice();

            // Assuming you have a SQL query to fetch the data
            string query = "SELECT CoursePrice FROM DPC_CoursePrice WHERE ID = @id";

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@id", 1);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            float priceValue;

                            if (float.TryParse(reader["CoursePrice"].ToString(), out priceValue))
                            {
                                price.Price = priceValue;
                            }
                            else
                            {
                                // handle parsing error
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }

            return price.Price;
            // Set Attachments
        }

        public SlotInfo GetBatchSlots(string batchRef)
        {
            
            // Assuming you have a SQL query to fetch the data
            string query = "SELECT Max_students_allowed , No_students_allocated FROM DPC_Slots_Management WHERE Batch_Ref = @batchRef";

            SlotInfo info = new SlotInfo();

            using (SqlConnection conn = new SqlConnection(GlobalVar.ConnectionString))
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@batchRef", batchRef);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            info.Allocated = (int)reader["No_students_allocated"];
                            info.Allowed = (int)reader["Max_students_allowed"];
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }

            return info;
        }

        /// <summary>
        /// Get list of main town/village
        /// </summary>
        /// <returns></returns>


        /// <summary>
        /// Get list of Ministries and Departments
        /// </summary>
        /// <returns></returns>


    }
}
