// ═══════════════════════════════════════════════════════════════════════════════
// 📊 HUD - Heads-Up Display (ou "Coisas Importantes Na Sua Cara")
// ═══════════════════════════════════════════════════════════════════════════════
// Design Pattern: Observer (observa o Player e mostra seus dados)
// O HUD é tipo aquele amigo que fica te lembrando quanto dinheiro você tem
// e quantas vidas restam. Útil, mas às vezes irritante.
// ═══════════════════════════════════════════════════════════════════════════════

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonOfAlgorithms.Source.Entities;

namespace DungeonOfAlgorithms.Source.Core;

/// <summary>
/// Interface gráfica do usuário - mostra HP, Score e outras coisas importantes.
/// Tipo o painel do carro, mas pra jogos.
/// </summary>
public class HUD
{
    // 🔤 A fonte usada pra escrever na tela
    private SpriteFont _font;
    
    /// <summary>
    /// Cria um novo HUD. Precisa de uma fonte, senão fica mudo.
    /// </summary>
    /// <param name="font">A fonte pra desenhar o texto</param>
    public HUD(SpriteFont font)
    {
        _font = font;
    }

    /// <summary>
    /// Desenha o HUD na tela. Mostra vida e pontuação do player.
    /// </summary>
    /// <param name="spriteBatch">O pincel mágico do MonoGame</param>
    /// <param name="player">O jogador (pra pegar os dados dele)</param>
    public void Draw(SpriteBatch spriteBatch, Player player)
    {
        // ❤️ Mostra a vida - vermelho porque sangue
        string healthText = $"HP: {player.Health}";
        spriteBatch.DrawString(_font, healthText, new Vector2(10, 10), Color.Red);
        
        // 💰 Mostra a pontuação - dourado porque dinheiro
        string scoreText = $"Score: {player.Score}";
        spriteBatch.DrawString(_font, scoreText, new Vector2(10, 30), Color.Gold);
    }
}
