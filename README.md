# Dungeon of Algorithms

Projeto educacional desenvolvido para o curso **Design Patterns em Jogos Digitais** do SENAI.

## Sobre o Projeto

Dungeon of Algorithms é um jogo top-down roguelike desenvolvido em C# com MonoGame, utilizado como base prática para o ensino de padrões de projeto (Design Patterns) aplicados ao desenvolvimento de jogos.

## Requisitos

| Requisito | Versão |
|-----------|--------|
| .NET SDK | 9.0 ou superior |
| MonoGame | 3.8.1 |
| Visual Studio / VS Code | 2022 ou superior |

## Instalação

### 1. Clonar o Repositório

```bash
git clone https://github.com/lucaslopes-ti/dungeon-of-algorithms.git
cd dungeon-of-algorithms
```

### 2. Restaurar Dependências

```bash
dotnet restore
```

### 3. Compilar o Projeto

```bash
dotnet build
```

### 4. Executar o Jogo

```bash
dotnet run
```

## Estrutura do Projeto

```
DungeonOfAlgorithms/
├── Program.cs                    # Ponto de entrada
├── DungeonOfAlgorithms.csproj    # Configuração do projeto
├── Content/                      # Assets do jogo
│   ├── Content.mgcb              # Pipeline de conteúdo
│   ├── Enemies/                  # Sprites de inimigos
│   ├── Fonts/                    # Fontes
│   ├── Maps/                     # Arquivos CSV dos mapas
│   ├── Player/                   # Sprites do jogador
│   └── Tiles/                    # Tilesets
└── Source/                       # Código-fonte
    ├── Core/                     # Classes principais
    │   ├── Game1.cs              # Classe principal do jogo
    │   ├── GameManager.cs        # Singleton - Gerenciador global
    │   ├── EnemyFactory.cs       # Factory - Criação de inimigos
    │   ├── ItemFactory.cs        # Factory - Criação de itens
    │   ├── IEnemyBehavior.cs     # Strategy - Interface de comportamento
    │   ├── EnemyBehaviors.cs     # Strategy - Implementações
    │   ├── DungeonManager.cs     # Gerenciador de salas
    │   ├── Room.cs               # Classe de sala
    │   └── Tilemap.cs            # Sistema de tiles
    └── Entities/                 # Entidades do jogo
        ├── Player.cs             # Jogador
        ├── Enemy.cs              # Inimigo
        └── Item.cs               # Item
```

## Conteúdo do Curso

### Dia 1: Singleton Pattern (4 horas)

**Objetivo:** Compreender e implementar o padrão Singleton para gerenciamento de estado global.

**Arquivos de estudo:**
- `Source/Core/GameManager.cs`

**Atividades:**
- Análise do GameManager existente
- Implementação do AchievementManager

---

### Dia 2: Factory Pattern (4 horas)

**Objetivo:** Aplicar o padrão Factory para centralizar a criação de objetos.

**Arquivos de estudo:**
- `Source/Core/EnemyFactory.cs`
- `Source/Core/ItemFactory.cs`

**Atividades:**
- Análise das factories existentes
- Adicionar novo tipo de inimigo (Bat)
- Implementar ProjectileFactory

---

### Dia 3: Strategy Pattern (4 horas)

**Objetivo:** Implementar o padrão Strategy para comportamentos intercambiáveis de IA.

**Arquivos de estudo:**
- `Source/Core/IEnemyBehavior.cs`
- `Source/Core/EnemyBehaviors.cs`
- `Source/Entities/Enemy.cs`

**Atividades:**
- Análise dos comportamentos existentes
- Implementar WanderBehavior
- Implementar comportamentos avançados (Circle, Ambush, Boss)

---

### Dia 4: Grafos e Projeto Final (4 horas)

**Objetivo:** Utilizar estrutura de dados Grafo para conectar salas do dungeon.

**Arquivos de estudo:**
- `Source/Core/DungeonManager.cs`
- `Source/Core/Room.cs`

**Atividades:**
- Implementar DungeonGraph
- Algoritmo BFS para pathfinding
- Projeto final integrando todos os padrões

---

## Controles do Jogo

| Tecla | Ação |
|-------|------|
| W / Seta Cima | Mover para cima |
| S / Seta Baixo | Mover para baixo |
| A / Seta Esquerda | Mover para esquerda |
| D / Seta Direita | Mover para direita |
| Espaço | Atacar |
| ESC | Pausar / Menu |

## Design Patterns Implementados

| Padrão | Categoria | Aplicação no Projeto |
|--------|-----------|---------------------|
| Singleton | Criacional | GameManager, DatabaseManager |
| Factory | Criacional | EnemyFactory, ItemFactory |
| Strategy | Comportamental | IEnemyBehavior e implementações |

## Tecnologias Utilizadas

- **Linguagem:** C# 12
- **Framework:** .NET 9.0
- **Engine:** MonoGame 3.8.1
- **IDE:** Visual Studio 2022 / Visual Studio Code

## Referências

**Livros:**
- Design Patterns: Elements of Reusable Object-Oriented Software (GoF, 1994)
- Game Programming Patterns (Robert Nystrom, 2014)
- Head First Design Patterns (Freeman & Robson, 2004)

**Documentação:**
- [MonoGame Documentation](https://docs.monogame.net/)
- [Refactoring Guru - Design Patterns](https://refactoring.guru/design-patterns)

## Suporte

Em caso de dúvidas ou problemas:
1. Verifique se todos os requisitos estão instalados
2. Execute `dotnet restore` para restaurar dependências
3. Consulte o instrutor durante as aulas

## Licença

Este projeto é de uso exclusivo educacional para o curso de Programacao de Jogos Digitais na unidade de Design Patterns do SENAI.

---

**SENAI - Serviço Nacional de Aprendizagem Industrial**

Curso Técnico em Programação de Jogos Digitais
