using Raylib_cs;

namespace IAudio
{
    /// <summary>
    /// Interface para carregar streams de audio Raylib
    /// </summary>
    internal interface IAudio
    {
        /// <summary>
        /// Enum de diferentes arquivos de musica
        /// </summary>
        enum Musica
        {
            TelaInicial,
            Puzzle,
            Boss
        }

        /// <summary>
        /// Enum de diferentes arquivos de Efeitos sonoros
        /// </summary>
        enum EfeitoSonoro
        {
            Andar,
            Bloco,
            Fogo,
            Agua,
            Madeira,
            Parede
        }

        /// <summary>
        /// Retorna AudioStream da musica
        /// </summary>
        /// <param name="musica">Musica</param>
        /// <returns>AudioStream</returns>
        Music getMusica(Musica musica);

        /// <summary>
        /// Retorna AudioStream do efeito sonoro
        /// </summary>
        /// <param name="som">EfeitoSonoro</param>
        /// <returns>AudioStream</returns>
        Sound getSom(EfeitoSonoro som);
    }
}
