using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cookproject
{
    internal class Recette
    {
        private string nomRecette;
        private string categorie;
        private string descriptif;
        private int prixvente;
        private int recompense;
        public int IdRecette { get; set; }
        public string NomRecette { get; set; }
        public List<IngredientRecette> Ingredients { get; set; }

        public Recette(MySqlDataReader reader, MySqlConnection connection)
        {
            IdRecette = reader.GetInt32(reader.GetOrdinal("idRecette"));
            NomRecette = reader.GetString(reader.GetOrdinal("nomRecette"));
            Ingredients = new List<IngredientRecette>();

            LoadIngredients(connection);
        }

        private void LoadIngredients(MySqlConnection connection)
        {
            string selectIngredients = "SELECT * FROM IngredientRecette INNER JOIN Recettes ON Recettes.idIngredientRecette = IngredientRecette.idIngredientRecette WHERE Recettes.idRecette = @idRecette";
            MySqlCommand ingredientsCommand = new MySqlCommand(selectIngredients, connection);
            ingredientsCommand.Parameters.AddWithValue("@idRecette", IdRecette);
            MySqlDataReader ingredientsReader = ingredientsCommand.ExecuteReader();

            while (ingredientsReader.Read())
            {
                IngredientRecette ingredient = new IngredientRecette(ingredientsReader);
                Ingredients.Add(ingredient);
            }

            ingredientsReader.Close();
        }


    }
}
