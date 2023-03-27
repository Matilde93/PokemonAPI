namespace PokemonLibrary
{
    
        public class Pokemon
        {
            public int Id { get; set; } //Ikke null
            public string? Name { get; set; }//Ikke null, minimum længde 2
            public int Level { get; set; }//Ikke null, 1-99
            public int Pokedex { get; set; }//Ikke null, pokedex >0


            //Hvis man laver en constructer, skal man lave en tom også grundet deserialization

            public override bool Equals(object? obj)
            {
                if (obj == null) return false;
                if (obj.GetType() != typeof(Pokemon)) return false;
                Pokemon pokemon = (Pokemon)obj;
                if (pokemon.Id != Id) return false;
                if (pokemon.Name != Name) return false;
                if (pokemon.Level != Level) return false;
                if (pokemon.Pokedex != Pokedex) return false;
                return true;
            }

            public void ValidateName()
            {
                if (Name == null) { throw new ArgumentNullException(); }
                if (Name.Length < 2) { throw new ArgumentException(); }
            }

            public void ValidateLevel()
            {
                if (Level < 1 || Level > 99) throw new ArgumentOutOfRangeException();
            }

            public void ValidatePokedex()
            {
                if (Pokedex < 1) throw new ArgumentOutOfRangeException();
            }


            public void Validate()
            {
                ValidateName();
                ValidateLevel();
                ValidatePokedex();
            }

            public override string ToString()
            {
                return $"Id: {Id} - Name: {Name}, Pokedex: {Pokedex}, Level: {Level}";
            }
        }

}