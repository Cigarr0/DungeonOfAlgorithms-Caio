// ═══════════════════════════════════════════════════════════════════════════════
// 💾 DATABASE MANAGER - O Guardião dos Saves
// ═══════════════════════════════════════════════════════════════════════════════
// Design Pattern: Singleton
// Este cara salva e carrega seu progresso usando SQLite.
// É tipo Ctrl+S da vida real. Sem ele, você perde tudo quando fecha o jogo.
// Tecnologia: SQLite - um banco de dados que cabe num arquivo só!
// ═══════════════════════════════════════════════════════════════════════════════

using System;
using Microsoft.Data.Sqlite;
using System.IO;
using Microsoft.Xna.Framework;

namespace DungeonOfAlgorithms.Source.Core;

/// <summary>
/// Gerenciador de banco de dados. Salva e carrega seu progresso.
/// Basicamente o HD externo do jogo.
/// </summary>
public class DatabaseManager
{
    // Singleton - só um banco pra governar todos os saves
    private static DatabaseManager _instance;
    public static DatabaseManager Instance => _instance ??= new DatabaseManager();

    // String de conexão com o banco (onde fica o arquivo .db)
    private string _connectionString;

    /// <summary>
    /// Construtor privado. Cria o banco de dados se não existir.
    /// </summary>
    private DatabaseManager() 
    {
        // Cria o arquivo do banco do lado do executável
        // Assim você não precisa de servidor, SQLite é portátil!
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "game.db");
        _connectionString = $"Data Source={dbPath}";
        
        // Debug: mostra onde o banco foi criado (útil pra achar o arquivo)
        System.Console.WriteLine("DEBUG: Banco de dados salvo em: " + dbPath);
        
        // Cria as tabelas se não existirem
        InitializeDatabase();
    }

    /// <summary>
    /// Cria a estrutura do banco de dados (tabelas).
    /// Se já existir, não faz nada (CREATE TABLE IF NOT EXISTS é vida).
    /// </summary>
    private void InitializeDatabase()
    {
        using var con = new SqliteConnection(_connectionString);
        con.Open();

        // SQL pra criar a tabela de saves
        // Id = slot de save (por enquanto só usamos o slot 1)
        // Level = qual sala/fase o player está
        // PlayerX/Y = posição do player no mundo
        // Score = pontuação (pra se gabar depois)
        string sql = @"
            CREATE TABLE IF NOT EXISTS SaveSlots (
                Id INTEGER PRIMARY KEY,
                Level INTEGER,
                PlayerX REAL,
                PlayerY REAL,
                Score INTEGER
            )";

        using var cmd = new SqliteCommand(sql, con);
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Salva o jogo no slot 1. É tipo dar Ctrl+S.
    /// </summary>
    /// <param name="level">ID da sala atual</param>
    /// <param name="position">Posição X,Y do player</param>
    /// <param name="score">Pontuação atual</param>
    public void SaveGame(int level, Vector2 position, int score)
    {
        using var con = new SqliteConnection(_connectionString);
        con.Open();

        // INSERT OR REPLACE = se existir, substitui. Se não, cria.
        // É tipo Ctrl+S que sobrescreve o arquivo anterior
        string sql = "INSERT OR REPLACE INTO SaveSlots (Id, Level, PlayerX, PlayerY, Score) VALUES (1, @Level, @X, @Y, @Score)";
        
        using var cmd = new SqliteCommand(sql, con);
        cmd.Parameters.AddWithValue("@Level", level);
        cmd.Parameters.AddWithValue("@X", position.X);
        cmd.Parameters.AddWithValue("@Y", position.Y);
        cmd.Parameters.AddWithValue("@Score", score);
        
        cmd.ExecuteNonQuery();
        System.Diagnostics.Debug.WriteLine("🎮 Jogo Salvo! Seus neurônios agradecem.");
    }

    /// <summary>
    /// Carrega o jogo do slot 1. É tipo dar Ctrl+O.
    /// </summary>
    /// <returns>Dados do save ou null se não tiver save</returns>
    public (int Level, Vector2 Position, int Score)? LoadGame()
    {
        using var con = new SqliteConnection(_connectionString);
        con.Open();

        string sql = "SELECT Level, PlayerX, PlayerY, Score FROM SaveSlots WHERE Id = 1";
        using var cmd = new SqliteCommand(sql, con);
        
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            // Extrai os dados do banco
            int level = reader.GetInt32(0);
            float x = reader.GetFloat(1);
            float y = reader.GetFloat(2);
            int score = reader.GetInt32(3);
            
            // Retorna uma tupla com todos os dados (C# moderno é lindo)
            return (level, new Vector2(x, y), score);
        }
        
        // Não achou save? Retorna null (sem save = começa do zero)
        return null;
    }
}

