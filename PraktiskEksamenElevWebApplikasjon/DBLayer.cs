using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace PraktiskEksamenElevWebApplikasjon
{
    public class DBLayer
    {
        public void BindGrid()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnElev"].ConnectionString;
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Elev", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

                reader.Close();
                conn.Close();
            }
        }

        public DataTable GetElevByNavn(string navn)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnElev"].ConnectionString;
            DataTable dt = new DataTable();
            SqlParameter param;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //den samme sql queryen her som dere allerede har testet i sql manager - her med et parameter, som er telefonnummeret
                SqlCommand cmd = new SqlCommand("SELECT Elev.ElevID, Elev.Fornavn, Elev.Etternavn, Elev.Adresse, Poststeder.Poststed, Klasse.KlasseNavn FROM Elev JOIN Poststeder ON Elev.PostNr = Poststeder.PostNr JOIN Klasse ON Elev.KlasseID = Klasse.KlasseID WHERE Elev.Fornavn = @navn", conn);
                cmd.CommandType = CommandType.Text;

                //params here
                param = new SqlParameter("@navn", SqlDbType.NVarChar);
                param.Value = navn; //variabel som blir sendt inn til metoden. Kommer fra bruker som tastet dette inn i et tekstfelt.
                cmd.Parameters.Add(param);

                SqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

                reader.Close();
                conn.Close();
            }
            return dt; //hele datasettet returneres. skal da kobles til feks en gridview
        }

        public DataTable GetElevByFag(string fagNavn)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnElev"].ConnectionString;
            DataTable dt = new DataTable();
            SqlParameter param;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //den samme sql queryen her som dere allerede har testet i sql manager - her med et parameter, som er telefonnummeret
                SqlCommand cmd = new SqlCommand("SELECT Elev.Fornavn, Elev.Etternavn, Fag.FagKode, Fag.Fagnavn FROM Elev INNER JOIN FagElev ON Elev.ElevID = FagElev.ElevID INNER JOIN Fag ON FagElev.FagKode = Fag.FagKode WHERE (Fag.Fagnavn = @fagNavn OR @fagNavn = ''))", conn);
                cmd.CommandType = CommandType.Text;

                //params here
                param = new SqlParameter("@fagNavn", SqlDbType.NVarChar);
                param.Value = fagNavn; //variabel som blir sendt inn til metoden. Kommer fra bruker som tastet dette inn i et tekstfelt.
                cmd.Parameters.Add(param);

                SqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

                reader.Close();
                conn.Close();
            }
            return dt; //hele datasettet returneres. skal da kobles til feks en gridview
        }
        public DataTable GetElevByKlasse(string klasseNavn)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnElev"].ConnectionString;
            DataTable dt = new DataTable();
            SqlParameter param;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //den samme sql queryen her som dere allerede har testet i sql manager - her med et parameter, som er telefonnummeret
                SqlCommand cmd = new SqlCommand("SELECT Elev.Fornavn, Elev.Etternavn, Klasse.KlasseNavn FROM Elev INNER JOIN Klasse ON Elev.KlasseID = Klasse.KlasseID\r\nWHERE (Klasse.KlasseNavn = @klasseNavn OR @klasseNavn = '')", conn);
                cmd.CommandType = CommandType.Text;

                //params here
                param = new SqlParameter("@klasseNavn", SqlDbType.NVarChar);
                param.Value = klasseNavn; //variabel som blir sendt inn til metoden. Kommer fra bruker som tastet dette inn i et tekstfelt.
                cmd.Parameters.Add(param);

                SqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

                reader.Close();
                conn.Close();
            }
            return dt; //hele datasettet returneres. skal da kobles til feks en gridview
        }

        public DataTable GetElevByKlasseogFag(string klasseNavn, string fagNavn)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnElev"].ConnectionString;
            DataTable dt = new DataTable();
            SqlParameter param;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //den samme sql queryen her som dere allerede har testet i sql manager - her med et parameter, som er telefonnummeret
                SqlCommand cmd = new SqlCommand("Sort by begge SELECT Elev.Fornavn, Elev.Etternavn, Klasse.KlasseNavn, Fag.FagKode, Fag.Fagnavn FROM Elev INNER JOIN Klasse ON Elev.KlasseID = Klasse.KlasseID INNER JOIN FagElev ON Elev.ElevID = FagElev.ElevID INNER JOIN Fag ON FagElev.FagKode = Fag.FagKode WHERE (Fag.FagNavn = Fag.FagNavn OR Fag.FagNavn = '') AND (Klasse.KlasseNavn = Klasse.KlasseNavn OR Klasse.KlasseNavn = '')\r\nORDER BY Klasse.KlasseID ASC;", conn);
                cmd.CommandType = CommandType.Text;

                //params here
                param = new SqlParameter("@klasseNavn", SqlDbType.NVarChar);
                param.Value = klasseNavn; //variabel som blir sendt inn til metoden. Kommer fra bruker som tastet dette inn i et tekstfelt.
                cmd.Parameters.Add(param);
                param = new SqlParameter("@fagNavn", SqlDbType.NVarChar);
                param.Value = fagNavn; //variabel som blir sendt inn til metoden. Kommer fra bruker som tastet dette inn i et tekstfelt.
                cmd.Parameters.Add(param);

                SqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

                reader.Close();
                conn.Close();
            }
            return dt; //hele datasettet returneres. skal da kobles til feks en gridview
        }
    }
}