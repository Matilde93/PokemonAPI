CREATE TABLE [dbo].[Pokemons](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[level] [int] NOT NULL,
	[PokeDex] [int] NOT NULL,
 )