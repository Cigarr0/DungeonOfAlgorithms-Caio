// ═══════════════════════════════════════════════════════════════════════════════
// COMPORTAMENTOS DE INIMIGOS - O Manual do Monstro
// ═══════════════════════════════════════════════════════════════════════════════
// Design Pattern: Strategy
// Aqui ficam as implementações concretas dos comportamentos de inimigos.
// PatrolBehavior = Monstro preguiçoso que anda pra lá e pra cá
// ChaseBehavior = Monstro psicopata que te persegue até o fim dos tempos
// ═══════════════════════════════════════════════════════════════════════════════

using Microsoft.Xna.Framework;
using DungeonOfAlgorithms.Source.Entities;
using System;

namespace DungeonOfAlgorithms.Source.Core;

/// <summary>
/// Comportamento de Patrulha - O Guarda Desatento
/// O monstro anda pra esquerda e direita, tipo segurança de shopping.
/// Não te persegue ativamente, mas se você entrar no caminho... BONK!
/// </summary>
public class PatrolBehavior : IEnemyBehavior
{
    // Timer pra trocar de direção
    private float _timer;
    
    // Direção atual (começa indo pra direita)
    private Vector2 _direction = new Vector2(1, 0);

    /// <summary>
    /// Atualiza o patrulheiro. Anda pra um lado, espera 2 segundos, vira, repete.
    /// Basicamente a vida de um NPC de RPG.
    /// </summary>
    public void Update(Enemy enemy, Player player, GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _timer += deltaTime;

        // A cada 2 segundos, inverte a direção (vai pra lá, volta pra cá)
        if (_timer > 2f)
        {
            _direction *= -1; // Inverte: direita vira esquerda e vice-versa
            _timer = 0;
        }

        // Move o inimigo na direção atual
        enemy.Position += _direction * enemy.Speed * deltaTime;
        
        // TODO: Em um jogo completo, poderíamos fazer:
        // if (Vector2.Distance(enemy.Position, player.Position) < 100)
        //     enemy.ChangeBehavior(new ChaseBehavior());
        // Mas por enquanto, fica só patrulhando feliz da vida
    }
}

/// <summary>
/// 🏃 Comportamento de Perseguição - O Stalker Profissional
/// O monstro te persegue sem descanso. Tipo cobrança de cartão de crédito.
/// Não importa pra onde você vá, ele VAI te encontrar.
/// </summary>
public class ChaseBehavior : IEnemyBehavior
{
    /// <summary>
    /// Atualiza o perseguidor. Calcula a direção até o player e CORRE!
    /// É a IA mais simples possível
    /// </summary>
    public void Update(Enemy enemy, Player player, GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Calcula a direção: Player - Enemy = vetor apontando pro player
        Vector2 direction = player.Position - enemy.Position;
        
        // Normaliza pra não ficar mais rápido quando longe
        if (direction != Vector2.Zero)
            direction.Normalize();

        // Move na direção do player (0.8 = 80% da velocidade, pra dar chance)
        // Se fosse 100%, seria impossível fugir. Somos bonzinhos.
        enemy.Position += direction * (enemy.Speed * 0.8f) * deltaTime;
    }
}
