using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cookproject
{
    internal class IngredientRecette
    {
        private int idProduit;
        private int quantite; 

        public IngredientRecette(MySqlDataReader reader)
        {
            idProduit = reader.GetInt32(reader.GetOrdinal("idProduit"));
            quantite = reader.GetInt32(reader.GetOrdinal("quantite"));
        }


    }
}
