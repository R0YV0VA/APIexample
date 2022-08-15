using ExtensionsCRUD.Application.Services;
using ExtensionsCRUD.Domain.Models;
using Npgsql;
using System;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace ExtensionsCRUD.Infrastructure.Services
{
    public class ExtensionService : IExtensionService
    {
        
        public ExtensionModel createExtension(string _name, string _description, string _version, string _conString)
        {
            using var con = new NpgsqlConnection(_conString);
            con.Open();
            using var database = new NpgsqlCommand("INSERT INTO extensions(name, description, version) VALUES(@name, @description, @version) RETURNING *;", con);
            database.Parameters.AddWithValue("name", _name);
            database.Parameters.AddWithValue("description", _description);
            database.Parameters.AddWithValue("version", _version);
            database.Prepare();
            using NpgsqlDataReader rdr = database.ExecuteReader();
            var extension = new ExtensionModel();
            while (rdr.Read())
            {
                extension.Id = rdr.GetInt32(0);
                extension.Name = rdr.GetString(1);
                extension.Description = rdr.GetString(2);
                extension.Version = rdr.GetString(3);
            }
            return extension;

        }
        public ExtensionModel getExtensionById(int _id, string _conString)
        {
            using var con = new NpgsqlConnection(_conString);
            con.Open();
            using var database = new NpgsqlCommand($"SELECT * FROM extensions WHERE id={_id}", con);
            using NpgsqlDataReader rdr = database.ExecuteReader();
            var extension = new ExtensionModel();
            while (rdr.Read())
            {
                extension.Id = rdr.GetInt32(0);
                extension.Name = rdr.GetString(1);
                extension.Description = rdr.GetString(2);
                extension.Version = rdr.GetString(3);
            }
            return extension;
        }
        public List<ExtensionModel> getExtensions(string _conString)
        {
            using var con = new NpgsqlConnection(_conString);
            con.Open();
            using var database = new NpgsqlCommand("SELECT * FROM extensions", con);
            using NpgsqlDataReader rdr = database.ExecuteReader();
            var extensions = new List<ExtensionModel>();
            while (rdr.Read())
            {
                var extension = new ExtensionModel();
                extension.Id = rdr.GetInt32(0);
                extension.Name = rdr.GetString(1);
                extension.Description = rdr.GetString(2);
                extension.Version = rdr.GetString(3);
                extensions.Add(extension);
            }
            return extensions;
        }
        public ExtensionModel putExtension(int _id, string _name, string _description, string _version, string _conString)
        {
            using var con = new NpgsqlConnection(_conString);
            con.Open();
            using var database = new NpgsqlCommand("UPDATE extensions SET name=@name, description=@description, version=@version WHERE id=@id RETURNING *;", con);
            database.Parameters.AddWithValue("id", _id);
            database.Parameters.AddWithValue("name", _name);
            database.Parameters.AddWithValue("description", _description);
            database.Parameters.AddWithValue("version", _version);
            database.Prepare();
            using NpgsqlDataReader rdr = database.ExecuteReader();
            var extension = new ExtensionModel();
            while (rdr.Read())
            {
                extension.Id = rdr.GetInt32(0);
                extension.Name = rdr.GetString(1);
                extension.Description = rdr.GetString(2);
                extension.Version = rdr.GetString(3);
            }
            return extension;
        }
        public ExtensionModel deleteExtensionById(int _id, string _conString)
        {
            using var con = new NpgsqlConnection(_conString);
            con.Open();
            using var database = new NpgsqlCommand($"DELETE FROM extensions WHERE id={_id} RETURNING *;", con);
            using NpgsqlDataReader rdr = database.ExecuteReader();
            var extension = new ExtensionModel();
            while (rdr.Read())
            {
                extension.Id = rdr.GetInt32(0);
                extension.Name = rdr.GetString(1);
                extension.Description = rdr.GetString(2);
                extension.Version = rdr.GetString(3);
            }
            return extension;
        }
    }
}
