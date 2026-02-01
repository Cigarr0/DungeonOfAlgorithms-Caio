// ═══════════════════════════════════════════════════════════════════════════════
// PLAYER
// ═══════════════════════════════════════════════════════════════════════════════
// Este é o protagonista! O cara que você controla. O escolhido.
// Ele anda, coleta moedas, leva dano e (esperamos) vence o jogo.
// Implementa IGameEntity porque precisa de Update e Draw.
// Tem animação, colisão, invencibilidade... é complexo, mas vale a pena.
// ═══════════════════════════════════════════════════════════════════════════════

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using DungeonOfAlgorithms.Source.Core;

namespace DungeonOfAlgorithms.Source.Entities;

/// <summary>
/// O jogador - herói controlável do jogo.
/// Anda, coleta itens, leva dano e tenta não morrer.
/// </summary>
public class Player : IGameEntity
{
    // ═══════════════════════════════════════════════════════════════════════════
    // PROPRIEDADES PÚBLICAS
    // ═══════════════════════════════════════════════════════════════════════════
    
    /// <summary>Posição do player no mundo (X, Y)</summary>
    public Vector2 Position { get; private set; }
    
    /// <summary>Velocidade de movimento em pixels por segundo</summary>
    public float Speed { get; set; } = 100f;
    
    /// <summary>Pontos de vida. Zero = Game Over. F.</summary>
    public int Health { get; private set; } = 100;
    
    /// <summary>Pontuação total (moedas coletadas)</summary>
    public int Score { get; private set; } = 0;
    
    /// <summary>Tá vivo? Se Health > 0, sim. Senão... RIP.</summary>
    public bool IsAlive => Health > 0;
    
    // ═══════════════════════════════════════════════════════════════════════════
    // 🎨 ANIMAÇÃO
    // ═══════════════════════════════════════════════════════════════════════════
    
    // Dicionário com todas as texturas de animação
    private Dictionary<string, Texture2D> _textures;
    
    // Qual animação está tocando agora
    private string _currentAnimation = "Down_Idle";
    
    // Efeito de espelhamento (pra não precisar de sprites separados pra esquerda)
    private SpriteEffects _flipEffect = SpriteEffects.None;
    
    // ═══════════════════════════════════════════════════════════════════════════
    // 
    // ESTADO E DIREÇÃO
    // ═══════════════════════════════════════════════════════════════════════════
    
    /// <summary>Enum pra saber pra onde o player está olhando</summary>
    private enum FaceDirection { Down, Up, Left, Right }
    private FaceDirection _facing = FaceDirection.Down;
    
    // Timer de invencibilidade (pra não levar 500 de dano por segundo)
    private float _invincibilityTimer = 0f;
    private const float INVINCIBILITY_DURATION = 1.0f; // 1 segundo de god mode
    
    // ═══════════════════════════════════════════════════════════════════════════
    // 🎬 CONTROLE DE FRAMES
    // ═══════════════════════════════════════════════════════════════════════════
    
    private const int FRAME_WIDTH = 32;       // Largura de cada frame
    private const int FRAME_HEIGHT = 32;      // Altura de cada frame
    private const int IDLE_FRAME_COUNT = 4;   // Quantos frames tem a animação idle
    private const int MOVE_FRAME_COUNT = 6;   // Quantos frames tem a animação de andar
    private const float FRAME_TIME = 0.15f;   // Tempo entre frames (segundos)
    
    private int _currentFrame = 0;            // Frame atual da animação
    private float _frameTimer = 0f;           // Timer pra trocar de frame
    private bool _isMoving = false;           // Tá andando?
    private bool _wasMoving = false;          // Tava andando no frame anterior?

    /// <summary>
    /// Área de colisão do player. Menor que o sprite pra ser mais justo.
    /// Ninguém gosta de hitbox injusta!
    /// </summary>
    public Rectangle Bounds => new Rectangle((int)Position.X + 8, (int)Position.Y + 16, 16, 16);

    /// <summary>
    /// Cria um novo player.
    /// </summary>
    /// <param name="textures">Dicionário com todas as texturas de animação</param>
    /// <param name="startPosition">Posição inicial no mundo</param>
    public Player(Dictionary<string, Texture2D> textures, Vector2 startPosition)
    {
        _textures = textures;
        Position = startPosition;
    }

    /// <summary>
    /// Teleporta o player pra uma nova posição. Usado em transições de sala.
    /// </summary>
    public void SetPosition(Vector2 newPosition)
    {
        Position = newPosition;
    }

    /// <summary>
    /// Aplica dano ao player. Só funciona se não estiver invencível.
    /// </summary>
    /// <param name="amount">Quantidade de dano</param>
    public void TakeDamage(int amount)
    {
        // Invencível? Ignora o dano (git gud, monstros!)
        if (_invincibilityTimer <= 0)
        {
            Health -= amount;
            _invincibilityTimer = INVINCIBILITY_DURATION; // Ativa god mode temporário
            System.Console.WriteLine($"💔 Tomou {amount} de dano! Vida: {Health}");
        }
    }

    /// <summary>
    /// Adiciona pontos ao score. Dinheiro = felicidade.
    /// </summary>
    public void AddScore(int points)
    {
        Score += points;
        System.Console.WriteLine($"💰 Score: {Score}");
    }

    /// <summary>
    /// Update básico (sem colisão com tilemap).
    /// </summary>
    public void Update(GameTime gameTime)
    {
        Update(gameTime, null);
    }
    
    /// <summary>
    /// Update completo com colisão de tilemap.
    /// </summary>
    public void Update(GameTime gameTime, Tilemap tilemap)
    {
        // Pega a direção do input (WASD ou setas)
        var direction = InputManager.Instance.GetMovementDirection();
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Atualiza timer de invencibilidade
        if (_invincibilityTimer > 0)
            _invincibilityTimer -= deltaTime;

        // ═══════════════════════════════════════════════════════════════════════
        // 🎨 LÓGICA DE ANIMAÇÃO
        // ═══════════════════════════════════════════════════════════════════════
        
        _isMoving = direction != Vector2.Zero;
        
        // Determina a direção que o player está olhando
        if (direction.Y > 0) _facing = FaceDirection.Down;
        else if (direction.Y < 0) _facing = FaceDirection.Up;
        else if (direction.X > 0) _facing = FaceDirection.Right;
        else if (direction.X < 0) _facing = FaceDirection.Left;
        
        // Escolhe a animação certa baseado na direção e se está andando
        _flipEffect = SpriteEffects.None;
        string suffix = _isMoving ? "" : "_Idle";
        
        switch (_facing)
        {
            case FaceDirection.Down:
                _currentAnimation = "Down" + suffix;
                break;
            case FaceDirection.Up:
                _currentAnimation = "Up" + suffix;
                break;
            case FaceDirection.Right:
                _currentAnimation = "Side" + suffix;
                // Espelha horizontalmente (o sprite original olha pra esquerda)
                _flipEffect = SpriteEffects.FlipHorizontally;
                break;
            case FaceDirection.Left:
                _currentAnimation = "Side" + suffix;
                _flipEffect = SpriteEffects.None;
                break;
        }

        // Reseta o frame quando muda de estado (parado <-> andando)
        if (_isMoving != _wasMoving)
        {
            _currentFrame = 0;
            _frameTimer = 0f;
            _wasMoving = _isMoving;
        }
        
        // Avança os frames da animação
        int frameCount = _isMoving ? MOVE_FRAME_COUNT : IDLE_FRAME_COUNT;
        _frameTimer += deltaTime;
        if (_frameTimer >= FRAME_TIME)
        {
            _frameTimer = 0f;
            _currentFrame = (_currentFrame + 1) % frameCount; // Loop infinito
        }

        // ═══════════════════════════════════════════════════════════════════════
        // 🏃 LÓGICA DE MOVIMENTO (com colisão)
        // ═══════════════════════════════════════════════════════════════════════
        
        Vector2 newPosition = Position + direction * Speed * deltaTime;
        
        // Se tem tilemap, verifica colisão
        if (tilemap != null)
        {
            // Tenta mover em X separadamente
            Rectangle xBounds = new Rectangle((int)newPosition.X + 8, (int)Position.Y + 16, 16, 16);
            if (!tilemap.IsColliding(xBounds))
            {
                Position = new Vector2(newPosition.X, Position.Y);
            }
            
            // Tenta mover em Y separadamente
            // Isso permite "deslizar" nas paredes em vez de travar
            Rectangle yBounds = new Rectangle((int)Position.X + 8, (int)newPosition.Y + 16, 16, 16);
            if (!tilemap.IsColliding(yBounds))
            {
                Position = new Vector2(Position.X, newPosition.Y);
            }
        }
        else
        {
            // Sem tilemap = movimento livre
            Position = newPosition;
        }
    }

    /// <summary>
    /// Desenha o player na tela com a animação atual.
    /// </summary>
    public void Draw(SpriteBatch spriteBatch)
    {
        // Efeito de piscar quando invencível (estilo clássico de dano)
        if (_invincibilityTimer > 0 && (int)(_invincibilityTimer * 10) % 2 == 0)
            return; // Pula o draw nesse frame = pisca!
        
        // Pega a textura da animação atual
        Texture2D texture = _textures[_currentAnimation];
        
        // Calcula qual parte da spritesheet desenhar (frame atual)
        Rectangle sourceRect = new Rectangle(_currentFrame * FRAME_WIDTH, 0, FRAME_WIDTH, FRAME_HEIGHT);
            
        // Desenha com o efeito de flip se necessário
        spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0f, Vector2.Zero, 1f, _flipEffect, 0f);
    }
}
