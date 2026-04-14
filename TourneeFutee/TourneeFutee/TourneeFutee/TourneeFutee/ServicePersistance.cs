using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace TourneeFutee
{
    /// <summary>
    /// Service de persistance permettant de sauvegarder et charger
    /// des graphes et des tournées dans une base de données MySQL.
    /// </summary>
    public class ServicePersistance
    {
        // Attributs privés
        private readonly string _connectionString;

       
        // Constructeur
     
        /// Instancie un service de persistance et vérifie la connexion à la base
        public ServicePersistance(string serverIp, string dbname, string user, string pwd)
        {
            _connectionString = "server={serverIp};database={dbname};uid={user};pwd={pwd};";
            try
            {
                using (var conn = OpenConnection())
                {
                    
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Connexion à la base de données impossible : {ex.Message}", ex);
            }
        }
   
        // Méthodes publiques   

      
        /// Sauvegarde le graphe <paramref name="g"/> en base de données
        /// (sommets et arcs inclus) et renvoie son identifiant.
     
        public uint SaveGraph(Graph g)
        {
            try
            {
                using (var conn = OpenConnection())
                {
                    // 1. Insérer une ligne dans Graphe, récupérer son id.
                    uint graphId;
                    string sqlGraphe = "INSERT INTO Graphe (nb_sommets, oriente) VALUES (@nbSommets, @oriente); SELECT LAST_INSERT_ID();";
                    using (var cmd = new MySqlCommand(sqlGraphe, conn))
                    {
                        cmd.Parameters.AddWithValue("@nbSommets", g.Order);
                        cmd.Parameters.AddWithValue("@oriente", g.Directed ? 1 : 0);
                        graphId = Convert.ToUInt32(cmd.ExecuteScalar());
                    }

                    // 2. Insérer chaque sommet, conserver la correspondance nom → id BdD.
                    var sommetIds = new Dictionary<string, uint>();
                    int ordre = 0;
                    foreach (string nom in g.Vertices)
                    {
                        float valeur = g.GetVertexValue(nom);
                        string sqlSommet = "INSERT INTO Sommet (nom, valeur, graphe_id, ordre) VALUES (@nom, @valeur, @grapheId, @ordre); SELECT LAST_INSERT_ID();";
                        using (var cmd = new MySqlCommand(sqlSommet, conn))
                        {
                            cmd.Parameters.AddWithValue("@nom", nom);
                            cmd.Parameters.AddWithValue("@valeur", valeur);
                            cmd.Parameters.AddWithValue("@grapheId", graphId);
                            cmd.Parameters.AddWithValue("@ordre", ordre);
                            sommetIds[nom] = Convert.ToUInt32(cmd.ExecuteScalar());
                        }
                        ordre++;
                    }

                    // 3. Insérer les arcs en parcourant les voisins de chaque sommet.
                    foreach (string sourceName in g.Vertices)
                    {
                        foreach (string destName in g.GetNeighbors(sourceName))
                        {
                            float poids = g.GetEdgeWeight(sourceName, destName);
                            string sqlArc = "INSERT INTO Arc (source_id, destination_id, poids, graphe_id) VALUES (@sourceId, @destId, @poids, @grapheId);";
                            using (var cmd = new MySqlCommand(sqlArc, conn))
                            {
                                cmd.Parameters.AddWithValue("@sourceId", sommetIds[sourceName]);
                                cmd.Parameters.AddWithValue("@destId", sommetIds[destName]);
                                cmd.Parameters.AddWithValue("@poids", poids);
                                cmd.Parameters.AddWithValue("@grapheId", graphId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    return graphId;
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Erreur lors de la sauvegarde du graphe : {ex.Message}", ex);
            }
        }

      
        /// Charge depuis la base de données le graphe identifié par <paramref name="id"/>
        /// et renvoie une instance de la classe <see cref="Graph"/>.
        public Graph LoadGraph(uint id)
        {
            try
            {
                using (var conn = OpenConnection())
                {
                    // 1. Charger les métadonnées du graphe.
                    bool oriente;
                    string sqlGraphe = "SELECT oriente FROM Graphe WHERE id = @id;";
                    using (var cmd = new MySqlCommand(sqlGraphe, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new ArgumentException("Graphe avec id={id} introuvable.");
                            oriente = Convert.ToBoolean(reader["oriente"]);
                        }
                    }

                    var graph = new Graph(oriente);

                    // 2. Charger les sommets dans leur ordre d'insertion.
                    var dbIdToName = new Dictionary<uint, string>();
                    string sqlSommets = "SELECT id, nom, valeur FROM Sommet WHERE graphe_id = @grapheId ORDER BY ordre ASC;";
                    using (var cmd = new MySqlCommand(sqlSommets, conn))
                    {
                        cmd.Parameters.AddWithValue("@grapheId", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                uint sommetId = Convert.ToUInt32(reader["id"]);
                                string nom = reader["nom"]?.ToString() ?? string.Empty;
                                float valeur = Convert.ToSingle(reader["valeur"]);
                                graph.AddVertex(nom, valeur);
                                dbIdToName[sommetId] = nom;
                            }
                        }
                    }

                    // 3. Charger les arcs et reconstituer la matrice d'adjacence.
                    string sqlArcs = "SELECT source_id, destination_id, poids FROM Arc WHERE graphe_id = @grapheId;";
                    using (var cmd = new MySqlCommand(sqlArcs, conn))
                    {
                        cmd.Parameters.AddWithValue("@grapheId", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                uint sourceId = Convert.ToUInt32(reader["source_id"]);
                                uint destId = Convert.ToUInt32(reader["destination_id"]);
                                float poids = Convert.ToSingle(reader["poids"]);
                                graph.SetEdgeWeight(dbIdToName[sourceId], dbIdToName[destId], poids);
                            }
                        }
                    }

                    return graph;
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erreur lors du chargement du graphe {id} : {ex.Message}", ex);
            }
        }

       
        /// Sauvegarde la tournée <paramref name="t"/> en base de données
        /// et renvoie son identifiant.
     
        public uint SaveTour(uint graphId, Tour t)
        {
            try
            {
                using (var conn = OpenConnection())
                {
                    // 1. Insérer la tournée, récupérer son id.
                    uint tourId;
                    string sqlTournee = "INSERT INTO Tournee (cout_total, graphe_id) VALUES (@coutTotal, @grapheId); SELECT LAST_INSERT_ID();";
                    using (var cmd = new MySqlCommand(sqlTournee, conn))
                    {
                        cmd.Parameters.AddWithValue("@coutTotal", t.Cost);
                        cmd.Parameters.AddWithValue("@grapheId", graphId);
                        tourId = Convert.ToUInt32(cmd.ExecuteScalar());
                    }

                    // 2. Insérer chaque étape avec son numéro d'ordre.
                    int ordre = 0;
                    foreach (string vertexName in t.Vertices)
                    {
                        // Retrouver l'id BdD du sommet par son nom dans ce graphe.
                        uint sommetId;
                        string sqlGetSommet = "SELECT id FROM Sommet WHERE nom = @nom AND graphe_id = @grapheId LIMIT 1;";
                        using (var cmd = new MySqlCommand(sqlGetSommet, conn))
                        {
                            cmd.Parameters.AddWithValue("@nom", vertexName);
                            cmd.Parameters.AddWithValue("@grapheId", graphId);
                            var result = cmd.ExecuteScalar();
                            if (result == null)
                                throw new ArgumentException("Sommet '{vertexName}' introuvable dans le graphe {graphId}.");
                            sommetId = Convert.ToUInt32(result);
                        }

                        string sqlEtape = "INSERT INTO EtapeTournee (tournee_id, sommet_id, ordre) VALUES (@tourneeId, @sommetId, @ordre);";
                        using (var cmd = new MySqlCommand(sqlEtape, conn))
                        {
                            cmd.Parameters.AddWithValue("@tourneeId", tourId);
                            cmd.Parameters.AddWithValue("@sommetId", sommetId);
                            cmd.Parameters.AddWithValue("@ordre", ordre);
                            cmd.ExecuteNonQuery();
                        }
                        ordre++;
                    }

                    return tourId;
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erreur lors de la sauvegarde de la tournée : {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Charge depuis la base de données la tournée identifiée par <paramref name="id"/>
        /// et renvoie une instance de la classe <see cref="Tour"/>.
        /// </summary>
        public Tour LoadTour(uint id)
        {
            try
            {
                using (var conn = OpenConnection())
                {
                    // 1. Charger le coût total de la tournée.
                    float coutTotal;
                    string sqlTournee = "SELECT cout_total FROM Tournee WHERE id = @id;";
                    using (var cmd = new MySqlCommand(sqlTournee, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        var result = cmd.ExecuteScalar();
                        if (result == null)
                            throw new ArgumentException($"Tournée avec id={id} introuvable.");
                        coutTotal = Convert.ToSingle(result);
                    }

                    // 2. Charger les étapes triées par numéro d'ordre.
                    var vertices = new List<string>();
                    string sqlEtapes = @"SELECT s.nom
                                         FROM EtapeTournee et
                                         JOIN Sommet s ON et.sommet_id = s.id
                                         WHERE et.tournee_id = @tourneeId
                                         ORDER BY et.ordre ASC;";
                    using (var cmd = new MySqlCommand(sqlEtapes, conn))
                    {
                        cmd.Parameters.AddWithValue("@tourneeId", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vertices.Add(reader["nom"]?.ToString() ?? string.Empty);
                            }
                        }
                    }

                    return new Tour(vertices, coutTotal);
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Erreur lors du chargement de la tournée {id} : {ex.Message}", ex);
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Méthodes utilitaires privées
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Crée et retourne une nouvelle connexion MySQL ouverte.
        /// Encadrez toujours l'appel dans un bloc using pour garantir la fermeture.
        /// </summary>
        private MySqlConnection OpenConnection()
        {
            var conn = new MySqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}
