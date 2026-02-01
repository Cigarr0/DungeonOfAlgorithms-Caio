// ═══════════════════════════════════════════════════════════════════════════════
// 🎭 INTERFACE DE ENTIDADE - O Contrato dos Cidadãos do Jogo
// ═══════════════════════════════════════════════════════════════════════════════
// Esta interface é o contrato que toda entidade do jogo precisa assinar.
// "Quer existir no meu jogo? Então implementa Update e Draw!"
// Player, Enemy, Item... todos precisam seguir essa regra.
// Democracia? Não. Ditadura das interfaces. E funciona.
// ═══════════════════════════════════════════════════════════════════════════════

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonOfAlgorithms.Source.Core;

/// <summary>
/// Interface base para todas as entidades do jogo.
/// Se existe no jogo e se move ou aparece na tela, implementa isso.
/// </summary>
public interface IGameEntity
{
    /// <summary>
    /// Atualiza a lógica da entidade. Chamado todo frame.
    /// Aqui você move, calcula, pensa, respira... virtualmente.
    /// </summary>
    /// <param name="gameTime">Tempo de jogo pra cálculos de física</param>
    void Update(GameTime gameTime);
    
    /// <summary>
    /// Desenha a entidade na tela. Também chamado todo frame.
    /// Se não implementar isso, sua entidade é invisível (e inútil).
    /// </summary>
    /// <param name="spriteBatch">O pincel mágico pra desenhar sprites</param>
    void Draw(SpriteBatch spriteBatch);
}
