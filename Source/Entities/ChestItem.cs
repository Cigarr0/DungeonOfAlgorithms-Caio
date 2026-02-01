// ═══════════════════════════════════════════════════════════════════════════════
// 📦 CHEST ITEM - O Baú do Tesouro (O Grande Prêmio!)
// ═══════════════════════════════════════════════════════════════════════════════
// Este é o baú que todo mundo quer achar. É tipo achar dinheiro no bolso
// da calça que você não lavava há 3 meses. Satisfação pura.
// Herda de Item porque também é coletável, mas é ESPECIAL.
// ═══════════════════════════════════════════════════════════════════════════════

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonOfAlgorithms.Source.Core;

namespace DungeonOfAlgorithms.Source.Entities;

/// <summary>
/// O baú do tesouro - objetivo final do jogo!
/// Coletar isso = VITÓRIA! 🏆
/// </summary>
public class ChestItem : Item
{
    /// <summary>
    /// Cria um novo baú do tesouro.
    /// ID 999 porque ele é lendário, único, especial.
    /// </summary>
    /// <param name="texture">A textura do baú (brilhante, dourado, irresistível)</param>
    /// <param name="position">Onde esconder o tesouro</param>
    public ChestItem(Texture2D texture, Vector2 position) 
        : base(999, "Treasure Chest", texture, position) // 999 = ID lendário
    {
        // O construtor da classe pai faz todo o trabalho
        // Esse baú é basicamente um Item com ego inflado
    }
}
