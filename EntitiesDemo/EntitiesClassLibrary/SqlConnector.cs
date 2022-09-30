using EntitiesClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace EntitiesClassLibrary
{
    public class SqlConnector
    {
        private const string connectionStringName = "Entities";

        public List<Entity> GetAllEntities()
        {
            List<Entity> entities = new List<Entity>();
            List<EntityType> types = GetAllEntityTypes();
            string query = "select * from Entities";
            DataTable dataTable = GetDataTable(query);
            foreach (DataRow row in dataTable.Rows)
            {
                Entity entity = new Entity();
                entity.Id = Convert.ToInt32(row["id"]);
                if (row["EntityName"] != null)
                {
                    entity.Name = Convert.ToString(row["EntityName"]);
                }                
                if (row["EntityDescription"]!=null)
                {
                    entity.Description = Convert.ToString(row["EntityDescription"]);
                }
                if (row["EntityPrice"] != null)
                {
                    entity.Price = Convert.ToDecimal(row["EntityPrice"]);
                }
                if (row["EntityTypeId"] != null)
                {
                    entity.Type = types.Where(x => x.Id == Convert.ToInt32(row["EntityTypeId"])).FirstOrDefault();
                }                
                entities.Add(entity);
            }
            return entities;
        }
        public List<EntityType> GetAllEntityTypes()
        {
            List<EntityType> types = new List<EntityType>();
            string query = "select * from EntityTypes";
            DataTable dataTable = GetDataTable(query);
            foreach (DataRow row in dataTable.Rows)
            {
                types.Add(new EntityType
                {
                    Id = Convert.ToInt32(row["id"]),
                    TypeName = Convert.ToString(row["TypeName"])
                });
            }
            return types;
        }

        private DataTable GetDataTable(string selectQuery)
        {
            using (SqlConnection connection = new SqlConnection(Connector.GetConnectionString(connectionStringName)))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
}
