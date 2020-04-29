using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace wpf_demo_phonebook
{
    class PhonebookDAO
    {
        private DbConnection conn;

        public PhonebookDAO()
        {
            conn = new DbConnection();
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par nom
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByName(string _name)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE FirstName LIKE @firstName OR LastName LIKE @lastName ";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@firstName", SqlDbType.NVarChar);
            parameters[0].Value = _name;

            parameters[1] = new SqlParameter("@lastName", SqlDbType.NVarChar);
            parameters[1].Value = _name;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par id
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByID(int _id)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            return conn.ExecuteSelectQuery(_query, parameters);
        }
        /// <summary>
        /// Méthode permettant de rechercher un contact par id
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchAll()
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] ";

            

            return conn.ExecuteSelectQuery(_query);
        }
        public void Save(ContactModel cm)
        {
            string _query =
                $"UPDATE [Contacts] " +
                $"SET FirstName = @firstName, LastName = @lastName, Email = @email, Phone = @phone, Mobile = @mobile "+
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = cm.ContactID;

            parameters[1] = new SqlParameter("@firstName", SqlDbType.NVarChar);
            parameters[1].Value = cm.FirstName;

            parameters[2] = new SqlParameter("@lastName", SqlDbType.NVarChar);
            parameters[2].Value = cm.LastName;

            parameters[3] = new SqlParameter("@email", SqlDbType.NVarChar);
            parameters[3].Value = cm.Email;

            parameters[4] = new SqlParameter("@phone", SqlDbType.NVarChar);
            parameters[4].Value = cm.Phone;

            parameters[5] = new SqlParameter("@mobile", SqlDbType.NVarChar);
            parameters[5].Value = cm.Mobile;

            conn.ExecutUpdateQuery(_query, parameters);
        }
        public void Supp(int _id)
        {
            string _query =
                $"DELETE " +
                $"FROM [Contacts] " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            conn.ExecutDeleteQuery(_query, parameters);
        }
        public void Add(ContactModel cm)
        {
            string _query =
                $"INSERT INTO [Contacts] " +
                $"VALUES ( @firstName, @lastName, @email, @phone, @mobile ) ";

            SqlParameter[] parameters = new SqlParameter[5];

            parameters[0] = new SqlParameter("@firstName", SqlDbType.NVarChar);
            parameters[0].Value = cm.FirstName;

            parameters[1] = new SqlParameter("@lastName", SqlDbType.NVarChar);
            parameters[1].Value = cm.LastName;

            parameters[2] = new SqlParameter("@email", SqlDbType.NVarChar);
            parameters[2].Value = cm.Email;

            parameters[3] = new SqlParameter("@phone", SqlDbType.NVarChar);
            parameters[3].Value = cm.Phone;

            parameters[4] = new SqlParameter("@mobile", SqlDbType.NVarChar);
            parameters[4].Value = cm.Mobile;

            conn.ExecutInsertQuery(_query, parameters);
        }
    }
}
