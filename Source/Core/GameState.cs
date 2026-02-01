// ═══════════════════════════════════════════════════════════════════════════════
// 🎭 ESTADOS DO JOGO - A montanha-russa emocional do gameplay
// ═══════════════════════════════════════════════════════════════════════════════
// Este enum é tipo o humor do jogo. Às vezes está feliz (Playing),
// às vezes está triste (GameOver), às vezes só quer uma pausa (Paused).
// É basicamente eu durante a semana de provas.
// ═══════════════════════════════════════════════════════════════════════════════

namespace DungeonOfAlgorithms.Source.Core;

/// <summary>
/// Os possíveis estados emocionais... digo, estados do jogo.
/// </summary>
public enum GameState
{
    MainMenu,   // 📋 Menu principal - "Bora jogar ou vazar?"
    Playing,    // 🎮 Jogando - Onde a ação acontece!
    Paused,     // ⏸️ Pausado - Hora do café ou do banheiro
    GameOver,   // 💀 Game Over - F no chat, amigo
    Victory     // 🏆 Vitória - VOCÊ É O CARA! (ou a cara!)
}
