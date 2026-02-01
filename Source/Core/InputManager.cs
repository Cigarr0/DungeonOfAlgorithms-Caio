// ═══════════════════════════════════════════════════════════════════════════════
// 🎹 INPUT MANAGER - O Tradutor de Dedadas no Teclado
// ═══════════════════════════════════════════════════════════════════════════════
// Design Pattern: Singleton
// Este cara escuta TUDO que você digita. Tipo um keylogger, mas do bem.
// Transforma suas marteladas no teclado em movimentos elegantes.
// WASD, setas, espaço... ele entende todos os idiomas dos gamers.
// ═══════════════════════════════════════════════════════════════════════════════

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DungeonOfAlgorithms.Source.Core;

/// <summary>
/// Gerenciador de inputs. Escuta o teclado igual mãe escuta fofoca.
/// </summary>
public class InputManager
{
    // 🔒 Singleton - só um ouvido para governar todos
    private static InputManager _instance;
    public static InputManager Instance => _instance ??= new InputManager();

    // 📊 Estados do teclado - presente e passado (não prevê o futuro, infelizmente)
    private KeyboardState _currentKeyboardState;  // O que está sendo apertado AGORA
    private KeyboardState _previousKeyboardState; // O que estava sendo apertado ANTES

    /// <summary>
    /// Construtor privado. Inicializa os estados pra não dar ruim no primeiro frame.
    /// </summary>
    private InputManager()
    {
        // Inicializa os estados para evitar leituras inconsistentes no primeiro Update
        // Sem isso, o jogo acha que você apertou TODAS as teclas no início
        _currentKeyboardState = Keyboard.GetState();
        _previousKeyboardState = _currentKeyboardState;
    }

    /// <summary>
    /// Atualiza os estados do teclado. Chame isso TODO FRAME ou vai dar ruim.
    /// </summary>
    public void Update()
    {
        // Guarda o estado anterior (tipo uma memória de peixe dourado)
        _previousKeyboardState = _currentKeyboardState;
        // Pega o estado atual (o que está sendo apertado AGORA)
        _currentKeyboardState = Keyboard.GetState();
    }

    /// <summary>
    /// Retorna a direção do movimento baseado em WASD ou setas.
    /// Normalizado para não andar mais rápido na diagonal (sem trapaça!).
    /// </summary>
    /// <returns>Vetor de direção normalizado ou zero se parado</returns>
    public Vector2 GetMovementDirection()
    {
        Vector2 direction = Vector2.Zero;

        // ⬆️ Cima - W ou Seta pra cima
        if (_currentKeyboardState.IsKeyDown(Keys.W) || _currentKeyboardState.IsKeyDown(Keys.Up))
            direction.Y -= 1;
        // ⬇️ Baixo - S ou Seta pra baixo
        if (_currentKeyboardState.IsKeyDown(Keys.S) || _currentKeyboardState.IsKeyDown(Keys.Down))
            direction.Y += 1;
        // ⬅️ Esquerda - A ou Seta pra esquerda
        if (_currentKeyboardState.IsKeyDown(Keys.A) || _currentKeyboardState.IsKeyDown(Keys.Left))
            direction.X -= 1;
        // ➡️ Direita - D ou Seta pra direita
        if (_currentKeyboardState.IsKeyDown(Keys.D) || _currentKeyboardState.IsKeyDown(Keys.Right))
            direction.X += 1;

        // Normaliza pra não virar o Flash na diagonal
        if (direction != Vector2.Zero)
            direction.Normalize();

        return direction;
    }

    /// <summary>
    /// Retorna true apenas no frame em que a tecla foi PRESSIONADA (não segurada).
    /// Útil para ações únicas tipo pular, atacar, pausar...
    /// </summary>
    public bool IsKeyPressed(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    }

    /// <summary>
    /// Retorna true enquanto a tecla estiver sendo SEGURADA.
    /// Útil para movimento contínuo.
    /// </summary>
    public bool IsKeyDown(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key);
    }

    /// <summary>
    /// Verifica se a ação principal (espaço) foi pressionada.
    /// Mantido por compatibilidade com código antigo.
    /// </summary>
    public bool IsActionPressed()
    {
        return IsKeyPressed(Keys.Space);
    }
}
