using Microsoft.VisualBasic;
using System;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using cookproject;

namespace Projet_cook
{
    internal class Program
    {
        #region TO DO



        #endregion 




        static void Main(string[] args)
        {

            #region Client
            /*Le client : peut créer compte, s’identifier, s’inscrire dans le programme créateur
            de recette, parcourir la liste des recettes proposées(seulement celles avec un stock
            de produit suffisant), choisir plusieurs plats et/ ou plusieurs fois le même plat,
            convertir ses points, payer la commande.*/

            #region parcours de la liste des recettes
            //On regarde quelles recettes sont faisables avec les stocks actuels :







            #endregion parcours



            #region Création d'un compte
            string connectionString = ""; // Remplacer pra l'id choisi

            string username = "";
            string password = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Comptes (nom_utilisateur, mot_de_passe) VALUES (@username, @password)";//Chat gpt m'a sorti cette ligne je sais pas quoi en faire

                using (MySqlCommand c = new MySqlCommand(query, connection))
                {
                    c.Parameters.AddWithValue("@username", username);
                    c.Parameters.AddWithValue("@password", password);
                    int rowsAffected = c.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Le compte a été créé avec succès !");
                    }
                    else
                    {
                        Console.WriteLine("Erreur lors de la création du compte.");
                    }
                }
            }
            #endregion
            #region S'identifier 

            //création de l'instance de la connexion: canal de communication


            Console.WriteLine("Veuillez entrer votre identifiant de connexion :");
            username = Console.ReadLine();
            Console.WriteLine("Veuillez entrer votre mot de passe:");
            password = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }

            connectionString = $"Server=localhost;Port=3306;Database=SKI;User Id={username};Password={password};";
            MySqlConnection maConnection = new MySqlConnection(connectionString);


            maConnection.Open();

            //création d'une commande vers le canal de communication
            MySqlCommand command = maConnection.CreateCommand();
            //insertion de texte de commande: texte de la requête
            command.CommandText = "select * from Club;";
            //création de structure de réception du résultat
            MySqlDataReader reader;
            //exécution et récupération du résultat
            reader = command.ExecuteReader();


            //ici valueString est un tableau qui va contenir à chaque
            //itération les valeurs de la nouvelle ligne
            //une valeur par case
            string[] valueString = new string[reader.FieldCount];
            //FieldCount c'est quoi alors?
            while (reader.Read())//retourne une nouvelle ligne à chaque itération
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + "\t");
                }
                Console.WriteLine();
            }

            //fermeture de la connexion
            maConnection.Close();
            #endregion
            #region S'inscrire dans le programme de recette
            #region Creation programme
            //creation du programme

            static void CreerRecette(int idClient, int soldePoint, int soldePortefeuille, int idRecette, string connectionString)
            {
                // Insérer la nouvelle recette dans la table "Recettes" de la base de données
                InsererNouvelleRecette(idRecette, connectionString);

                // Insérer l'entrée correspondante dans la table "CreateurRecettes" de la base de données
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO CreateurRecettes (idClient, soldePoint, soldePortefeuille, idRecette) VALUES (@idClient, @soldePoint, @soldePortefeuille, @idRecette)";
                    //voir toujours pour ce qui est entre les guillemets 
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idClient", idClient);
                        command.Parameters.AddWithValue("@soldePoint", soldePoint);
                        command.Parameters.AddWithValue("@soldePortefeuille", soldePortefeuille);
                        command.Parameters.AddWithValue("@idRecette", idRecette);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("La recette a été créée avec succès !");
                        }
                        else
                        {
                            Console.WriteLine("Erreur lors de la création de la recette.");
                        }
                    }
                }
            }

            static void InsererNouvelleRecette(int idRecette, string connectionString)
            {
                // Insérer la nouvelle recette dans la table "Recettes" de la base de données
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Recettes (idRecette) VALUES (@idRecette)";//j'ai reutiliser comme avant mais a voir avec sql

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idRecette", idRecette);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("La recette a été ajoutée à la table 'Recettes' avec succès !");
                        }
                        else
                        {
                            Console.WriteLine("Erreur lors de l'ajout de la recette à la table 'Recettes'.");
                        }
                    }
                }
            }
            #endregion
            #region S'inscrire dans ce programme

            #endregion
            #endregion
            #endregion
            #region Paiement commande
            /*Le paiement de la commande doit permettre une mise à jour du stock des produits,
            du solde client, de son solde point*/

            #endregion
            #region Création de recette
            /*Le créateur de recette : peut saisir une recette(toutes les informations) consulter
            son solde, afficher la liste de ses recettes*/

            #endregion
            #region Gestionnaire CookinGest
            /*Le gestionnaire de CookinGest a un tableau de bord de la semaine qui lui affiche le
            créateur de recette de la semaine(ayant les recettes les plus commandées), le top 5
            de la semaine(les recettes les plus commandées), le Client de la semaine(celui
            ayant payé le plus de commande cette semaine).Il a aussi un tableau de gestion des
            produits : ajouter un produit, afficher la liste des produits, afficher la liste des
            produits à restocker, augmenter le stock d’un produit, supprimer une recette,
            supprimer un client(ce dernier est optionnel)*/


            #endregion



            #region Fonctions
            static int SaisieENtierSec()
            {
                bool val; uint outp;
                do
                {
                    val = uint.TryParse(Console.ReadLine(), out outp);
                } while (!val);
                return Convert.ToInt32(outp);
            }




            #endregion Fonctions
        }

        public List<Recette> AvailableRecipe(MySqlConnection connection)
        {
            
            connection.Open();
            string selectRecettes = "SELECT * FROM Recettes";
            MySqlCommand recettesCommand = new MySqlCommand(selectRecettes, connection);
            MySqlDataReader recettesReader = recettesCommand.ExecuteReader();

            List<Recette> availableRecipes = new List<Recette>();
            while (recettesReader.Read())
            {
                int idRecette = recettesReader.GetInt32("idRecette");
                string nomRecette = recettesReader.GetString("nomRecette");
                string selectIngredients = "SELECT * FROM IngredientRecette INNER JOIN Recettes ON Recettes.idIngredientRecette = IngredientRecette.idIngredientRecette WHERE Recettes.idRecette = @idRecette";
                MySqlCommand ingredientsCommand = new MySqlCommand(selectIngredients, connection);
                ingredientsCommand.Parameters.AddWithValue("@idRecette", idRecette);
                MySqlDataReader ingredientsReader = ingredientsCommand.ExecuteReader();

                bool stockSuffisant = true;

                while (ingredientsReader.Read())
                {
                    int idProduit = ingredientsReader.GetInt32("idProduit");
                    int quantite = ingredientsReader.GetInt32("quantite");

                    string selectProduit = "SELECT * FROM Produit WHERE idProduit = @idProduit";
                    MySqlCommand produitCommand = new MySqlCommand(selectProduit, connection);
                    produitCommand.Parameters.AddWithValue("@idProduit", idProduit);
                    MySqlDataReader produitReader = produitCommand.ExecuteReader();

                    if (produitReader.Read())
                    {
                        string nomProduit = produitReader.GetString("nomProduit");
                        int currentStock = produitReader.GetInt32("currentStock");

                        if (currentStock < quantite)
                        {
                            stockSuffisant = false;
                            break;
                        }
                    }

                    produitReader.Close();
                }

                ingredientsReader.Close();

                if (stockSuffisant)
                {
                    Recette recette = new Recette(recettesReader, connection);
                    availableRecipes.Add(recette);
                }
            }
            recettesReader.Close();
            connection.Close();
            return availableRecipes;
        }
    }
    



    
}